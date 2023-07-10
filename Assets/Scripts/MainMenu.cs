using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int PlaySceneID;
    public Toggle ControlsToggle;
    private FadeController fader;
    public AudioSource mus;

    private void Start()
    {
        if (ControlsToggle != null)
        {
            Debug.Log(PlayerPrefs.GetInt("Controls"));
            ControlsToggle.isOn = PlayerPrefs.GetInt("Controls", 0) == 1;
        }
        fader = gameObject.AddComponent<FadeController>();
        fader.FadeIn(0.5f);
    }
    public void Play(){
        mus.Stop();
        fader.FadeOutToSceen(0.5f, PlaySceneID);
    }

    public void Quit(){
        Application.Quit();
    }

    public void SetControls()
    {
        if (ControlsToggle.isOn){
            PlayerPrefs.SetInt("Controls", 1);
        } else {
            PlayerPrefs.SetInt("Controls", 0);
        }
    }
}
