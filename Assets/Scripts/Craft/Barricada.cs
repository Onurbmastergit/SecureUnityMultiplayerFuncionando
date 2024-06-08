using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class Barricada : NetworkBehaviour
{
    #region Instatiation

    /// <summary>
    /// Create Barricada inside parent.
    /// </summary>
    public static Barricada Create(Transform _parent, Vector3 _position)
    {
        Barricada reference = Resources.Load<Barricada>("Prefabs/Craft/BuildBarricada");
        Barricada instance = Instantiate(reference, _parent);

        instance.transform.position = _position;

        return instance;
    }

    #endregion
}
