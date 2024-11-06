using System.Collections;
using UnityEngine;
using UnityEngine.Networking; // Import UnityWebRequest

public class LoadGame : MonoBehaviour
{
    public static bool loadgame = false;
    public static int[] stepsdoneDB = new int[3];

    public void Start()
    {
        if (loadgame)
        {
            Debug.Log("function loaded");
            StartCoroutine(getData());
            loadgame = false;
        }
    }

    IEnumerator getData()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", DBManager.username);

        // Use UnityWebRequest instead of WWW
        using (UnityWebRequest webRequest = UnityWebRequest.Post("https://agrohub.ml/sqlconnect/webtest.php", form))
        {
            // Send the request and wait for a response
            yield return webRequest.SendWebRequest();

            // Check for errors
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string responseText = webRequest.downloadHandler.text;

                // Check if the first character is '0' (success case)
                if (responseText[0] == '0')
                {
                    Debug.Log("Positions Loaded");

                    // Split the response and parse the stepsdoneDB values
                    string[] data = responseText.Split('\t');
                    stepsdoneDB[0] = int.Parse(data[1]);
                    stepsdoneDB[1] = int.Parse(data[3]);
                    stepsdoneDB[2] = int.Parse(data[4]);

                    // Load the scene
                    UnityEngine.SceneManagement.SceneManager.LoadScene(2);

                    Debug.Log("Red: " + stepsdoneDB[0] + "\t" + "Green: " + stepsdoneDB[1] + "\t" + "Yellow: " + stepsdoneDB[2] + "\t");
                    GameManager.movetoLoadPosdone = true;
                }
                else
                {
                    Debug.Log("Login error: " + responseText);
                }
            }
        }
    }
}
