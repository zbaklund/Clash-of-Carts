using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

class SliderController : MonoBehaviour
{
    public Slider slider;
    public AudioManager audio;

    private void Start() {
        slider.value = audio.getCurrentSound().volume;
    }

    public void changeVolume(){
        audio.changeVolume(slider.value);
    }

}