public class ShootingControl : Control
{
    protected override void StartAction()
    {
        Plane.StartShooting();
    }

    protected override void StopAction()
    {
        Plane.StopShooting();
    }
}
