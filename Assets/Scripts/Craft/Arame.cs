using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arame : MonoBehaviour
{
    #region Instatiation

    /// <summary>
    /// Create Arame inside parent.
    /// </summary>
    public static Arame Create(Transform _parent, Vector3 _position)
    {
        Arame reference = Resources.Load<Arame>("Prefabs/Craft/BuildArame");
        Arame instance = Instantiate(reference, _parent);

        instance.transform.position = _position;

        return instance;
    }

    #endregion
}
