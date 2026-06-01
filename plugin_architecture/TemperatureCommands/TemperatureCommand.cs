using PlugInBase;

namespace TemperatureCommands;

// A concrete plugin. The host discovers it by reflection — it never references
// this class directly.
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
