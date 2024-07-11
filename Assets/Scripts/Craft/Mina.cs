using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.AI;

public class Mina : NetworkBehaviour 
{
    #region Variables

    public GameObject explosioArea;

    #endregion

    #region Functions

    void ActivateExplosion()
    {
        explosioArea.SetActive(true);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Zombie"))
        {
            Invoke("ActivateExplosion", 1f);
            collider.GetComponent<NavMeshAgent>().speed *= 0.25f;
        }
    }

    public void DestroyMine()
    {
        Destroy(gameObject);
    }

    #endregion
}
