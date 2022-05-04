using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SetPlayerPrefs : MonoBehaviour{

    public Slider slider;
    public AudioMixer mixer;
    
    void Awake(){
        if(slider!=null && PlayerPrefs.HasKey("volume")){
            float wantedVolume = PlayerPrefs.GetFloat("volume",1f);
            slider.value = wantedVolume;
            mixer.SetFloat("MusicVol", Mathf.Log10(wantedVolume) * 20);        }
    }
}