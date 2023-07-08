using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int PlaySceneID;

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

}
