using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Arame : MonoBehaviour
{
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

    void OnTriggerStay(Collider collider)
    {
        if(collider.CompareTag("Zombie"))
        {
            collider.GetComponent<NavMeshAgent>().speed = 5 * 0.25f;
            //transform.GetComponent<ObjectStatus>().ReceberDano(5);
        }
        else
        {
            collider.GetComponent<NavMeshAgent>().speed = 5;    
        }
    }
}
