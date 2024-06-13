using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public int vidaTotal = 100;
    public int vidaAtual;

    public Image BarLifeStatus;
     public float fillAmount;
    
    void Start()
    {
        BarLifeStatus = GameObject.FindWithTag("Health").GetComponent<Image>();
        vidaAtual = vidaTotal;
        Invoke("SetBarLife",2f);
        
    }

    void Update()
    {
        fillAmount = (float)vidaAtual/vidaTotal;
        BarLifeStatus = GameObject.Find("HealthBar Fill").GetComponent<Image>();
        if (BarLifeStatus == null) return;
        BarLifeStatus.fillAmount = fillAmount;
    }

    void SetBarLife()
    {
        Debug.Log("PegouAVida");
        BarLifeStatus = GameObject.FindWithTag("Health").GetComponent<Image>();
    }
    
    public void ReceberDano(int valor, NetworkConnection connection)
    {
        vidaAtual -= valor;
        VerificarMorte();
    }
    void VerificarMorte()
    {
        if(vidaAtual == 0)
        {
           transform.GetComponent<PlayerMoves>().enabled = false;
           transform.GetComponent<PlayerAttacks>().enabled = false;
        }
    }
}
