using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer mixer;
    public void setLevel(float sliderValue)
    {
        mixer.SetFloat("MainVol",Mathf.Log10(sliderValue) * 20);
    }
}
