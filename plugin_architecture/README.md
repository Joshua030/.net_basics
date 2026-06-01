# C# Plugin Architecture — WeatherApp

A minimal, working example of the **plugin architecture pattern** in .NET, built while
following the Code Maze article
[*C# Plugin Architecture Pattern*](https://code-maze.com/csharp-plugin-architecture-pattern/).

A console **host** discovers and runs **plugins** at runtime — without referencing them at
compile time. New commands can be added simply by dropping a new plugin assembly next to
the host.

---

## What is the plugin architecture pattern?

The idea is to split an application into:

- A small **host** that knows *how* to find and run features, but not *which* features exist.
- A shared **contract** (an interface) that defines what a feature looks like.
- Any number of **plugins** that implement the contract independently.

The host loads plugin assemblies dynamically (via reflection + `AssemblyLoadContext`),
finds the types that implement the contract, instantiates them, and calls them. Because the
host only depends on the *contract* — never on the concrete plugins — you can add, remove,
or update features without recompiling the host.

---

## Project layout

```
plugin_architecture.slnx
│
├── PlugInBase/                 ← shared contract (class library)
│   └── ICommand.cs             ← the ONE interface every plugin implements
│
├── WeatherAppHost/             ← the host (console / Exe)
│   ├── Program.cs              ← load → discover → invoke
│   └── PluginLoadContext.cs    ← isolated assembly loading + dependency resolution
│
└── TemperatureCommands/        ← a plugin (class library)
    └── TemperatureCommand.cs   ← implements ICommand
```

```
PlugInBase (ICommand)
  ▲                       ▲
  │ copy-local ref        │ ExcludeAssets=runtime ref
  │ (PlugInBase.dll in     │ (NO PlugInBase.dll in plugin output)
  │  host output)          │
WeatherAppHost ◄─ build-order only ─ TemperatureCommands (plugin)
(the host)        (ReferenceOutputAssembly=false)   │
  │                                                  │ deploys its files into
  │ at runtime scans:                                ▼
  └─►  bin/.../plugins/TemperatureCommands/TemperatureCommands.dll
```

Two things make this work:

- The host has **no compile-time reference** to `TemperatureCommands`
  (`ReferenceOutputAssembly=false`) — it only forces the plugin to build first and
  discovers its types by reflection at runtime.
- Each plugin is deployed into its **own** `plugins/<PluginName>/` subfolder that does
  **not** contain `PlugInBase.dll`. That separate location is essential (see
  [the key trick](#the-key-trick-one-shared-pluginbasedll) below).

---

## How it works

### 1. The contract — `PlugInBase/ICommand.cs`

```csharp
public interface ICommand
{
    string Name { get; }         // used to select the command from the CLI
    string Description { get; }   // shown in the command list
    int Invoke();                 // runs the command (0 = success)
}
```

### 2. The plugin — `TemperatureCommands/TemperatureCommand.cs`

```csharp
public class TemperatureCommand : ICommand
{
    public string Name => "temperature";
    public string Description => "Displays high and low temperatures for the user's location.";

    public int Invoke()
    {
        Console.WriteLine("In your area, there will be a high of 84F and a low of 69F.");
        return 0;
    }
}
```

### 3. Isolated loading — `WeatherAppHost/PluginLoadContext.cs`

Each plugin is loaded into its own [`AssemblyLoadContext`](https://learn.microsoft.com/dotnet/api/system.runtime.loader.assemblyloadcontext).
This keeps plugins isolated from one another and lets each resolve its own private
dependencies via an `AssemblyDependencyResolver` (which reads the plugin's `.deps.json`).

```csharp
protected override Assembly? Load(AssemblyName assemblyName)
{
    var path = _resolver.ResolveAssemblyToPath(assemblyName);
    return path is null ? null : LoadFromAssemblyPath(path);   // null ⇒ fall back to default context
}
```

### 4. Discover + invoke — `WeatherAppHost/Program.cs`

The host:
1. Looks in the `plugins/` folder next to itself and, for each subfolder, loads
   `plugins/<Name>/<Name>.dll` through a `PluginLoadContext`.
2. Reflects for public types implementing `ICommand` and creates them with `Activator.CreateInstance`.
3. Matches the command names passed on the command line and calls `Invoke()`.

---

## The key trick: one shared `PlugInBase.dll`

For the host's `typeof(ICommand)` and a plugin's `ICommand` to be the **same .NET type**,
there must be exactly **one** `PlugInBase.dll` loaded. If the plugin shipped its own copy,
the runtime would treat them as two different types and the cast to `ICommand` would fail.

That is why **both** the host and the plugin reference `PlugInBase` like this:

```xml
<ProjectReference Include="..\PlugInBase\PlugInBase.csproj">
  <Private>false</Private>            <!-- don't copy PlugInBase.dll locally -->
  <ExcludeAssets>runtime</ExcludeAssets>
</ProjectReference>
```

- `ExcludeAssets=runtime` / `Private=false` → `PlugInBase.dll` is **not** copied into the
  plugin's output, and the plugin is deployed to its own `plugins/<Name>/` folder.
- So when the plugin asks for `PlugInBase`, the resolver can't find it locally (it isn't in
  the plugin's folder), `Load` returns `null`, and the runtime loads it from the **default
  context** — the single copy living in the host's output. One DLL, one type identity. ✅

> ⚠️ **Why a separate folder matters.** If `PlugInBase.dll` sat *next to* the plugin DLL,
> the resolver would find and load its **own** copy into the plugin's context. You'd then
> have two `PlugInBase.ICommand` types from the same file path but with different identities,
> and the cast `is ICommand` would silently fail. Isolating each plugin in its own directory
> (without the contract DLL) is what prevents this.

Plugins also set `<EnableDynamicLoading>true</EnableDynamicLoading>`, which produces the
`.deps.json` the resolver needs.

---

## Build & run

```powershell
# from the repo root
dotnet build plugin_architecture.slnx

# list available commands
dotnet run --project WeatherAppHost

# run the temperature plugin
dotnet run --project WeatherAppHost temperature
```

Expected output for `temperature`:

```
Loading commands from: ...\WeatherAppHost\bin\Debug\net10.0\plugins\TemperatureCommands\TemperatureCommands.dll
Welcome to the Weather App.
-- temperature --
In your area, there will be a high of 84F and a low of 69F.

Application Closing
```

Running with no arguments lists the available commands; an unknown command prints
`No such command is known.`

---

## Adding a new plugin

1. Create a new class library, e.g. `dotnet new classlib -n HumidityCommands`.
2. Give its `.csproj` the same `EnableDynamicLoading` flag, the same `PlugInBase`
   reference (with `Private=false` + `ExcludeAssets=runtime`), and the same
   `CopyToHostPlugins` MSBuild target shown in `TemperatureCommands.csproj` (the target is
   generic — it uses `$(MSBuildProjectName)`, so no edits are needed).
3. Add a class implementing `ICommand`.
4. Add a `ProjectReference` from `WeatherAppHost` to the new plugin with
   `ReferenceOutputAssembly=false` (build-order only — see `WeatherAppHost.csproj`).
5. Add the project to `plugin_architecture.slnx` and rebuild — the host discovers the new
   command automatically from its `plugins/HumidityCommands/` folder.
