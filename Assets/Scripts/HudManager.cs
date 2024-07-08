using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    public static HudManager instance;

    public GameObject hudAlerta;
    public GameObject loginHud;
    public bool active = true;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (LevelManager.instance.currentHour.Value == 23 && active == true)
        {
            Invoke("AtivarAlerta", 1);
            active = false;
        }
        if (LevelManager.instance.currentHour.Value == 6)
        {
            active = true;
        }
    }

    void AtivarAlerta()
    {
        hudAlerta.SetActive(true);
    }

    public void DesativarAlerta()
    {
        hudAlerta.SetActive(false);
    }

    public void DesativarLogin()
    {
        loginHud.SetActive(false);
    }
}
