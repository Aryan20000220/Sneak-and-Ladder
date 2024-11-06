using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class SnakesLaddersManager : MonoBehaviour
{
    private string apiUrl = "http://localhost:5000/generate"; // API endpoint
    private float refreshTime = 4f; // Time interval for refreshing data (every 4 seconds)

    // Data structure to hold snake and ladder information
    [System.Serializable]
    public class Snake
    {
        public int head;
        public int tail;
    }

    [System.Serializable]
    public class Ladder
    {
        public int bottom;
        public int top;
    }

    [System.Serializable]
    public class SnakesLaddersData
    {
        public List<Snake> snakes;
        public List<Ladder> ladders;
    }

    void Start()
    {
        // Start the coroutine to call the API every 4 seconds
        InvokeRepeating("FetchSnakesAndLadders", 0f, refreshTime);
    }

    void FetchSnakesAndLadders()
    {
        StartCoroutine(GetSnakesAndLadders());
    }

    IEnumerator GetSnakesAndLadders()
    {
        UnityWebRequest www = UnityWebRequest.Get(apiUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error); // Log error if the request fails
        }
        else
        {
            // Parse the response JSON
            string responseText = www.downloadHandler.text;
            SnakesLaddersData data = JsonUtility.FromJson<SnakesLaddersData>(responseText);

            // Update snakes and ladders on the board
            UpdateSnakesAndLadders(data);
        }
    }

    void UpdateSnakesAndLadders(SnakesLaddersData data)
    {
        // Clear current snakes and ladders from the board
        ClearSnakesAndLadders();

        // Add new snakes
        foreach (var snake in data.snakes)
        {
            Debug.Log($"Snake from {snake.head} to {snake.tail}");
            // Add code here to visually display the snake on the board
        }

        // Add new ladders
        foreach (var ladder in data.ladders)
        {
            Debug.Log($"Ladder from {ladder.bottom} to {ladder.top}");
            // Add code here to visually display the ladder on the board
        }

        // Optionally, highlight the most dangerous snake
    }

    void ClearSnakesAndLadders()
    {
        // Clear the existing snakes and ladders on the game board
    }
}
