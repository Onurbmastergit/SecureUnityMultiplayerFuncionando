using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class LabStatus : NetworkBehaviour
{
    public float vidaAtual = 1;
    public float vidaBase;
    public Image lifeBaseStatus;
    public VfxColor color;
    void Awake()
    {
       vidaAtual = vidaBase;     
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        if(base.ClientManager.Clients.Count == 0)return;
    }
    void Update()
    {
        float fillAmount = vidaAtual/vidaBase;
        lifeBaseStatus.fillAmount = fillAmount;

        color.ChangeColor();
        
        if(vidaAtual <= 0)
        {
            LevelManager.instance.scientistsHealth = 0;
        }
    }
    public void ReceberDano(int damage)
    {
        vidaAtual -= damage;
    }


}
