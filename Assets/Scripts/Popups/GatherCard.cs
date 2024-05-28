using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatherCard : MonoBehaviour
{
    #region Variables

    Location location;

    [SerializeField] TextMeshProUGUI locationName;
    [SerializeField] TextMeshProUGUI woodAmount;
    [SerializeField] TextMeshProUGUI stoneAmount;
    [SerializeField] TextMeshProUGUI metalAmount;

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
        Destroy(GameObject.Find("GatherPopup(Clone)"));
    }

    #endregion

    #region Instatiation

    /// <summary>
    /// Add collection card inside parent.
    /// </summary>
    public static GatherCard Add(Transform _parent, Location _location)
    {
        GatherCard reference = Resources.Load<GatherCard>("Prefabs/Popups/GatherCard");
        GatherCard instance = Instantiate(reference, _parent);

        instance.location = _location;

        instance.locationName.text = _location.Title;
        instance.woodAmount.text = _location.Wood.ToString();
        instance.stoneAmount.text = _location.Stone.ToString();
        instance.metalAmount.text = _location.Metal.ToString();

        return instance;
    }

    #endregion
}
