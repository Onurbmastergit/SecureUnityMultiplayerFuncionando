using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mina : MonoBehaviour
{
    #region Instatiation

    /// <summary>
    /// Create Mina inside parent.
    /// </summary>
    public static Mina Create(Transform _parent, Vector3 _position)
    {
        Mina reference = Resources.Load<Mina>("Prefabs/Craft/BuildMina");
        Mina instance = Instantiate(reference, _parent);

        instance.transform.position = _position;

        return instance;
    }

    #endregion
}
