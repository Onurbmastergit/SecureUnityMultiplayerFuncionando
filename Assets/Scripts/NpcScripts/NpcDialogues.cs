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

    public  void ShowTextOutsinal()
    {
    
        string dialogueOutsinal = $"Olá sobrevivente aqui está o relatório da coleta \n{LevelManager.instance.selectedLocation.Title}\nMadeira: +{LevelManager.instance.selectedLocation.Wood}\nPedra: +{LevelManager.instance.selectedLocation.Stone}\nMetal: +{LevelManager.instance.selectedLocation.Metal}";
        StartFunctions(dialogueOutsinal);
    }
    public void StartFunctions(string dialogueOutsinal)
    {
        StartCoroutine(TextEffect.instacia.ShowText(dialogueOutsinal));
    }
}
