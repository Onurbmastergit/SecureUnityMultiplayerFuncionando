using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftPopup : MonoBehaviour
{
    #region Variables

    [SerializeField] Transform craftContainer;

    [SerializeField] TextMeshProUGUI wood;
    [SerializeField] TextMeshProUGUI stone;
    [SerializeField] TextMeshProUGUI metal;
    [SerializeField] TextMeshProUGUI tecnology;

    [SerializeField] Craft[] craftList;

    #endregion

    #region Initialization

    void Start()
    {
        if (craftList != null) InstantiateCraftCards();
        else Debug.Log("O vetor locationList dentro do GatherPopup é null.");
    }

    #endregion

    #region Functions

    /// <summary>
    /// Instantiate CraftCards inside the CraftPopup.
    /// </summary>
    void InstantiateCraftCards()
    {
        foreach (var craft in craftList)
        {
            CraftCard.Add(craftContainer, craft);
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
    /// Instantiate Craft popup.
    /// </summary>
    public static CraftPopup Show(Transform _parent)
    {
        CraftPopup reference = Resources.Load<CraftPopup>("Prefabs/Popups/CraftPopup");
        CraftPopup instance = Instantiate(reference, _parent);

        instance.wood.text = LevelManager.instance.woodTotal.ToString();
        instance.stone.text = LevelManager.instance.stoneTotal.ToString();
        instance.metal.text = LevelManager.instance.metalTotal.ToString();
        instance.tecnology.text = LevelManager.instance.tecnologyTotal.ToString();

        return instance;
    }

    #endregion
}
