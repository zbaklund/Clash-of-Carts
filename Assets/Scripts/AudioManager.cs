using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private Sound currentSound;
    
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
            instance = this;
        else{
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start() {
        Play("World");
    }

    public void changeVolume(float value){
        currentSound.volume = value;
    }

    public Sound getCurrentSound(){
        return currentSound;
    }

    // use attach: FindObjectOfType<AudioManager>().Play("sound.name");
    public void Play (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        currentSound = s;

        if (s == null){
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();
    }
}
