using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxColor : MonoBehaviour
{
     public  ObjectStatus status;
    Color32 corVermelho = new Color32(249, 6, 0, 255); // Red = F90600
    Color32 corVerde = new Color32(0, 249, 22, 255);   // Green = 00F916
    Color32 corAmarelo = new Color32(249, 192, 0, 255);// Yellow = F9C000

    void Start()
    {
        status = gameObject.GetComponent<ObjectStatus>();
    }
    public void ChangeColor()
    {
     if (status.vidaAtual <= status.vidaTotal * 0.2f) // Menos de 20% de vida (Vermelho)
        {
            status.vidaObject.color = corVermelho;
        }
        else if (status.vidaAtual >= status.vidaTotal * 0.8f) // Mais de 80% de vida (Verde)
        {
            status.vidaObject.color = corVerde;
        }
        else // Entre 20% e 80% de vida (Amarelo)
        {
            status.vidaObject.color = corAmarelo;
        }
    }
}
