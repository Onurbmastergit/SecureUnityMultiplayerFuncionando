using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStatus : NetworkBehaviour
{
    public int vidaAtual = 100;
    public int vidaBase = 100;

    public Image LifeBar;
    private Animator animator;
    private NavMeshAgent agent;
    public bool tomouDano;
    private Rigidbody rb;

    void Start() 
    {
        vidaAtual = vidaBase;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        vidaAtual = vidaBase;
        animator.SetInteger("Death", vidaAtual);
    }
    [ObserversRpc(BufferLast = true)]
    private void Update()
    {
        float fillAmount = (float)vidaAtual/vidaBase;
        LifeBar.fillAmount = fillAmount;
        animator.SetInteger("Death", vidaAtual);
        if(LevelManager.instance.dayStart)
        {
            vidaAtual = 0;
        }
    }
    [ObserversRpc(BufferLast = true)]
    public void ReceberDano(int dano) 
    {
        animator.SetTrigger("Hit");
        agent.enabled = false;
        tomouDano = true; 
        vidaAtual -= dano;
        VerificarMorte();
    }
    [ServerRpc]
    void VerificarMorte() 
    {
        if (vidaAtual <= 0) 
        {
            agent.enabled = false;
            transform.GetComponent<CapsuleCollider>().enabled = false;
            animator.SetInteger("Death",0);
        }
    }
    [ObserversRpc(BufferLast = true)]
    public void Morte()
    {
        base.Despawn(gameObject);
    }
    public void DisableAnimation() 
    {
        tomouDano = false;
        agent.enabled = true;
    }
}
