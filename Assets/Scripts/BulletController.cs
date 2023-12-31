using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float moveSpeed, horRotateSpeed, vertRotateSpeed, mouseMoveMultiplier;
    private float bulletSensitivity;
    [SerializeField] private float fireDelay = 1.0f;
    [Space]
    [SerializeField] private SlowMoController slowMo;
    [SerializeField] private ParticleSystem yellowExplosionParticles, orangeExplosionParticles, flashParticles, muzzleParticles, bloodParticles;
    [Space]
    [SerializeField] private CinemachineController cameraController;
    [SerializeField] private GameObject gunObject;
    [SerializeField] public GameObject gameOverText, resetPrompt, winMenu;
    public PauseMenu pauseMenu;

    [Header("Audio")]
    [SerializeField] private MusicScript musicScript;
    [SerializeField] private AudioSource gunFireSfx, objectHitSound, loseSound, winSound;
    
    private bool inBarrel = true;
    private bool alive = true;
    private bool moving = false;

    private float rotX = 0;
    private float rotY = 0;
    private bool wasdControls;
    private bool slowMoEnabled = false;
    private bool gameWon = false;
    private FadeController fader;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.GetFloat("sensitivity", -1) == -1)
        {
            PlayerPrefs.SetFloat("sensitivity", 0.5f);
        }

        gunObject.transform.parent = null;
        rb = GetComponent<Rigidbody>();
        rotX = transform.localEulerAngles.x;
        rotY = transform.localEulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(WaitThenFire());
        fader = this.transform.gameObject.AddComponent<FadeController>();
        fader.FadeIn(0.5f);
        new WaitForSeconds(0.5f);
        slowMoEnabled = true;
    }

    void Update(){
        wasdControls = PlayerPrefs.GetInt("Controls", 0) == 1;
        bulletSensitivity = PlayerPrefs.GetFloat("sensitivity");

        
        if (Input.GetKeyDown(KeyCode.R)){
            fader.FadeOutToSceen(0.5f, SceneManager.GetActiveScene().buildIndex);
        }

        if(!inBarrel && alive && slowMoEnabled)
        {
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
        if(!moving || !alive) { return; }

        rb.velocity = transform.forward * moveSpeed * slowMo.currentMultiplier;

        if(!inBarrel){
            HandleRotation();
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
            rotY += Input.GetAxis("Mouse X") * vertRotateSpeed * (mouseMoveMultiplier * PlayerPrefs.GetFloat("sensitivity"));
            rotX -= Input.GetAxis("Mouse Y") * horRotateSpeed * (mouseMoveMultiplier * PlayerPrefs.GetFloat("sensitivity"));
        }

        //apply rotation
        //rotX = Mathf.Clamp(rotX, -135, 135);
        Quaternion goal = Quaternion.Euler(rotX, rotY, 0);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, goal, horRotateSpeed);
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Obstacle"){
            pauseMenu.gameOver = true;
            Explode();
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "BarrelExitTrigger"){
            inBarrel = false;
            cameraController.ExitGun();
        }

        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "FinalEnemy")
        {
            FindObjectOfType<PauseMenu>().enemiesHit += 1;
            bloodParticles.Play();

            Transform screamBox = other.transform.Find("ScreamBox");
            screamBox.parent = null;
            AudioSource[] screams = screamBox.GetComponents<AudioSource>();
            if(screams.Length > 0){
                AudioSource targetScream = screams[Random.Range(0,screams.Length)];
                targetScream.Play();
            }

            if (other.gameObject.tag == "FinalEnemy")
            {
                winSound.Play();
                gameWon = true;
                Explode();
            }

            Destroy(other.gameObject);
        }
    }

    void Explode(){ 
        rb.isKinematic = true;
        slowMoEnabled = false;

        if (!gameWon)
        {
            
            objectHitSound.Play();
            loseSound.Play();

            StartCoroutine(DelayedGameOverText());
            StartCoroutine(DelayedResetPrompt());
        }
        else
        {
            winMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        musicScript.StopMusic();
        musicScript.isDead = true;

        slowMo.slowDown();

        cameraController.Explode();

        yellowExplosionParticles.Play();
        orangeExplosionParticles.Play();

        alive = false;

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

    public IEnumerator WaitThenFire(){
        yield return new WaitForSeconds(fireDelay);
        gunFireSfx.Play();
        flashParticles.Play();
        muzzleParticles.Play();
        moving = true;
    }

    public IEnumerator DelayedGameOverText(){
        yield return new WaitForSeconds(0f);
        gameOverText.SetActive(true);
    }

    public IEnumerator DelayedResetPrompt(){
        yield return new WaitForSeconds(3.37f);
        resetPrompt.SetActive(true);
    }
}
