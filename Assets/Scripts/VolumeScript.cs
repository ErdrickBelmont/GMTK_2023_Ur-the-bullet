using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    [SerializeField] private AudioMixer MasterMixer, MusicMixer, SFXMixer;
    [SerializeField] private Slider MasterSlider, MusicSlider, SFXSlider;
    public float masterVolume, baseMusicVolume, baseSfxVolume = 1f;
    private float finalMusicVolume, finalSfxVolume = 1f;

    private void Start(){
        float volume = 0f;
        MasterMixer.GetFloat("MasterVolume", out volume); MasterSlider.value = Mathf.Pow(10f, volume / 20f);
        MusicMixer.GetFloat("MusicVolume", out volume);   MusicSlider.value  = Mathf.Pow(10f, volume / 20f);
        SFXMixer.GetFloat("SFXVolume", out volume);       SFXSlider.value    = Mathf.Pow(10f, volume / 20f);
    }

    public void setMasterVolume(float value) 
    {
        masterVolume = value;
        finalMusicVolume = baseMusicVolume * masterVolume;
        finalSfxVolume = baseSfxVolume * masterVolume;

        MasterMixer.SetFloat("MasterVolume", Mathf.Log10(finalMusicVolume) * 20);
    }

    public void setMusicVolume(float value){
        baseMusicVolume = value;
        finalMusicVolume = baseMusicVolume * masterVolume;

        MasterMixer.SetFloat("MusicVolume", Mathf.Log10(finalMusicVolume) * 20);
    }

    public void setSFXVolume(float value){
        baseSfxVolume = value;
        finalSfxVolume = baseSfxVolume * masterVolume;

        MasterMixer.SetFloat("SfxVolume", Mathf.Log10(finalSfxVolume) * 20);
    }
}
