using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionDialog : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogBox.Instance.ShowQuestion("Are you sure to Exit the game?", () =>
            {
                Application.Quit();               
            }, () => { });
        }
    }
}
