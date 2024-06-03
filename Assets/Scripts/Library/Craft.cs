using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Build", menuName = "Secure/Build", order = 1)]
public class Craft : ScriptableObject
{
    #region Variables

    /// <summary>
    /// Title of the craft.
    /// </summary>
    public string Title => title;
    [SerializeField] string title;

    /// <summary>
    /// Description of the craft.
    /// </summary>
    public string Description => description;
    [SerializeField] string description;

    /// <summary>
    /// Image of the craft.
    /// </summary>
    public Sprite Image => image;
    [SerializeField] Sprite image;

    /// <summary>
    /// Amount of wood to craft.
    /// </summary>
    public int WoodCost;

    /// <summary>
    /// Amount of stone to craft.
    /// </summary>
    public int StoneCost;

    /// <summary>
    /// Amount of metal to craft.
    /// </summary>
    public int MetalCost;

    /// <summary>
    /// Tecnology level to craft.
    /// </summary>
    public int TecnologyCost;

    /// <summary>
    /// Prefab of the craft.
    /// </summary>
    public GameObject Prefab;

    #endregion
}
