using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking; // Import UnityWebRequest

public class GameSettings : MonoBehaviour
{
    public Toggle redHuman, redCPU;
    public Toggle greenHuman, greenCPU;
    public Toggle yellowHuman, yellowCPU;
    public Toggle blueHuman, blueCPU;

    public static bool loadgame = false;
    public int Reddone;
    public int Bluedone;
    public int Greendone;
    public int Yellowdone;

    public void ReadToggles()
    {
        if (redCPU.isOn)
        {
            SaveSettings.players[0] = "CPU";
        }
        else if (redHuman.isOn)
        {
            SaveSettings.players[0] = "HUMAN";
        }

        if (blueCPU.isOn)
        {
            SaveSettings.players[3] = "CPU";
        }
        else if (blueHuman.isOn)
        {
            SaveSettings.players[3] = "HUMAN";
        }

        if (greenCPU.isOn)
        {
            SaveSettings.players[1] = "CPU";
        }
        else if (greenHuman.isOn)
        {
            SaveSettings.players[1] = "HUMAN";
        }

        if (yellowCPU.isOn)
        {
            SaveSettings.players[2] = "CPU";
        }
        else if (yellowHuman.isOn)
        {
            SaveSettings.players[2] = "HUMAN";
        }
    }

    public void StartGame(int scene)
    {
        ReadToggles();
        // StartCoroutine(hidePanel(scene));
        SceneManager.LoadScene(scene);

        if (loadgame)
        {
            loadPositions();
        }
    }

    IEnumerator hidePanel(int scene)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(scene);
    }

    public void loadPositions()
    {
        if (loadgame)
        {
            Debug.Log("function loaded");
            StartCoroutine(getData());
        }
    }

    IEnumerator getData()
    {
        Debug.Log("DB Connected");
        WWWForm form = new WWWForm();
        form.AddField("Name", DBManager.username);

        // Use UnityWebRequest instead of WWW
        using (UnityWebRequest webRequest = UnityWebRequest.Post("https://agrohub.ml/sqlconnect/webtest.php", form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Logging error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Positions Loaded");
                string responseText = webRequest.downloadHandler.text;

                Reddone = int.Parse(responseText.Split('\t')[1]);
                Bluedone = int.Parse(responseText.Split('\t')[2]);
                Greendone = int.Parse(responseText.Split('\t')[3]);
                Yellowdone = int.Parse(responseText.Split('\t')[4]);

                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
        }

        loadgame = false;
    }
}

public static class SaveSettings
{
    public static string[] players = new string[4];
}
