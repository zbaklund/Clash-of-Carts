using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

class SliderController : MonoBehaviour
{
    public AudioSource audio;
    private float musicVolume = 0.6f;
    public Slider slider;

    private void Start() {
        audio.Play();
    }

    public void changeVolume(){
        audio.volume = musicVolume;
    }

    public void updateVolume(){
        musicVolume = slider.value;
    }

}