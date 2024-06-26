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

        Respawn();
    }
    
    public void ReceberDano(int valor, NetworkConnection connection)
    {
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
}
