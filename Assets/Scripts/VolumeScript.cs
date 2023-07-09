using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeScript : MonoBehaviour
{
    [SerializeField] private AudioMixer MasterMixer, MusicMixer, SFXMixer;
    public float masterVolume, baseMusicVolume, baseSfxVolume = 1f;
    private float finalMusicVolume, finalSfxVolume = 1f;

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
