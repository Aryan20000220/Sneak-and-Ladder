using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class setVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetLevel(float sliderVal)
    {
        mixer.SetFloat("SliderVol", Mathf.Log10(sliderVal)*20);
    }
}
