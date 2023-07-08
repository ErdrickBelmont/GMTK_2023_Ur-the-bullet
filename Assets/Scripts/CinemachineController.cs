using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool mainCamera = true;

    public void SwitchState(){
        if(mainCamera){
            animator.Play("FarCamera");
        } else {
            animator.Play("MainCamera");
        }

        mainCamera = !mainCamera;
    }
}
