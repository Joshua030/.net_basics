namespace PlugInBase;

// The shared contract every plugin must implement. This interface lives in its
// own assembly so both the host and every plugin can reference the SAME type.
public interface ICommand
{
    // Unique name used to select the command from the command line.
    string Name { get; }

    // Human-readable description of what the command does.
    string Description { get; }

    // Runs the command. Returns an exit/status code (0 = success).
    int Invoke();
}
