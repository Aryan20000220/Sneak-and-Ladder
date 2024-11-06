using UnityEngine;

public class ScreenAdjuster : MonoBehaviour
{
    public RectTransform boardPanel;
    public RectTransform uiPanel;

    void Start()
    {  
        Debug.Log("ScreenAdjuster: Start called");
        AdjustLayout();
    }

    void Update()
    {
        if (Screen.orientation == ScreenOrientation.Landscape || Screen.orientation == ScreenOrientation.Portrait)
        {
            AdjustLayout();
        }
    }

    void AdjustLayout()
    {
        Debug.Log("ScreenAdjuster: Adjusting layout for " + Screen.orientation);
        
        if (Screen.width > Screen.height) // Landscape Mode
        {
            boardPanel.anchorMin = new Vector2(0, 0);
            boardPanel.anchorMax = new Vector2(0.7f, 1);
            boardPanel.offsetMin = Vector2.zero;
            boardPanel.offsetMax = Vector2.zero;

            uiPanel.anchorMin = new Vector2(0.7f, 0);
            uiPanel.anchorMax = new Vector2(1, 1);
            uiPanel.offsetMin = Vector2.zero;
            uiPanel.offsetMax = Vector2.zero;
        }
        else // Portrait Mode
        {
            boardPanel.anchorMin = new Vector2(0, 0.3f);
            boardPanel.anchorMax = new Vector2(1, 1);
            boardPanel.offsetMin = Vector2.zero;
            boardPanel.offsetMax = Vector2.zero;

            uiPanel.anchorMin = new Vector2(0, 0);
            uiPanel.anchorMax = new Vector2(1, 0.3f);
            uiPanel.offsetMin = Vector2.zero;
            uiPanel.offsetMax = Vector2.zero;
        }
    }
}
