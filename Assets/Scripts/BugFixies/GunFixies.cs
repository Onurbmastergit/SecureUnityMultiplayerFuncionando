using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFixies : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(-160, -178, 259);
    }
}
