using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class Mina : NetworkBehaviour 
{
    #region Variables

    public GameObject explosioArea;

    #endregion

    #region Functions

    void OnTriggerEnter(Collider collider)
     {
        if(collider.CompareTag("Zombie")) explosioArea.SetActive(true);
     }

     public void DestroyMine()
     {
        Destroy(gameObject);
     }

    #endregion
}
