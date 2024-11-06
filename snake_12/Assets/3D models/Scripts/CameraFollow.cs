using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The stone (chessman) to follow
    public float zoomOutHeight = 30f; // Height when showing the whole board
    public float zoomInHeight = 15f; // Height when zooming in on the stone
    public float zoomSpeed = 2f; // Speed of zooming in and out
    public float smoothSpeed = 0.1f; // Camera movement smoothness

    private Vector3 initialPosition; // Starting position of the camera
    private bool isZoomingIn = false;
    private bool isZoomingOut = false;

    void Start()
    {
        // Set the initial position of the camera to show the whole board
        initialPosition = new Vector3(0, zoomOutHeight, 0); // Adjust to ensure whole board is visible
        transform.position = initialPosition;
    }

    void LateUpdate()
    {
        // If the target (stone) is assigned, handle zooming and movement
        if (target != null)
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
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // This method will be called when the stone starts moving
    public void ZoomIn()
    {
        isZoomingIn = true;
        isZoomingOut = false;
    }

    // This method will be called when the stone stops moving
    public void ZoomOut()
    {
        isZoomingOut = true;
        isZoomingIn = false;
    }

    private void ZoomInToStone()
    {
        // Target the stone position, but keep the camera in top-down view
        Vector3 targetPosition = target.position;
        targetPosition.y = zoomInHeight; // Keep the camera at a set height

        // Smoothly move the camera towards the stone
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // If close enough to the target position, stop zooming in
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isZoomingIn = false;
        }
    }

    private void ZoomOutToBoard()
    {
        // Smoothly move the camera back to the starting position (showing the whole board)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, initialPosition, zoomSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // If close enough to the initial position, stop zooming out
        if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
        {
            isZoomingOut = false;
        }
    }
}
