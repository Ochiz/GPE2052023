using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderForSFX : MonoBehaviour
{
    public AudioMixer SFXAudioMixer;
    public Slider SFXVolumeSlider;

    public void Start()
    {
        OnSFXVolumeChange();
    }

    public void OnSFXVolumeChange()
    {
        float newVolume = SFXVolumeSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;
        }
        SFXAudioMixer.SetFloat("sfxVolume", newVolume);
    }
}
