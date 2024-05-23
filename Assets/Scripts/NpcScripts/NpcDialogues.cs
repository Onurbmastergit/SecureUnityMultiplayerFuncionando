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
    dialogueAlert = $"Ei sobrevivente , a horda esta vindo ao {SpawnSelection.instacia.spawnDirecao} fique atento a esta direção"; 
    StartCoroutine(TextEffect.instacia.ShowText(dialogueAlert)); 
   }
   public void ShowTextOutsinal()
   {
    string dialogueOutsinal = $"............................................................ SEM SINAL"; 
    StartCoroutine(TextEffect.instacia.ShowText(dialogueOutsinal)); 
   }
}
