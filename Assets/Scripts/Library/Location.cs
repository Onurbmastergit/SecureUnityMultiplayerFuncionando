using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Location", menuName = "Secure/Location", order = 0)]
public class Location : ScriptableObject
{
    #region Variables

    /// <summary>
    /// Title of the location.
    /// </summary>
    public string Title => title;
    [SerializeField] string title;

    /// <summary>
    /// Amount of wood at the location.
    /// </summary>
    public int Wood;

    /// <summary>
    /// Amount of stone at the location.
    /// </summary>
    public int Stone;

    /// <summary>
    /// Amount of metal at the location.
    /// </summary>
    public int Metal;

    #endregion

    #region Functions

    /// <summary>
    /// Set mount of wood at the location.
    /// </summary>
    public void SetWood(int _wood)
    {
        Wood = _wood;
    }

    /// <summary>
    /// Set mount of stone at the location.
    /// </summary>
    public void SetStone(int _stone)
    {
        Stone = _stone;
    }

    /// <summary>
    /// Set mount of metal at the location.
    /// </summary>
    public void SetMetal(int _metal)
    {
        Metal = _metal;
    }

    #endregion
}
