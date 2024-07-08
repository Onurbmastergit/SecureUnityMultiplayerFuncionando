using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{
    #region Variables

    [Header("Buttons")]
    [SerializeField] GameObject soloButton;
    [SerializeField] GameObject coopButton;

    [Header("Panels")]
    [SerializeField] GameObject singleplayerPanel;
    [SerializeField] GameObject multiplayerPanel;

    [Header("Effects")]
    [SerializeField] GameObject soloButtonEffect;
    [SerializeField] GameObject coopButtonEffect;

    #endregion

    #region Functions

    public void SelectSolo()
    {
        singleplayerPanel.SetActive(true);
        multiplayerPanel.SetActive(false);

        soloButtonEffect.SetActive(true);
        coopButtonEffect.SetActive(false);
    }

    public void SelectCoop()
    {
        singleplayerPanel.SetActive(false);
        multiplayerPanel.SetActive(true);

        soloButtonEffect.SetActive(false);
        coopButtonEffect.SetActive(true);
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion
}
