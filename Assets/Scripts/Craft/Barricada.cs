using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using Unity.VisualScripting;
using UnityEngine;

public class Barricada : NetworkBehaviour
{
    void Start()
    {
        base.Spawn(gameObject);
    }

}
