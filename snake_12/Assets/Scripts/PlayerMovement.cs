using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CameraSwitcher cameraSwitcher; // Reference to CameraSwitcher

    public void StartMovement()
    {
        // Call this method when the stone starts moving
        cameraSwitcher.StartZoomIn();
    }

    public void StopMovement()
    {
        // Call this method when the stone stops moving
        cameraSwitcher.StartZoomOut();
    }
}
