using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera boardCamera;

    public void ShowBoardCamera()
    {
        mainCamera.enabled = false;
        boardCamera.enabled = true;
    }

    public void ShowMainCamera()
    {
        mainCamera.enabled = true;
        boardCamera.enabled = false;
    }
}
