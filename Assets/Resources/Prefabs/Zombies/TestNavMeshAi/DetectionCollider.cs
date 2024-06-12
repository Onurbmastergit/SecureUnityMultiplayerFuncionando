using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class DetectionCollider : MonoBehaviour
{
    public bool Attack = false;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Build") || other.CompareTag("Player"))
        { 
            if(transform.parent.GetComponent<EnemyStatus>().vidaAtual > 0)
            {
                Attack = true;
            }
            else
            {
                Attack = false;
            }
            
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Build")|| collider.CompareTag("Player"))
        {
            Attack = false;
        }
    }
}
