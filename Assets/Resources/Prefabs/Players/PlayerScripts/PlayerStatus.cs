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
        vidaAtual = vidaTotal;
        StartCoroutine("SetBarLife");
        
    }

    void Update()
    {
        fillAmount = (float)vidaAtual/vidaTotal;

        if (BarLifeStatus == null) return;
        BarLifeStatus.fillAmount = fillAmount;
    }

    IEnumerator SetBarLife()
    {
        while (BarLifeStatus == null)
        {
            // Aguarda um frame antes de verificar novamente
            yield return null;

            // Atualiza a referência do objeto
            BarLifeStatus = GameObject.FindWithTag("Health").GetComponent<Image>();
        }
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
            Debug.Log("Morreu");
        }
    }
}
