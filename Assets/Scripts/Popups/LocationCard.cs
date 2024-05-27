using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationCard : MonoBehaviour
{
    #region Variables

    Location location;

    [SerializeField] TextMeshProUGUI locationName;
    [SerializeField] TextMeshProUGUI woodAmount;
    [SerializeField] TextMeshProUGUI stoneAmount;
    [SerializeField] TextMeshProUGUI metalAmount;

    #endregion

    #region Functions

    public void SelectLocation()
    {
        LevelManager.instance.woodGathered = location.Wood;
        LevelManager.instance.stoneGathered = location.Stone;
        LevelManager.instance.metalGathered = location.Metal;
    }

    public void Close()
    {
        GatherPopup.instance.Close();
    }

    #endregion

    #region Instatiation

    /// <summary>
    /// Add collection card.
    /// </summary>
    public static LocationCard Add(Transform _parent, Location _location)
    {
        LocationCard reference = Resources.Load<LocationCard>("Prefabs/Popups/LocationCard");
        LocationCard instance = Instantiate(reference, _parent);

        instance.location = _location;

        instance.locationName.text = _location.Title;
        instance.woodAmount.text = _location.Wood.ToString();
        instance.stoneAmount.text = _location.Stone.ToString();
        instance.metalAmount.text = _location.Metal.ToString();

        return instance;
    }

    #endregion
}
