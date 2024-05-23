using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject menuMain;
    public GameObject menuPause;
    public GameObject menuSettings;
    public GameObject menuCredits;
    public GameObject menuBuild;
    public InputControllers inputControllers;
    public GameObject menuRadio;

    public bool mainMenu;
    public bool isPaused;

    public static Menu instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        inputControllers = GameObject.FindWithTag("Player").GetComponent<InputControllers>();
        if (menuMain != null) menuMain.SetActive(true);
        if (menuPause != null) menuPause.SetActive(false);
        menuSettings.SetActive(false);
        menuCredits.SetActive(false);
        if (menuBuild != null) menuBuild.SetActive(false);
        if (menuRadio != null) menuRadio.SetActive(false);
    }

    private void Update()
    {
        if (!mainMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuBuild.activeSelf)
            {
                menuBuild.SetActive(false);
                return;
            }

            if (!isPaused)
            {
                isPaused = true;
                menuPause.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                isPaused = false;
                menuPause.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        if (menuCredits.activeSelf && Input.anyKey)
        {
            menuSettings.SetActive(true);
            menuCredits.SetActive(false);
        }

        if (menuRadio != null && menuRadio.activeSelf && Input.anyKey)
        {
            menuRadio.SetActive(false);
            LevelManager.instance.buildButton.SetActive(true);
        }
    }
    public void ButtonPlay()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void ButtonResume()
    {
        isPaused = false;
        menuPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ButtonMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ButtonSettings()
    {
        if (menuMain != null) menuMain.SetActive(false);
        if (menuPause != null) menuPause.SetActive(false);
        menuSettings.SetActive(true);
    }

    public void ButtonCredits()
    {
        menuSettings.SetActive(false);
        menuCredits.SetActive(true);
    }

    public void ButtonBack()
    {
        if (menuMain != null) menuMain.SetActive(true);
        if (menuPause != null) menuPause.SetActive(true);
        menuSettings.SetActive(false);
    }

    public void ButtonRadio()
    {
        menuRadio.SetActive(true);
        menuBuild.SetActive(false);
        LevelManager.instance.radioButton.SetActive(false);
        LevelManager.instance.buildButton.SetActive(false);
    }

    public void ButtonBuild()
    {
        inputControllers.build = true; 
        if (!menuBuild.activeSelf) menuBuild.SetActive(true);
        else menuBuild.SetActive(false);   
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
