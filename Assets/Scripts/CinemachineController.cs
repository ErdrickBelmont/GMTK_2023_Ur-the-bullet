using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public void Explode(){
        animator.Play("FarCamera");
    }

    public void ExitGun(){
        animator.Play("MainCamera");
    }

    public void Reset(){
        animator.Play("GunCamera");
    }
}
