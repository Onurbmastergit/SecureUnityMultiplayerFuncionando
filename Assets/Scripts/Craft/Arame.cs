using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Arame : NetworkBehaviour
{
    public int zombiePassed;
    #region Instatiation

    /// <summary>
    /// Create Arame inside parent.
    /// </summary>
    public static Arame Create(Transform _parent, Vector3 _position)
    {
        Arame reference = Resources.Load<Arame>("Prefabs/Craft/BuildArame");
        Arame instance = Instantiate(reference, _parent);

        instance.transform.position = _position;

        return instance;
    }

    #endregion

    void Update()
    {
        if(zombiePassed >= 5)
        {
            Destroy(gameObject);
            Despawn(gameObject);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider.CompareTag("Zombie"))
        {
            collider.GetComponent<NavMeshAgent>().speed = 5 * 0.25f;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Zombie"))
        {
            zombiePassed++;
            collider.GetComponent<EnemyStatus>().ReceberDano(15);
        }
    }
}
