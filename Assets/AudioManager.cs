using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup mixer;
    public Sound[] Sounds;

    void Awake() {
        
        foreach (var s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = mixer;

        }
    }
    private void Start()
    {        
       Play("Background");
	}

    public void Play(string Name)
    {
        var s = Array.Find(Sounds, x => x.name == Name);
        if (s != null)
        {
            s.source.Play();
            Debug.Log(String.Format("Playing: {0}", s.source.isPlaying));
        }
    }
}
