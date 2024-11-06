using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountMenu : MonoBehaviour
{
    public InputField name;
    public InputField pass;
    public InputField pass2;
    public Button submit;

    public void CallReg()
    {
        StartCoroutine(Reg());
       
    }

    IEnumerator Reg()
    {
        if (pass.text != pass2.text)
        {
            Debug.Log("Passwords Doesn't match");
            DialogBox.Instance.ShowQuestion("Entered Passwords does not match.", () =>
            {
            }, () => { });
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("Name", name.text);
            form.AddField("Pass", pass.text);
            WWW www = new WWW("https://agrohub.ml/sqlconnect/reg.php", form);
            yield return www;

            if (www.text == "User adding success")
            {
                Debug.Log("User Registered");
                SceneManager.LoadScene(1);
                DBManager.username = name.text;
                DBManager.score = int.Parse(www.text.Split('\t')[1]);
            }
            else
            {
                DialogBox.Instance.ShowQuestion("Cannot Create User Account. Try different credentials or check your Network", () =>
                {
                }, () => { });
            }
        }
        
    }

    public void VerifyInputs()
    {
        submit.interactable = (name.text.Length >= 8 && pass.text.Length >= 8 && pass2.text.Length >=8);
    }
}
