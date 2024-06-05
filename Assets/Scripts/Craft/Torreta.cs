using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    #region Instatiation

    /// <summary>
    /// Create Mina inside parent.
    /// </summary>
    public static Torreta Create(Transform _parent, Vector3 _position)
    {
        Torreta reference = Resources.Load<Torreta>("Prefabs/Craft/BuildTorreta");
        Torreta instance = Instantiate(reference, _parent);

        instance.transform.position = _position;

        return instance;
    }

    #endregion
    
}
