using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;         // Reference to the Main Camera
    public Camera boardCamera;        // Reference to the Board Camera
    public Transform target;          // The stone to follow during zoom
    public float zoomInHeight = 15f;  // Height when zooming in on the stone
    public float zoomOutHeight = 30f; // Height when showing the full board
    public float zoomSpeed = 2f;      // Speed of zooming in and out

    private Vector3 initialPosition;  // Starting position for zoom-out
    private bool isZoomingIn = false;
    private bool isZoomingOut = false;

    void Start()
    {
        // Initialize cameras: Main Camera is active, Board Camera is inactive
        mainCamera.enabled = true;
        boardCamera.enabled = false;

        // Set initial position to zoom-out height, keeping the full board in view
        initialPosition = new Vector3(0, zoomOutHeight, 0);
        boardCamera.transform.position = initialPosition;
    }

    void LateUpdate()
    {
        if (isZoomingIn)
        {
            ZoomInToStone();
        }
        else if (isZoomingOut)
        {
            ZoomOutToBoard();
        }
    }

    public void StartZoomIn()
    {
        // Activate Board Camera and start zooming in
        mainCamera.enabled = false;
        boardCamera.enabled = true;
        isZoomingIn = true;
        isZoomingOut = false;
    }

    public void StartZoomOut()
    {
        // Begin zoom-out process
        isZoomingOut = true;
        isZoomingIn = false;
    }

    private void ZoomInToStone()
    {
        // Target the stone position but keep the camera in top-down view
        Vector3 targetPosition = target.position;
        targetPosition.y = zoomInHeight; // Set the camera to zoom-in height

        // Smoothly move the camera towards the stone
        boardCamera.transform.position = Vector3.Lerp(boardCamera.transform.position, targetPosition, zoomSpeed * Time.deltaTime);

        // Stop zooming in when close enough to the target position
        if (Vector3.Distance(boardCamera.transform.position, targetPosition) < 0.1f)
        {
            isZoomingIn = false;
        }
    }

    private void ZoomOutToBoard()
    {
        // Smoothly move the camera back to initial zoom-out position
        boardCamera.transform.position = Vector3.Lerp(boardCamera.transform.position, initialPosition, zoomSpeed * Time.deltaTime);

        // Stop zooming out when close enough to the initial position
        if (Vector3.Distance(boardCamera.transform.position, initialPosition) < 0.1f)
        {
            isZoomingOut = false;
            boardCamera.enabled = false; // Deactivate board camera after zoom-out
            mainCamera.enabled = true;   // Reactivate main camera
        }
    }
}
