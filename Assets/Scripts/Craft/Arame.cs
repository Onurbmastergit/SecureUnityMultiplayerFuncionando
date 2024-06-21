using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Arame : NetworkBehaviour
{
    #region Variables

    int zombiePassed;
    [SerializeField] int durability = 15;

    #endregion

    #region Funtions

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Zombie"))
        {
            zombiePassed++;
            collider.GetComponent<EnemyStatus>().ReceberDano(30);
            collider.GetComponent<NavMeshAgent>().speed = 5 * 0.25f;

            // Se auto-destroi apos certa quantidade de usos.
            if (zombiePassed >= durability)
            {
                Destroy(gameObject);
                Despawn(gameObject);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Zombie"))
        {
            collider.GetComponent<NavMeshAgent>().speed = 5;
        }
    }

    #endregion
}
