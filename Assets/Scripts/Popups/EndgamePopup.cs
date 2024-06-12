using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgamePopup : MonoBehaviour
{
    #region Variables

    [SerializeField] GameObject victoryCard;
    [SerializeField] GameObject defeatCard;

    #endregion

    #region Funtions

    /// <summary>
    /// Updates Endgame popup informations.
    /// </summary>
    public void UpdateEndgamePopup(bool _victory)
    {
        victoryCard.SetActive(_victory);
        defeatCard.SetActive(!_victory);
    }

    /// <summary>
    /// Return to MainMenu Scene.
    /// </summary>
    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion
}
