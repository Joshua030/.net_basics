using System.Reflection;
using System.Runtime.Loader;

namespace WeatherAppHost;

// A custom load context that lets us load plugin assemblies (and their private
// dependencies) at runtime, isolated from the host's default context.
public class PluginLoadContext : AssemblyLoadContext
{
    private readonly AssemblyDependencyResolver _resolver;

    public PluginLoadContext(string pluginPath)
    {
        // The resolver reads the plugin's .deps.json to locate its dependencies.
        _resolver = new AssemblyDependencyResolver(pluginPath);
    }

    // Called when the runtime needs an assembly this context depends on.
    // Returning null falls back to the default context — that is exactly what we
    // want for PlugInBase, so the host and plugin share the same ICommand type.
    protected override Assembly? Load(AssemblyName assemblyName)
    {
        var assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
        return assemblyPath is null ? null : LoadFromAssemblyPath(assemblyPath);
    }

    // Same idea for native (unmanaged) libraries a plugin might ship with.
    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        var libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        return libraryPath is null ? IntPtr.Zero : LoadUnmanagedDllFromPath(libraryPath);
    }
}
