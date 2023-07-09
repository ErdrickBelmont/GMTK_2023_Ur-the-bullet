using System.Collections;
using System.Collections.Generic;
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
    }
    public void ExitToMenu()
    {
        gameOver = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(MainMenuSceneID);
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
