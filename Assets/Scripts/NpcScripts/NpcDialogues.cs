using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogues : MonoBehaviour
{
    public static NpcDialogues instacia;
    public string dialogueAlert;

    void Awake()
    {
        instacia = this;
    }

    public void ShowTextAlert()
    {
        dialogueAlert = $"Ei sobrevivente, uma horda esta se aproximando ao {SpawnSelection.instacia.spawnDirecao}. Durante a noite, fique atento a esta direção";
        StartCoroutine(TextEffect.instacia.ShowText(dialogueAlert));
    }

    public void ShowTextOutsinal()
    {
        string dialogueOutsinal = $"............................................................ SEM SINAL";
        StartCoroutine(TextEffect.instacia.ShowText(dialogueOutsinal));
    }
}
