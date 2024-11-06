using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{
 public static InfoBox instance;

 public Text InfoText;

 void Awake()
 {
     instance = this;
     InfoText.text = "";
 }

 public void ShowMessage(string _text)
 {
     InfoText.text = _text;
 }
}
