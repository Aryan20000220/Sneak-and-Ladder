using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
//using SimpleJSON; // Optional, useful for parsing JSON

public class APIManager : MonoBehaviour
{
    // Base URL of your API
    private string baseUrl = "https://localhost:5001/api/user"; // Change the port to match your API port

    // Function to get all users and their scores from the API
    public void GetUsers()
    {
        StartCoroutine(GetUsersCoroutine());
    }

    private IEnumerator GetUsersCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text); // Print the JSON response

            // Optional: Use SimpleJSON to parse the response
            /*var jsonResponse = JSON.Parse(request.downloadHandler.text);
            foreach (var user in jsonResponse)
            {
                Debug.Log($"Username: {user.Value["username"]}, Score: {user.Value["score"]}");
            }*/
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }

    // Function to get a user by username
    public void GetUserByUsername(string username)
    {
        StartCoroutine(GetUserByUsernameCoroutine(username));
    }

    private IEnumerator GetUserByUsernameCoroutine(string username)
    {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl + "/" + username);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }

    // Function to send new user data to the API
    public void AddUser(string username, int score)
    {
        StartCoroutine(AddUserCoroutine(username, score));
    }

    private IEnumerator AddUserCoroutine(string username, int score)
    {
        User user = new User { Username = username, Score = score };
        string jsonData = JsonUtility.ToJson(user);

        UnityWebRequest request = new UnityWebRequest(baseUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("User added: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }

    [System.Serializable]
    public class User
    {
        public string Username;
        public int Score;
    }
}

