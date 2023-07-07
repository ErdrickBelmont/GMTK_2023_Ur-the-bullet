using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] public float moveSpeed, horRotateSpeed, vertRotateSpeed, mouseMoveMultiplier;

    // Start is called before the first frame update
    void Start() {}

    void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed;

        float rotX = 0;
        float rotY = 0;

        //Horizontal rotation
        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
            rotY = -1;
            //transform.Rotate(0, -horRotateSpeed, 0, Space.World);
        }
        if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)){
            rotY =  1;
            //transform.Rotate(0,  horRotateSpeed, 0, Space.World);
        }

        //Vertical rotation
        if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)){
            rotX = 1;
            //transform.Rotate(-vertRotateSpeed, 0, 0);
        }
        if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)){
            rotX = -1;
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
}
