using UnityEngine;

public class SecondaryCameraController : MonoBehaviour
{
    public Camera boardCamera;
    public float zoomInSize = 5f;
    public float zoomOutSize = 10f;
    public float zoomSpeed = 2f;

    private bool isZoomingIn = false;

    void Update()
    {
        float targetSize = isZoomingIn ? zoomInSize : zoomOutSize;
        boardCamera.orthographicSize = Mathf.Lerp(boardCamera.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
    }

    public void UpdateZoom(bool zoomIn)
    {
        isZoomingIn = zoomIn;
    }
}
