using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderForMusic : MonoBehaviour
{
    public AudioMixer musicAudioMixer;
    public Slider musicVolumeSlider;

    public void Start()
    {
        OnMusicVolumeChange();
    }

    public void OnMusicVolumeChange()
    {
        float newVolume = musicVolumeSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;
        }
        musicAudioMixer.SetFloat("musicVolume", newVolume);
    }
}
