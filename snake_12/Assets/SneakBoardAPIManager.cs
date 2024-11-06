using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; // Make sure you include this if you're using UnityWebRequest
using System.Linq; // Add this for ToList()

public class SneakBoardAPIManager : MonoBehaviour
{
    private List<Player> players; // Declare a List of Player

    private void Start()
    {
        StartCoroutine(GetPlayerScores());
    }

    private IEnumerator GetPlayerScores() // Change IEnumerator<T> to IEnumerator
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost:5214/api/player"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                // Get the JSON result
                string jsonResponse = webRequest.downloadHandler.text;

                // Deserialize JSON into Player array
                Player[] playerArray = JsonUtility.FromJson<Player[]>(jsonResponse);

                // Convert array to list
                players = playerArray.ToList(); // Convert to List<Player>

                // Now you can use the players list as needed
                foreach (Player player in players)
                {
                    Debug.Log($"Player: {player.name}, Score: {player.score}"); // Make sure to use 'name' and 'score' correctly
                }
            }
        }
    }
}

[System.Serializable]
public class Player
{
    public int id;       // Player ID
    public string name;  // Player Name
    public int score;    // Player Score
}
