using System.Reflection.Metadata.Ecma335;

namespace MyHomeAutomationSystem.Internals;

public class BinarySensor : IEntity
{
    public BinarySensor(string name, bool state)
    {
        Name  = name;
        State = state ? "true" : "false";
    }

    public string Name { get; }
    public string State { get; }
    public bool IsOn => State == "true";
    public bool IsOff => State == "false";
}