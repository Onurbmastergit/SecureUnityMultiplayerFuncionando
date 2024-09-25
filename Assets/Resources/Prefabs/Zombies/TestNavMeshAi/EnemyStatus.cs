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
    public GameObject lifeBarObject;
    Animator animator;
    NavMeshAgent agent;
    public ParticleSystem particles;
    public bool tomouDano;
    public VfxColor color;
    Rigidbody rb;
    [SerializeField] GameObject minimapIcon;

    [SerializeField] bool isBulker;

    #endregion

    #region Initialization

    void Start()
    {
        if (isBulker) vidaBase *= base.ClientManager.Clients.Count;
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
            if (!isBulker)
            {
                animator.SetBool("Hit", true);
                agent.enabled = false;
                tomouDano = true;
            }
            vidaAtual -= dano;
        }
        particles.Play();
        color.ChangeColor();
        VerificarMorte();
    }

    [ObserversRpc(BufferLast = true)]
    void VerificarMorte()
    {
        if (vidaAtual <= 0)
        {
            vidaAtual = 0;
            agent.enabled = false;
            transform.GetComponent<BoxCollider>().enabled = false;
            lifeBarObject.SetActive(false);
            minimapIcon.SetActive(false);
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
