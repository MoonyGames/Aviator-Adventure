public class MovingUpControl : Control
{
    protected override void StartAction()
    {
        Plane.StartMovingUp();
    }

    protected override void StopAction()
    {
        Plane.StopMovingUp();
    }
}
