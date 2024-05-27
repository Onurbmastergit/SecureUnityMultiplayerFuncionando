using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatherPopup : MonoBehaviour
{
    #region Variables

    public static GatherPopup instance;

    [SerializeField] Transform locationsContainer;

    [SerializeField] Location[] locationList;

    #endregion

    #region Initialization

    void Start()
    {
        foreach (var location in locationList)
        {
            LocationCard.Add(locationsContainer, location);
        }
    }

    #endregion

    #region Functions

    public void Close()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Instatiation

    /// <summary>
    /// Instantiate collection popup.
    /// </summary>
    public static void Show(Location _location)
    {
        GameObject instance = Resources.Load<GameObject>("Prefabs/Popups/GatherPopup");
    }

    #endregion
}
