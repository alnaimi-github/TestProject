using MyHomeAutomationSystem.Internals;

namespace BddAndSpecFlow;
[Binding]
public class Transforms
{

    [StepArgumentTransformation]
    public MotionSensorWithMotion MotionSensorWithMotionTransform(string roomName)
    {
        return new MotionSensorWithMotion(roomName);
    }

    [StepArgumentTransformation]
    public MotionSensorWithoutMotion MotionSensorWithoutMotionTransform(string roomName)
    {
        return new MotionSensorWithoutMotion(roomName);
    }
}

