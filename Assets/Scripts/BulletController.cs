using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float moveSpeed, horRotateSpeed, vertRotateSpeed, mouseMoveMultiplier;
    [SerializeField] private SlowMoController slowMo;
    [SerializeField] private ParticleSystem yellowExplosionParticles, orangeExplosionParticles;

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
        float rotX = 0;
        float rotY = 0;

        //Horizontal rotation
        if(Input.GetKey(KeyCode.A)){
            rotY -= 1;
            //transform.Rotate(0, -horRotateSpeed, 0, Space.World);
        }
        if(Input.GetKey(KeyCode.D)){
            rotY +=  1;
        }

        //Vertical rotation
        if(Input.GetKey(KeyCode.W)){
            rotX += 1;
        }
        if(Input.GetKey(KeyCode.S)){
            rotX -= 1;
            //transform.Rotate( vertRotateSpeed, 0, 0);
        }

        //now for the mouse rotation. NOTE that rotY and rotX are swapped
        rotY += Input.GetAxis ("Mouse X") * horRotateSpeed * mouseMoveMultiplier;
        rotX += Input.GetAxis ("Mouse Y") * vertRotateSpeed * mouseMoveMultiplier;
 
        rotX = Mathf.Clamp(rotX, -1, 1);      
        rotY = Mathf.Clamp(rotY, -1, 1);

        rotX *= horRotateSpeed;
        rotY *= vertRotateSpeed;
        
        transform.Rotate(-rotX, 0, 0);
        transform.Rotate(0, rotY, 0, Space.World);
    }

    void OnCollisionEnter(Collision other){
        print("hello");
        if(other.gameObject.tag == "Obstacle"){
            Explode();
        }
    }

    void Explode(){ 
        slowMo.slowDown();

        yellowExplosionParticles.Play();
        orangeExplosionParticles.Play();

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<BulletController>().enabled = false;
    }
}
