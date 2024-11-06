using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    public Text message;

    public void ShowWinMessage(string winner)
    {
        message.text = winner;
    }

    public void backButton()
    {
        SceneManager.LoadScene(1);
    }
}
