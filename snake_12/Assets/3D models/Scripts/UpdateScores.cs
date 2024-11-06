using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateScores : MonoBehaviour
{
    AudioSource _audioSource;
    //public AudioClip _audioClip;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Stone")
        {
            Debug.Log("Collider2");
           // _audioSource.clip = _audioClip;
            _audioSource.Play();
        }
    }


    /* void OnTriggerEnter(Collider other)
     {

         if (other.CompareTag("Stone"))
         {
             Renderer[] renderers = GetComponentsInChildren<Renderer>();
             foreach (Renderer r in renderers)
                 r.enabled = false;

             _audioSource.PlayOneShot(_audioClip);
             //Destroy(gameObject, _audioClip.length);

         }
     }

     


    /* private void Update()
     {
         if (DBManager.score == 11)
         {
             SceneManager.UnloadScene(8);
         }
     }*/
}
