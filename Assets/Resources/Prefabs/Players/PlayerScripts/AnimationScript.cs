using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class AnimationScript : NetworkBehaviour
{
    [SerializeField]
    private GameObject attackCollider;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayerMoves playerMoves;
    [SerializeField]
    private InputControllers inputControllers;
    private PlayerStatus status;
    public bool walk;
    public bool attack;
    public bool run;
    public bool talk;
    private int injuriedLayerIndex;
    public float smoothingSpeed;
    float healthPercentage;

    public override void OnStartClient()
    {
        base.OnStartClient();
         if(base.IsOwner == false) return;
        status = GetComponent<PlayerStatus>();
        animator = GetComponent<Animator>();
        inputControllers = GetComponent<InputControllers>();
        playerMoves = GetComponent<PlayerMoves>();
        injuriedLayerIndex = animator.GetLayerIndex("Injured");
        
    }
    void Update()
    {
         if(base.IsOwner == false) return;
        healthPercentage = status.vidaAtual/status.vidaTotal;
       
        animator.SetFloat("Life", status.vidaAtual);
        animator.SetBool("Pistol", InputControllers.pistol);
        animator.SetFloat("InputX",inputControllers.movimentoHorizontal);
        animator.SetFloat("InputY",inputControllers.movimentoVertical);
        walk = inputControllers.movimentoHorizontal > 0 || inputControllers.movimentoVertical > 0;    
        attack = inputControllers.Attack;
        //animator.SetBool("Attack" , attack);
        if(walk == false && inputControllers.build == true)
        {
             animator.SetBool("Build",true);
        }
        if(walk == false && talk == true)
        {
            animator.SetBool("Talk",true);
        }
        if(walk == true)
        {
             animator.SetBool("Build", false);
             animator.SetBool("Talk", false);
             talk = false;
        }
       Injuried();

        run = inputControllers.Run;
        animator.SetBool("Run", run);
       
         
    }   
    public void EnableCollison()
    {
        attackCollider.GetComponent<Collider>().enabled = true;
      
    }
    public void DisableCollison()
    {
        attackCollider.GetComponent<Collider>().enabled = false;
   
    }
    public void TalkAnim()
    {
        talk = true;
    }
    void Injuried()
    {
    float remainingHealth = status.vidaTotal - status.vidaAtual;

// Normaliza a diferença para obter a porcentagem de saúde que falta
    float healthPercentageRemaining = remainingHealth / status.vidaTotal;

// Calcula a força da camada diretamente com base na porcentagem de saúde que falta
    float targetStrength = healthPercentageRemaining;

// Obtém o peso atual da camada de injúria
    float currentInjuredLayerWeight = animator.GetLayerWeight(injuriedLayerIndex);

// Atualiza o peso da camada de injúria para a força alvo
    float smoothedWeight = Mathf.Lerp(currentInjuredLayerWeight, targetStrength, smoothingSpeed * Time.deltaTime);
    animator.SetLayerWeight(injuriedLayerIndex, smoothedWeight);
 }
 
}
