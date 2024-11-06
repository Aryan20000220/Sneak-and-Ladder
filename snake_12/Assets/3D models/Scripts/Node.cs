using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{

    public int nodeId;
    public Text Numbers;
    public Node connectionNode;
    public int score;

    List<Stone> stoneList = new List<Stone>();

    public void SetNodeId(int _nodeId)
    {
        nodeId = _nodeId;
        if (Numbers!= null)
        {
            Numbers.text = nodeId.ToString();
            //Debug.Log(Numbers.text);
        }
    }

    void OnDrawGizmos()
    {
        if(connectionNode != null)
        {
            Color col = Color.white;

            col = (connectionNode.nodeId > nodeId) ? Color.blue : Color.red;

            Debug.DrawLine(transform.position,connectionNode.transform.position,col);
        }
    }

    public void AddStone(Stone st)
    {
        stoneList.Add(st);
        ReArrange();

    }

    public void RemoveStone(Stone st)
    {
        stoneList.Remove(st);
        ReArrange();

    }

    void ReArrange()
    {
        if(stoneList.Count>1)
        {
            int squareSize = Mathf.CeilToInt(Mathf.Sqrt(stoneList.Count));
            int stone= -1;
            for (int i = 0; i < squareSize; i++)
            {
                for (int j = 0; j < squareSize; j++)
                {
                    stone++;
                    if(stone>stoneList.Count-1)
                    {
                        break;
                    }

                    Vector3 newPos = transform.position+ new Vector3(-0.25f+i*0.5f,0,-0.25f+j*0.5f);
                    stoneList[stone].transform.position = newPos;
                }
            }   
        }
        else if(stoneList.Count == 1)
        {
            stoneList[0].transform.position = transform.position;
        }
    }

}
