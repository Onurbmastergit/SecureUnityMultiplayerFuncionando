using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class LabStatus : NetworkBehaviour
{
    public float vidaBase;
    public Image lifeBaseStatus;
    public Image bgIconLifeStatus;
    public Image imageRachaduras;
    public Image neoBgLifeHud;
    public Image handsZombies;
    public VfxColor color;
    public GameObject labIcon;

    public GameObject[] sirenArray;

    public override void OnStartServer()
    {
        base.OnStartServer();
        LevelManager.instance.SetLabHealth(vidaBase);
        HudUpdate();
    }

    public void ReceberDano(int damage)
    {
        LevelManager.instance.SetLabHealth(LevelManager.instance.labHealth.Value - damage);

        HudUpdate();

        labIcon.GetComponent<Animator>().SetTrigger("Dano");

        foreach (GameObject siren in sirenArray)
        {
            siren.GetComponent<SirenScript>().SirenAlert();
        }
    }

    void HudUpdate()
    {
        float fillAmount = LevelManager.instance.labHealth.Value / vidaBase;
        float transparencia = LevelManager.instance.labHealth.Value / vidaBase;
        lifeBaseStatus.fillAmount = fillAmount;
        Color cor = imageRachaduras.color;
        Color corz = handsZombies.color;
        cor.a = 1f - transparencia;
        corz.a = 1f - transparencia;
        imageRachaduras.color = cor;
        handsZombies.color = corz;
        color.ChangeColor();
    }
}
