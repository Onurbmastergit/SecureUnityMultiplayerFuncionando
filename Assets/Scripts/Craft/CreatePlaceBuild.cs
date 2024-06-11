using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatePlaceBuild : MonoBehaviour
{
    #region Instatiation

    /// <summary>
    /// Add collection card inside parent.
    /// </summary>
    public static CreatePlaceBuild Create(Transform _parent, Craft _craft)
    {
        CreatePlaceBuild reference = Resources.Load<CreatePlaceBuild>("Prefabs/Craft/PlaceBuild");
        CreatePlaceBuild instance = Instantiate(reference, _parent);

        instance.transform.GetComponent<PlaceBuild>().craft = _craft;
        instance.transform.GetComponent<PlaceBuild>().placeContainer = _parent;

        return instance;
    }

    #endregion
}
