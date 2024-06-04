using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricada : MonoBehaviour
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
