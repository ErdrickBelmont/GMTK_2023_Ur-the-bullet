using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    [SerializeField] private AudioSource wwIntro, bdIntro, wwLoop, bdLoop;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayLoop());
    }

    private IEnumerator PlayLoop(){
        yield return new WaitForSeconds(bdIntro.clip.length);
        wwLoop.Play();
        bdLoop.Play();
    }
}
