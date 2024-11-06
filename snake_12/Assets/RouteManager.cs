using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class RouteData
{
    public int start;
    public int end;
}

[System.Serializable]
public class BoardData
{
    public List<RouteData> snakes;
    public List<RouteData> ladders;
}

public class RouteManager : MonoBehaviour
{
    public Route route;
    
    private string apiUrl = "https://e4f2-124-43-79-210.ngrok-free.app/getCrossRoutes";

    void Start()
    {
        StartCoroutine(FetchRouteData());
    }

IEnumerator FetchRouteData()
{
    Debug.Log("Attempting to fetch route data...");
    UnityWebRequest request = UnityWebRequest.Get(apiUrl);

    // Optionally add authorization header if needed
    // request.SetRequestHeader("Authorization", "Bearer YOUR_TOKEN_HERE");

    yield return request.SendWebRequest();

    // Log the response code for additional debugging
    Debug.Log("Response Code: " + request.responseCode);

    if (request.result == UnityWebRequest.Result.Success)
    {
        string json = request.downloadHandler.text;
        Debug.Log("Received data: " + json);  // Log the JSON data for inspection
        BoardData boardData = JsonUtility.FromJson<BoardData>(json);
        ApplyRoutes(boardData);
    }
    else
    {
        Debug.LogError("Failed to fetch route data: " + request.error + "\nURL: " + apiUrl);
    }
}

    void ApplyRoutes(BoardData data)
    {
        foreach (var snake in data.snakes)
        {
            SetConnection(snake.start, snake.end, Color.red);
        }
        foreach (var ladder in data.ladders)
        {
            SetConnection(ladder.start, ladder.end, Color.blue);
        }
    }

    void SetConnection(int startId, int endId, Color color)
    {
        Node startNode = route.nodeList[startId].GetComponent<Node>();
        Node endNode = route.nodeList[endId].GetComponent<Node>();
        startNode.connectionNode = endNode;
    }
}
