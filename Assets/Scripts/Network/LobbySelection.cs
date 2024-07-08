using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySelection : MonoBehaviour
{
    #region Variables

    [Header("Buttons")]
    [SerializeField] GameObject entrarButton;
    [SerializeField] GameObject criarButton;

    [Header("Panels")]
    [SerializeField] GameObject clientPanel;
    [SerializeField] GameObject serverPanel;

    [Header("Effects")]
    [SerializeField] GameObject entrarEffect;
    [SerializeField] GameObject criarEffect;

    #endregion

    #region Functions

    public void SelectEntrar()
    {
        clientPanel.SetActive(true);
        serverPanel.SetActive(false);

        entrarEffect.SetActive(true);
        criarEffect.SetActive(false);
    }

    public void SelectCriar()
    {
        clientPanel.SetActive(false);
        serverPanel.SetActive(true);

        entrarEffect.SetActive(false);
        criarEffect.SetActive(true);
    }

    #endregion
}
