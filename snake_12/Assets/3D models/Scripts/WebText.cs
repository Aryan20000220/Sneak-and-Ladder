using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebText : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        WWW request = new WWW("https://agrohub.ml/sqlconnect/webtest.php");
        yield return request;

        //Debug.Log(request.text);
        string[] webResults = request.text.Split('\t');
        foreach (string item in webResults)
        {
            Debug.Log(item);
        }
    }

   
}
