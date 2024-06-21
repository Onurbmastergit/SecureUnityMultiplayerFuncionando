using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    Transform laboratory;

    void Start()
    {
        laboratory = GameObject.FindWithTag("HouseDefender").transform;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GetComponentInParent<NavMeshEnemy>().point = collider.transform;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GetComponentInParent<NavMeshEnemy>().point = laboratory;
        }
    }
}
