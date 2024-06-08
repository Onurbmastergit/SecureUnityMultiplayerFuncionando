using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class Torreta : NetworkBehaviour
{
    

    #region Instatiation

    /// <summary>
    /// Create Mina inside parent.
    /// </summary>
    public static Torreta Create(Transform _parent, Vector3 _position)
    {
        Torreta reference = Resources.Load<Torreta>("Prefabs/Craft/BuildTorreta");
        Torreta instance = Instantiate(reference, _parent);
        GameObject torreta = instance.gameObject;
        instance.transform.position = _position;
        

        return instance;
    }
    #endregion
}