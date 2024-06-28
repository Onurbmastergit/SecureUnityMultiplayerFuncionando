using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatherCard : MonoBehaviour
{
    #region Variables

    [SerializeField] Location location;

    [SerializeField] TextMeshProUGUI locationName;
    [SerializeField] TextMeshProUGUI woodAmount;
    [SerializeField] TextMeshProUGUI stoneAmount;
    [SerializeField] TextMeshProUGUI metalAmount;

    [SerializeField] GameObject map;

    #endregion

    #region Initialization

    void Start()
    {
        locationName.text = location.Title;
        woodAmount.text = location.Wood.ToString();
        stoneAmount.text = location.Stone.ToString();
        metalAmount.text = location.Metal.ToString();
    }

    #endregion

    #region Functions

    /// <summary>
    /// Select and saves the location that the materials are going to be gathered.
    /// </summary>
    public void SelectLocation()
    {
        LevelManager.instance.selectedLocation = location;
    }

    /// <summary>
    /// Destroys the GatherPopup.
    /// </summary>
    public void Close()
    {
        map.SetActive(false);
    }

    #endregion
}
