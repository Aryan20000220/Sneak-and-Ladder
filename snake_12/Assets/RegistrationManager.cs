using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class RegistrationManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;

    public void RegisterPlayer()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // Send this data to the server for registration
        StartCoroutine(Register(username, password));
    }

    IEnumerator Register(string username, string password)
    {
        // Create a form to send to the API
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/register", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            Debug.Log("Player registered successfully!");
        }
    }
}
