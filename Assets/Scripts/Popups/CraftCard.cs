using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftCard : MonoBehaviour
{
    #region Variables

    Craft craft;

    [SerializeField] TextMeshProUGUI craftName;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI woodCost;
    [SerializeField] TextMeshProUGUI stoneCost;
    [SerializeField] TextMeshProUGUI metalCost;
    [SerializeField] TextMeshProUGUI tecnologyCost;

    #endregion

    #region Functions

    /// <summary>
    /// Destroys the GatherPopup.
    /// </summary>
    public void Close()
    {
        Destroy(GameObject.Find("CraftPopup(Clone)"));
    }

    #endregion

    #region Instatiation

    /// <summary>
    /// Add collection card inside parent.
    /// </summary>
    public static CraftCard Add(Transform _parent, Craft _craft)
    {
        CraftCard reference = Resources.Load<CraftCard>("Prefabs/Popups/CraftCard");
        CraftCard instance = Instantiate(reference, _parent);

        instance.craft = _craft;

        instance.craftName.text = _craft.Title;
        instance.description.text = _craft.Description;
        instance.woodCost.text = _craft.WoodCost.ToString();
        instance.stoneCost.text = _craft.StoneCost.ToString();
        instance.metalCost.text = _craft.MetalCost.ToString();
        instance.tecnologyCost.text = _craft.TecnologyCost.ToString();

        return instance;
    }

    #endregion
}
