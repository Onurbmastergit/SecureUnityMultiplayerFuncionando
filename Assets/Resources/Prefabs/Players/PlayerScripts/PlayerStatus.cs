using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public int vidaTotal = 100;
    public int vidaAtual;
    
    public Image BarLifeStatus;
    public Image veias;
    public Image blood;
    public DamageScript damageScript;
    public float fillAmount;
    public VfxColor color;
    
     void Start()
    {
        vidaAtual = vidaTotal;
    }

    
    void Update()
    {
        fillAmount = (float)vidaAtual/vidaTotal;
        if (BarLifeStatus == null) return;
        BarLifeStatus.fillAmount = fillAmount;
        Infection();
        Respawn();
    }
    
    public void ReceberDano(int valor, NetworkConnection connection)
    {
        Infection();
        damageScript.SpawnRandomBite();
        vidaAtual -= valor;
        color.ChangeColor();
        VerificarMorte();
    }

    void VerificarMorte()
    {
        if(vidaAtual == 0)
        {
            transform.GetComponent<PlayerMoves>().enabled = false;
            transform.GetComponent<PlayerAttacks>().enabled = false;
            transform.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void Respawn()
    {
        if (LevelManager.instance.currentHour.Value == 7)
        {
            vidaAtual = vidaTotal;
            color.ChangeColor();

            transform.GetComponent<PlayerMoves>().enabled = true;
            transform.GetComponent<PlayerAttacks>().enabled = true;
            transform.GetComponent<BoxCollider>().enabled = true;
        }
    }
    void Infection()
    {
        float transparencia = fillAmount;
        Color cor = veias.color;
        Color corz = blood.color;
        cor.a = 1f - transparencia;
        corz.a = 1f - transparencia;
        veias.color = cor;
        blood.color = corz;
    }
}
