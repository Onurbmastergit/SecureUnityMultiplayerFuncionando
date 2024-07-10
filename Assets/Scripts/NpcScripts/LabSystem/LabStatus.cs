using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class LabStatus : NetworkBehaviour
{
    [Header("Stats")]
    public float vidaBase;

    [Header("Hud")]
    public Image lifeBaseStatus;
    public Image bgIconLifeStatus;
    public Image imageRachaduras;
    public Image neoBgLifeHud;
    public Image handsZombies;
    public VfxColor color;
    public GameObject labIcon;

    [Header("Scientists")]
    [SerializeField] GameObject[] doctorArray;

    [Header("Sirens")]
    [SerializeField] GameObject[] sirenArray;

    [Header("Particles")]
    [SerializeField] GameObject particles;

    public override void OnStartServer()
    {
        base.OnStartServer();
        LevelManager.instance.SetLabHealth(vidaBase);
        HudUpdate();
    }

    void Update()
    {
        // No momento do Endgame o codigo segue.
        if (!LevelManager.instance.endgame.Value) return;

        // Caso o laboratorio esteja ainda vivo.
        if (LevelManager.instance.labHealth.Value > 0)
        {
            foreach (GameObject doctor in doctorArray)
            {
                doctor.GetComponent<Animator>().SetTrigger("Victory");
                particles.SetActive(true);
            }
        }
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

        if (LevelManager.instance.labHealth.Value <= 0)
        {
            transform.GetComponent<BoxCollider>().size = new Vector3(2,10,2);

            foreach (GameObject doctor in doctorArray)
            {
                doctor.GetComponent<Animator>().SetTrigger("Zombies");
            }
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
