using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPause;
    public GameObject move;
    public GameObject pauseMenuUI;
    PlayerControls controls;

    private void Start()
    {
        move.SetActive(!GameIsPause);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            move.gameObject.SetActive(false);
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        else
        {
            move.SetActive(!GameIsPause);
        }
    }

    public void PauseBTN()
    {
        
        if (GameIsPause)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void RestartLevel()
    {
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Start Scene");
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        SceneManager.LoadScene("End Scene");

    }

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.land.Esc.performed += ctx => PauseBTN();
    }

}
