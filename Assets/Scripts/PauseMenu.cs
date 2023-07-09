using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuHolder;
    [SerializeField] int MainMenuSceneID;
    [HideInInspector] public bool gameOver = false;
    public Toggle ControlsToggle;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI finalTimer;
    public TextMeshProUGUI finalEnemies;
    private float currentTime = 0;
    [HideInInspector] public int enemiesHit = 0;
    public int enemyCount = 0;
    public BulletController bc;

    private void Start()
    {
        if (ControlsToggle != null)
        {
            ControlsToggle.isOn = PlayerPrefs.GetInt("Controls", 0) == 1;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuHolder.activeSelf && !gameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenuHolder.SetActive(true);
            Time.timeScale = 0;
        } else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuHolder.activeSelf && !gameOver)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenuHolder.SetActive(false);
            Time.timeScale = 1;
        }
        currentTime += Time.deltaTime;
        timerText.text = ((int)(currentTime / 60)).ToString("00") + ":" + ((int)(currentTime % 60)).ToString("00");
        enemyCountText.text = enemiesHit + " / " + enemyCount + " targets eliminated";
        if (bc.winMenu.activeSelf)
        {
            timerText.text = "";
            enemyCountText.text = "";
            finalTimer.text = "Final Time: \n" + ((int)(currentTime / 60)).ToString("00") + ":" + ((int)(currentTime % 60)).ToString("00");
            finalEnemies.text = enemiesHit + " / " + enemyCount + " targets eliminated";
        }
    }
    public void ExitToMenu()
    {
        gameOver = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(MainMenuSceneID);
    }

    public void NextLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void SetControls()
    {
        if (ControlsToggle.isOn)
        {
            PlayerPrefs.SetInt("Controls", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Controls", 0);
        }
    }
}
