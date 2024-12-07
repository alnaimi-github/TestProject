namespace MyHomeAutomationSystem.Internals;

public class MotionSensor : IEntity
{
    public MotionSensor(string name, bool state)
    {
        Name  = name;
        State = state ? "true" : "false";
    }

    public string Name { get; }
    public string State { get; }
}

public class MotionSensorWithMotion : MotionSensor
{
    public MotionSensorWithMotion(string name) : base($"motion.{name}", true)
    {

    }
}
public class MotionSensorWithoutMotion : MotionSensor
{
    public MotionSensorWithoutMotion(string name) : base($"motion.{name}", false)
    {

    }
}