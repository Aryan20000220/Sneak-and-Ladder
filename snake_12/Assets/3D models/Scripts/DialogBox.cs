using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogBox : MonoBehaviour
{
    public static DialogBox Instance { get; private set; }
    TextMeshProUGUI textMeshPro;
    Button yesBtn;
    Button noBtn;

    private void Awake()
    {
        Instance = this;
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        yesBtn = transform.Find("Yes").GetComponent<Button>();
        noBtn = transform.Find("No").GetComponent<Button>();

        Hide();
    }

    public void ShowQuestion(string text,Action yesAction, Action noAction)
    {
        gameObject.SetActive(true);

        textMeshPro.text = text;
        yesBtn.onClick.AddListener(() => {
            Hide();
            yesAction();       
        });
        noBtn.onClick.AddListener(() => {
            Hide();
            noAction();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
