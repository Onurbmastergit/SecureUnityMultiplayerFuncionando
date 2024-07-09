using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    #region Variables

    public static Menu instance;

    public GameObject menuMain;
    public GameObject menuPause;
    public GameObject menuSettings;
    public GameObject menuCredits;
    public GameObject menuBuild;
    public InputControllers inputControllers;

    public bool mainMenu;
    public bool isPaused;

    GameObject networkManager;

    #endregion

    #region Initialization

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

        // Destroi o NetworkManager caso a cena seja MainMenu.
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            networkManager = GameObject.Find("NetworkManager");
            Destroy(networkManager);
        }
    }

    void Start()
    {
        if (GameObject.FindWithTag("Player") != null) inputControllers = GameObject.FindWithTag("Player").GetComponent<InputControllers>();
        if (menuMain != null) menuMain.SetActive(true);
        if (menuPause != null) menuPause.SetActive(false);
        menuSettings.SetActive(false);
        menuCredits.SetActive(false);
        if (menuBuild != null) menuBuild.SetActive(true);
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
                if (GameObject.Find("PlaceBuild") != null) return;

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
    }

    #endregion

    #region Functions

    /// <summary>
    /// Calls the Game Scene.
    /// </summary>
    public void ButtonPlay()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Closes the Menu Panel.
    /// </summary>
    public void ButtonResume()
    {
        isPaused = false;
        menuPause.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Calls the MainMenu Scene.
    /// </summary>
    public void ButtonMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Opens the Settings Panel.
    /// </summary>
    public void ButtonSettings()
    {
        if (menuMain != null) menuMain.SetActive(false);
        if (menuPause != null) menuPause.SetActive(false);
        menuSettings.SetActive(true);
    }

    /// <summary>
    /// Opens the Credits Panel.
    /// </summary>
    public void ButtonCredits()
    {
        menuSettings.SetActive(false);
        menuCredits.SetActive(true);
    }

    /// <summary>
    /// Closes Settings Panel.
    /// </summary>
    public void ButtonBack()
    {
        if (menuMain != null) menuMain.SetActive(true);
        if (menuPause != null) menuPause.SetActive(true);
        menuSettings.SetActive(false);
    }

    /// <summary>
    /// Opens or Closes Build Panel.
    /// </summary>
    public void ButtonBuild()
    {
        inputControllers.build = true; 
        if (!menuBuild.activeSelf) menuBuild.SetActive(true);
        else menuBuild.SetActive(false);   
    }

    /// <summary>
    /// Quits from Application.
    /// </summary>
    public void ButtonQuit()
    {
        Application.Quit();
    }

    #endregion
}
