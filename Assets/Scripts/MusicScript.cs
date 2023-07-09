using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    [SerializeField] private AudioSource wwIntro, bdIntro, wwLoop, bdLoop;
    public bool isDead = false;

    public void StopMusic(){
        wwIntro.Stop();
        bdIntro.Stop();
        wwLoop.Stop();
        bdLoop.Stop();
        isDead = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayLoop());
    }

    private IEnumerator PlayLoop(){
        yield return new WaitForSeconds(bdIntro.clip.length);
        if(!isDead){
            wwLoop.Play();
            bdLoop.Play();
        }
    }
}
