using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    /*public GameObject gameObject;
    private void Start()
    {
        gameObject.SetActive(false);
    }*/
    private void Awake()
    {
        gameObject.SetActive(true);
        DontDestroyOnLoad(transform.gameObject);
    }
}
