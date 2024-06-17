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
    public Image bgIconLifeStatus;
    public Image imageRachaduras;
    public Image handsZombies;
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
        float transparencia = vidaAtual / vidaBase;
        lifeBaseStatus.fillAmount = fillAmount;
        Color cor = imageRachaduras.color;
        Color corz = handsZombies.color;
        cor.a = 1f - transparencia;
        corz.a = 1f - transparencia;
        imageRachaduras.color = cor;
        handsZombies.color = corz;
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
