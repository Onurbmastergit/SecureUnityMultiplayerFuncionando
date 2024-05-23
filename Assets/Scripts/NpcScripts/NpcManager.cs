using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NpcManager : MonoBehaviour
{
  public static NpcManager instacia;  
  public string[] nameNpc;
  int numberName;  
  public GameObject npcAlert;
  public GameObject outSinalTexture;
  public GameObject radioButton;
  void Awake()
  {
    instacia = this;
  }
  void Update()
  {
    if(LevelManager.instance.currentHour <= 20)
    {
      radioButton.SetActive(true);
    }
    else if(LevelManager.instance.currentHour > 20)
    {
      radioButton.SetActive(false);
    }
  }  
  public void Alert()
  {
    if(SpawnSelection.instacia.spawnDirecao != null)
    {
    npcAlert.SetActive(true);
    NpcDialogues.instacia.ShowTextAlert();
    RandomizarNome();
    outSinalTexture.SetActive(false);
    }     
    
  }
  void Outsinal()
  {
    TextEffect.instacia.nameNpcShow = "INTERFERENCIA";
    outSinalTexture.SetActive(true);
    npcAlert.SetActive(true);
    NpcDialogues.instacia.ShowTextOutsinal();
  }

  void SelecionarNome()
  {
    for(int i = 0; i < nameNpc.Length; i++)
    {
        if(i == numberName)
        {
           if(nameNpc[i] != TextEffect.instacia.nameNpcShow)
           {
            TextEffect.instacia.nameNpcShow = nameNpc[i];
           }
           else if(nameNpc[i] == TextEffect.instacia.nameNpcShow)
           {
            RandomizarNome();
           }
        }
    }
  }

  void RandomizarNome()
  {
    if(nameNpc != null)
    {
    numberName = UnityEngine.Random.Range(0, nameNpc.Length+1);
    SelecionarNome();
    }
    return;
  }
}
