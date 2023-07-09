using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float moveSpeed, horRotateSpeed, vertRotateSpeed, mouseMoveMultiplier;
    [SerializeField] private float fireDelay = 1.0f;
    [Space]
    [SerializeField] private SlowMoController slowMo;
    [SerializeField] private ParticleSystem yellowExplosionParticles, orangeExplosionParticles;
    [Space]
    [SerializeField] private CinemachineController cameraController;
    [SerializeField] private GameObject gunObject;
    [SerializeField] private GameObject gameOverText, resetPrompt;
    
    private bool inBarrel = true;
    private bool alive = true;

    private float rotX = 0;
    private float rotY = 0;
    private bool wasdControls;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        gunObject.transform.parent = null;
        rb = GetComponent<Rigidbody>();
        rotX = transform.localEulerAngles.x;
        rotY = transform.localEulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        wasdControls = PlayerPrefs.GetInt("Controls", 0) == 1;
        yellowExplosionParticles.Stop();
        orangeExplosionParticles.Stop(); 
    }

    void Update(){
        if(fireDelay >= 0.0f) { fireDelay -= Time.deltaTime; }
    
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(!inBarrel && alive){
            if (wasdControls) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    slowMo.slowDown();
                }
                if (Input.GetKeyUp(KeyCode.Space)) {
                    slowMo.speedUp();
                }
            } else {
                if (Input.GetMouseButtonDown(0)) {
                    slowMo.slowDown();
                }
                if (Input.GetMouseButtonUp(0)) {
                    slowMo.speedUp();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if(fireDelay > 0.0f || !alive) { return; }

        if(alive){
            rb.velocity = transform.forward * moveSpeed * slowMo.currentMultiplier;

            if(!inBarrel){
                HandleRotation();
            }
        }
    }

    void HandleRotation(){
        //calc rotation
        if (wasdControls) {
            //Horizontal rotation
            if (Input.GetKey(KeyCode.A)) {
                rotY -= 1 * horRotateSpeed;
            }
            if (Input.GetKey(KeyCode.D)) {
                rotY += 1 * horRotateSpeed;
            }

            //Vertical rotation
            if (Input.GetKey(KeyCode.W)) {
                rotX -= 1 * vertRotateSpeed;
            }
            if (Input.GetKey(KeyCode.S)) {
                rotX += 1 * vertRotateSpeed;
            }
        }
        else {
            rotY += Input.GetAxis("Mouse X") * vertRotateSpeed * mouseMoveMultiplier;
            rotX -= Input.GetAxis("Mouse Y") * horRotateSpeed * mouseMoveMultiplier;
        }

        //apply rotation
        rotX = Mathf.Clamp(rotX, -135, 135);
        Quaternion goal = Quaternion.Euler(rotX, rotY, 0);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, goal, horRotateSpeed);
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Obstacle"){
            Explode();
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "BarrelExitTrigger"){
            inBarrel = false;
            cameraController.ExitGun();
        }
    }

    void Explode(){ 
        rb.isKinematic = true;

        StartCoroutine(DelayedGameOverText());
        StartCoroutine(DelayedResetPrompt());

        slowMo.slowDown();

        cameraController.Explode();

        yellowExplosionParticles.Play();
        orangeExplosionParticles.Play();

        alive = false;

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

    public IEnumerator DelayedGameOverText(){
        yield return new WaitForSeconds(1f);
        gameOverText.SetActive(true);
    }

    public IEnumerator DelayedResetPrompt(){
        yield return new WaitForSeconds(2f);
        resetPrompt.SetActive(true);
    }
}
