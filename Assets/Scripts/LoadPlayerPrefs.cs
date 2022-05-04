using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class LoadPlayerPrefs : MonoBehaviour{

    public AudioMixer mixer;
    
    void Awake(){
        if(PlayerPrefs.HasKey("volume")){
            float wantedVolume = PlayerPrefs.GetFloat("volume", 1f);
            mixer.SetFloat("MusicVol", Mathf.Log10(wantedVolume) * 20);        }
    }
}