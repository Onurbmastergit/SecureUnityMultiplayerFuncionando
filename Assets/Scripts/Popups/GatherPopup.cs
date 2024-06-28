using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherPopup : MonoBehaviour
{
    #region Variables

    [SerializeField] Transform locationsContainer;

    [SerializeField] Location[] locationList;

    #endregion

    #region Initialization

    void Start()
    {
        if (locationList != null) InstantiateLocationCards();
        else Debug.Log("O vetor locationList dentro do GatherPopup é null.");
    }

    #endregion

    #region Functions

    /// <summary>
    /// Instantiate LocationCards inside the GatherPopup.
    /// </summary>
    void InstantiateLocationCards()
    {
        foreach (var location in locationList)
        {
            //GatherCard.Add(locationsContainer, location);
        }
    }

    /// <summary>
    /// Destroys the Popup.
    /// </summary>
    public void Close()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Instatiation

    /// <summary>
    /// Instantiate Gather popup.
    /// </summary>
    public static GatherPopup Show(Transform _parent)
    {
        GatherPopup reference = Resources.Load<GatherPopup>("Prefabs/Popups/GatherPopup");
        GatherPopup instance = Instantiate(reference, _parent);

        return instance;
    }

    #endregion
}
