using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameLoad : MonoBehaviour
{
    public Text nearBoard;

    private void Start()
    {
        if (DBManager.LoggedIn)
        {
            nearBoard.text = "Welcome,\t" + DBManager.username;
        }
    }


}
