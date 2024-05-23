using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public NavMeshAgent agent;
    public DetectionCollider buildOn;
    public Collider areaAttack;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
    }
    private void Update()
    {

        animator.SetBool("Attack", buildOn.Attack);

    }
    public void EnableCollider()
    {
        areaAttack.enabled = true;
    }
    public void DisableCollider()
    {
        areaAttack.enabled = false;
    }

}
