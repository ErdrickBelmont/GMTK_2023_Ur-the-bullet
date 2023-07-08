using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int PlaySceneID;
    public Toggle ControlsToggle;

    private void Start()
    {
        if (ControlsToggle != null)
        {
            Debug.Log(PlayerPrefs.GetInt("Controls"));
            ControlsToggle.isOn = PlayerPrefs.GetInt("Controls", 0) == 1;
        }
    }
    public void Play(){
        StartCoroutine(DelayedPlay());
    }

    public IEnumerator DelayedPlay(){
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(PlaySceneID);
    }

    // public void Tutorial(){
    //     SceneManager.LoadScene(TutorialSceneID);
    // }

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
        Debug.Log(PlayerPrefs.GetInt("Controls"));
    }
}
