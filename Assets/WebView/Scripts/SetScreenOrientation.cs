using UnityEngine;

public class SetScreenOrientation : MonoBehaviour
{
    private ScreenOrientation _screenOrientation = ScreenOrientation.AutoRotation;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Screen.orientation = _screenOrientation;
    }
}
