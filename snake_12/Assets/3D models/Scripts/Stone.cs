using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public Route route; // Reference to the route, assuming it is assigned in Inspector
    public List<Node> nodeList = new List<Node>();

    private int routePosition;
    private int stoneId;

    private float speed = 8f;
    private int stepsToMove;
    public int doneSteps;

    private bool isMoving;

    private float cTime = 0;
    private float amplitude = 0.5f;

    void Start()
    {
        // Ensure route is not null
        if (route == null)
        {
            Debug.LogError("Route reference is missing on " + gameObject.name);
            return;
        }

        // Initialize node list from the route
        foreach (Transform c in route.nodeList)
        {
            Node n = c.GetComponentInChildren<Node>();
            if (n != null)
            {
                nodeList.Add(n);
            }
            else
            {
                Debug.LogWarning("Node missing on " + c.name);
            }
        }

        if (nodeList.Count == 0)
        {
            Debug.LogError("Node list is empty for route on " + gameObject.name);
        }
    }

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }

        isMoving = true;

        // Remove the stone from the current node before moving
        nodeList[routePosition].RemoveStone(this);

        // Moving across the number of steps (dice roll)
        while (stepsToMove > 0)
        {
            routePosition++;

            if (routePosition >= nodeList.Count)
            {
                Debug.LogWarning("Reached the end of the node list.");
                yield break;
            }

            Vector3 nextPos = route.nodeList[routePosition].transform.position;
            Vector3 startPos = route.nodeList[routePosition - 1].transform.position;

            // Move in an arc from start to next position
            while (MoveInArc(startPos, nextPos, 4f))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            cTime = 0;
            stepsToMove--;
            doneSteps++;
        }

        yield return new WaitForSeconds(0.2f);

        // Check for connection node (snake or ladder)
        if (nodeList[routePosition].connectionNode != null)
        {
            int connectionNodeId = nodeList[routePosition].connectionNode.nodeId;
            Vector3 nextPos = nodeList[routePosition].connectionNode.transform.position;

            while (MoveToNext(nextPos))
            {
                yield return null;
            }

            doneSteps = connectionNodeId;
            routePosition = connectionNodeId;
        }

        // Add the stone back to the new node
        nodeList[routePosition].AddStone(this);

        // Check if the player has reached the end
        if (doneSteps == nodeList.Count - 1)
        {
            GameManager.instance.ReportWinner();
            yield break;
        }

        // Move to the next player's turn
        GameManager.instance.state = GameManager.States.SWITCH_PLAYER;

        // Finish moving
        isMoving = false;
        GameManager.instance.LaunchScene(doneSteps);

        // Zoom out after movement is complete
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.ZoomOut();
        }
    }

    bool MoveToNext(Vector3 nextPos)
    {
        // Smooth movement to the next position
        return nextPos != (transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime));
    }

    bool MoveInArc(Vector3 startPos, Vector3 nextPos, float _speed)
    {
        // Smooth arc movement
        cTime += _speed * Time.deltaTime;
        Vector3 myPos = Vector3.Lerp(startPos, nextPos, cTime);
        myPos.y += amplitude * Mathf.Sin(Mathf.Clamp01(cTime) * Mathf.PI);

        // Move smoothly to the target position
        return nextPos != (transform.position = Vector3.Lerp(transform.position, myPos, cTime));
    }

    public void MakeTurn(int diceNumber)
    {
        stepsToMove = diceNumber;

        if (doneSteps + stepsToMove < route.nodeList.Count)
        {
            StartCoroutine(Move());

            // Zoom in during movement
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.SetTarget(transform);
                cameraFollow.ZoomIn();
            }
        }
        else
        {
            Debug.LogWarning("Nodes exceeded for " + gameObject.name);
            GameManager.instance.state = GameManager.States.SWITCH_PLAYER;
        }
    }

    public int CurrentNode()
    {
        return doneSteps;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            Debug.Log("Collided with another stone.");
        }
    }
}
