using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftPopup : MonoBehaviour
{
    #region Variables

    [SerializeField] TextMeshProUGUI wood;
    [SerializeField] TextMeshProUGUI stone;
    [SerializeField] TextMeshProUGUI metal;
    [SerializeField] TextMeshProUGUI tecnology;

    #endregion

    #region Initialization

    void Start()
    {
        UpdateMaterials();
    }

    #endregion

    #region Functions

    /// <summary>
    /// Update amount of Materials in CraftPopup HUD.
    /// </summary>
    public void UpdateMaterials()
    {
        wood.text = LevelManager.instance.woodTotal.Value.ToString();
        stone.text = LevelManager.instance.stoneTotal.Value.ToString();
        metal.text = LevelManager.instance.metalTotal.Value.ToString();
        tecnology.text = LevelManager.instance.tecnologyTotal.Value.ToString();
    }

    /// <summary>
    /// Closes CraftPopup.
    /// </summary>
    public void Close()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
