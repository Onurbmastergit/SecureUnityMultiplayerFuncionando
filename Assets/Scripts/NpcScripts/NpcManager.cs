using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class NpcManager : NetworkBehaviour
{
    #region Variables

    public static NpcManager instacia;
    public string[] nameNpc;
    int numberName;
    public GameObject npcAlert;
    public GameObject outSinalTexture;
    public bool showDirectionInMap;
    public GameObject radioButton;

    bool ligarBotao;

    #endregion

    #region Initialization

    void Awake()
    {
        instacia = this;
    }

    void Start()
    {
        ligarBotao = true;
    }

    void Update()
    {
        if (LevelManager.instance.isDay == true && ligarBotao)
        {
            radioButton.SetActive(true);
            ligarBotao = false;
        }
        else if (LevelManager.instance.isDay == false)
        {
            radioButton.SetActive(false);
            ligarBotao = true;
        }
    }

    #endregion

    #region Funtions

    public void Alert()
    {
        if (SpawnSelection.instacia.spawnDirecao != null)
        {
            npcAlert.SetActive(true);
            NpcDialogues.instacia.ShowTextAlert();
            outSinalTexture.SetActive(false);
            showDirectionInMap = true;
            radioButton.SetActive(false);
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
        for (int i = 0; i < nameNpc.Length; i++)
        {
            if (i == numberName)
            {
                if (nameNpc[i] != TextEffect.instacia.nameNpcShow)
                {
                    TextEffect.instacia.nameNpcShow = nameNpc[i];
                }
                else if (nameNpc[i] == TextEffect.instacia.nameNpcShow)
                {
                    RandomizarNome();
                }
            }
        }
    }

    void RandomizarNome()
    {
        if (nameNpc != null)
        {
            numberName = UnityEngine.Random.Range(0, nameNpc.Length + 1);
            SelecionarNome();
        }
        return;
    }

    #endregion
}
