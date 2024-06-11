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

    #region Initialization

    void Start()
    {
        
    }

    #endregion

    #region Funtions

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion

    #region Instatiation

    /// <summary>
    /// Instantiate Endgame popup.
    /// </summary>
    public static EndgamePopup Show(Transform _parent, bool _victory)
    {
        EndgamePopup reference = Resources.Load<EndgamePopup>("Prefabs/Popups/EndgamePopup");
        EndgamePopup instance = Instantiate(reference, _parent);

        instance.victoryCard.SetActive(_victory);
        instance.defeatCard.SetActive(!_victory);

        return instance;
    }

    #endregion
}
