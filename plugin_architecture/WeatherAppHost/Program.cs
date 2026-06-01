using System.Reflection;
using PlugInBase;

namespace WeatherAppHost;

public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // Each plugin is deployed into its own subfolder under "plugins" next to
            // the host. Keeping plugins in separate directories (none of which contain
            // PlugInBase.dll) is what lets the host share the single ICommand type.
            var pluginsRoot = Path.Combine(AppContext.BaseDirectory, "plugins");

            var commands = DiscoverPluginAssemblies(pluginsRoot)
                .SelectMany(pluginPath => CreateCommands(LoadPlugin(pluginPath)))
                .ToList();

            Console.WriteLine("Welcome to the Weather App.");

            if (args.Length == 0)
            {
                Console.WriteLine();
                Console.WriteLine(commands.Count == 0 ? "No plugins found." : "Available commands:");
                foreach (var command in commands)
                {
                    Console.WriteLine($"  {command.Name,-14} {command.Description}");
                }

                Console.WriteLine();
                Console.WriteLine("Usage: dotnet run --project WeatherAppHost <command> [<command> ...]");
                return;
            }

            foreach (var commandName in args)
            {
                Console.WriteLine($"-- {commandName} --");

                var command = commands.FirstOrDefault(c => c.Name == commandName);
                if (command is null)
                {
                    Console.WriteLine();
                    Console.WriteLine("No such command is known.");
                    return;
                }

                command.Invoke();
            }

            Console.WriteLine("\nApplication Closing");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    // Each plugin lives in plugins\<PluginName>\<PluginName>.dll. We load only that
    // assembly per folder (its private dependencies are resolved on demand), so we
    // never mistake a dependency DLL for a plugin.
    private static IEnumerable<string> DiscoverPluginAssemblies(string pluginsRoot)
    {
        if (!Directory.Exists(pluginsRoot))
        {
            yield break;
        }

        foreach (var dir in Directory.GetDirectories(pluginsRoot))
        {
            var assemblyPath = Path.Combine(dir, Path.GetFileName(dir) + ".dll");
            if (File.Exists(assemblyPath))
            {
                yield return assemblyPath;
            }
        }
    }

    // Load a plugin assembly inside its own isolated PluginLoadContext.
    private static Assembly LoadPlugin(string assemblyPath)
    {
        Console.WriteLine($"Loading commands from: {assemblyPath}");

        var loadContext = new PluginLoadContext(assemblyPath);
        return loadContext.LoadFromAssemblyName(
            new AssemblyName(Path.GetFileNameWithoutExtension(assemblyPath)));
    }

    // Reflect over an assembly and instantiate every public type that implements ICommand.
    private static IEnumerable<ICommand> CreateCommands(Assembly assembly)
    {
        var count = 0;

        foreach (var type in assembly.GetTypes())
        {
            if (typeof(ICommand).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            {
                if (Activator.CreateInstance(type) is ICommand command)
                {
                    count++;
                    yield return command;
                }
            }
        }

        if (count == 0)
        {
            var availableTypes = string.Join(", ", assembly.GetTypes().Select(t => t.FullName));
            throw new ApplicationException(
                $"Can't find any type which implements ICommand in {assembly} from {assembly.Location}.\n" +
                $"Available types: {availableTypes}");
        }
    }
}
