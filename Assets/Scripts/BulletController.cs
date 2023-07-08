using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float moveSpeed, horRotateSpeed, vertRotateSpeed, mouseMoveMultiplier;
    [Space]
    [SerializeField] private SlowMoController slowMo;
    [SerializeField] private ParticleSystem yellowExplosionParticles, orangeExplosionParticles;
    [Space]
    [SerializeField] private CinemachineController cameraController;
    private float rotX = 0;
    private float rotY = 0;

    // Start is called before the first frame update
    void Start() { yellowExplosionParticles.Stop(); orangeExplosionParticles.Stop(); }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            slowMo.slowDown();
        }
        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)){
            slowMo.speedUp();
        }
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * slowMo.currentMultiplier;

        HandleRotation();
    }

    void HandleRotation(){
        
        //Horizontal rotation
        if(Input.GetKey(KeyCode.A)){
            rotY -= 1 * horRotateSpeed;
        }
        if(Input.GetKey(KeyCode.D)){
            rotY += 1 * horRotateSpeed;
        }

        //Vertical rotation
        if(Input.GetKey(KeyCode.W)){
            rotX -= 1 * vertRotateSpeed;
        }
        if(Input.GetKey(KeyCode.S)){
            rotX += 1 * vertRotateSpeed;
        }
        rotX = Mathf.Clamp(rotX, -45, 45);
        Quaternion goal = Quaternion.Euler(rotX, rotY, 0);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, goal, 2);
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Obstacle"){
            Explode();
        }
    }

    void Explode(){ 
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        slowMo.slowDown();

        cameraController.SwitchState();

        yellowExplosionParticles.Play();
        orangeExplosionParticles.Play();

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<BulletController>().enabled = false;
    }
}
