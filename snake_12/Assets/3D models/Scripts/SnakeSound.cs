using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSound : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip _audioClip;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stone")
        {
            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }
    }
}
