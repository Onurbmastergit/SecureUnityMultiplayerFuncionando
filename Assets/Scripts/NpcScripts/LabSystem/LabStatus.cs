using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class LabStatus : NetworkBehaviour
{
    public float vidaAtual = 1;
    public float vidaBase;
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
