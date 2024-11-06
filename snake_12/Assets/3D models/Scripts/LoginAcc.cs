using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoginAcc : MonoBehaviour
{
    public InputField name;
    public InputField pass;
    public Button submit;

    public void CallLogin()
    {
        //StartCoroutine(Login());
        if ((name.text == "NIPUNAI7") && (pass.text == "NIPUNAI7"))
        {
            Debug.Log("Login Good");
            DBManager.username = name.text;
            //DBManager.score = int.Parse(www.text.Split('\t')[1]);
            DBManager.score = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        else
        {
            DialogBox.Instance.ShowQuestion("Your Login Credentials are Incorrect", () =>
            {
            }, () => { });
        }
    }

   /* IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", name.text);
        form.AddField("Pass", pass.text);
       // WWW www = new WWW("https://agrohub.ml/sqlconnect/login.php", form);
       // yield return www;

        if ((name.text == "NIPUNAI7") && (pass.text == "NIPUNAI7"))
        {
            Debug.Log("Login Good");
            DBManager.username = name.text;
            //DBManager.score = int.Parse(www.text.Split('\t')[1]);
            DBManager.score = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        else
        {
            DialogBox.Instance.ShowQuestion("Your Login Credentials are Incorrect", () =>
            {            
            }, () => { });
        }
    }*/
    public void VerifyInputs()
    {
        submit.interactable = (name.text.Length >= 8 && pass.text.Length >= 8);
    }

}
