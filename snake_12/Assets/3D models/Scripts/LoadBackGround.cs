using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Added to access SceneManager

public class LoadBackGround : MonoBehaviour
{
    public static string state;

    public void PanelVisibility()
    {
        Debug.Log("Going to Loop");
        Renderer panel = gameObject.GetComponent<Renderer>();
        panel.enabled = false;
        StartCoroutine(HidePanel(3));
    }

    IEnumerator HidePanel(int scene)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(scene);
        Debug.Log("Loop Done");
    }

    private void Start()
    {
        if (state == "paused")
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync(7);
            Debug.Log("Scene 7 resumed");
        }
        else
        {
            Debug.Log("Scene 7 loaded");
            SceneManager.LoadScene(3);
            SceneManager.UnloadSceneAsync(7);
        }
    }
}
