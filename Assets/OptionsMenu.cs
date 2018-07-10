using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour {
    public AudioMixer mixer;
    private bool muted = false;
    private float currentVolume = -60f;
    public void SetVolume(float volume)
    {
        currentVolume = volume;
        if(!muted)
            mixer.SetFloat("MasterVolume", volume);
    }

    public void EnableMusic(bool enabled)
    {
        if(!enabled)
            mixer.SetFloat("MasterVolume", -80f);
        else
            mixer.SetFloat("MasterVolume", currentVolume);
    }
}
