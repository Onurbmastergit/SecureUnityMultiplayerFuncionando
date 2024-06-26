using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStatus : NetworkBehaviour
{
    #region Variables

    public int vidaAtual = 100;
    public int vidaBase = 100;

    public Image LifeBar;
    private Animator animator;
    private NavMeshAgent agent;
    public bool tomouDano;
    public VfxColor color;
    private Rigidbody rb;

    #endregion

    #region Initialization

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
        float fillAmount = (float)vidaAtual / vidaBase;
        LifeBar.fillAmount = fillAmount;
        animator.SetInteger("Death", vidaAtual);
        if (LevelManager.instance.currentHour.Value > 5)
        {
            vidaAtual = 0;
        }
    }

    #endregion

    #region Funtions

    [ObserversRpc(BufferLast = true)]
    public void ReceberDano(int dano)
    {
        if (vidaAtual > 0)
        {
            animator.SetBool("Hit", true);
            agent.enabled = false;
            tomouDano = true;
            vidaAtual -= dano;
        }
        color.ChangeColor();
        VerificarMorte();
    }

    [ServerRpc]
    void VerificarMorte()
    {
        if (vidaAtual <= 0)
        {
            vidaAtual = 0;
            Destroy(rb);
            agent.enabled = false;
            transform.GetComponent<BoxCollider>().enabled = false;
        }
    }

    [ObserversRpc(BufferLast = true)]
    public void Morte()
    {
        base.Despawn(gameObject);
    }

    public void DisableAnimation()
    {
        animator.SetBool("Hit", false);
        tomouDano = false;
        agent.enabled = true;
    }

    #endregion
}
