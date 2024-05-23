using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using FishNet.Object;

public class LevelManager : NetworkBehaviour
{
    // Contadores de Materiais no Menu.
    public float cureMeter = 0;
    public int woodTotal = 99;
    public int stoneTotal = 99;
    public int metalTotal = 99;
    public int tecnologyTotal = 1;
    public TextMeshProUGUI woodCounter;
    public TextMeshProUGUI stoneCounter;
    public TextMeshProUGUI metalCounter;
    public TextMeshProUGUI tecnologyCounter;

    // Botoes da HUD do jogo.
    public GameObject buildButton;
    public GameObject radioButton;

    // Sistema de passagem de dias e horas dentro do jogo.
    public int currentDay = 1;
    public int currentHour = 6;
    public TextMeshProUGUI calendar;
    public TextMeshProUGUI hourText;
    public bool isDay = true;
    public bool dayStart;
    public bool nightStart;
    float hourDurationDay = 7.5f; // Duracao de cada hora do dia em segundos.
    float hourDurationNight = 30.0f; // Duracao de cada hora da noite em segundos.
    float timer; // Tempo decorrido.

    // Rotacao do sun durante as horas.
    public Transform sun;
    float sunRotationSpeed = 360.0f / 180.0f;
    float sunRotationTimer;

    // HUD de Cure Research.
    public Image cureMeterHud;

    public int numSurvivorsGatherer;
    public int numSurvivorsScientist;

    public static LevelManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public override void OnStartServer()
    {
        base.OnStartServer();

        woodCounter.text = woodTotal.ToString();
        stoneCounter.text = stoneTotal.ToString();
        metalCounter.text = metalTotal.ToString();
        tecnologyCounter.text = "Lvl " + tecnologyTotal.ToString();
    }

    [Server]
    void Update()
    {
        if(base.ClientManager.Clients.Count == 0) return; 
        TimeSystem();
    }

    // Comanda toda a passagem de Tempo dentro do jogo.
    [ObserversRpc(BufferLast = true)]
    void TimeSystem()
    {
        
        // Conta os Segundos em float.
        timer += Time.deltaTime;
        SunRotation();

        isDay = (currentHour > 5) ? true : false;
        // Caso seja dia, o tempo passa mais rapido.
        if (isDay)
        {
            if (timer >= hourDurationDay)
            {
                HourTick();
                timer = 0;
            }
        }
        // Caso seja noite, o tempo passa mais devagar.
        else
        {
            if (timer >= hourDurationNight)
            {
                HourTick();
                timer = 0;
            }
        }
    }

    // Gira o Sol em relacao ao horario do dia.
    void SunRotation()
    {
        sunRotationTimer = (isDay) ? sunRotationTimer + Time.deltaTime : sunRotationTimer + Time.deltaTime / 4;
        float currentRotation = sunRotationSpeed * sunRotationTimer;
        sun.rotation = Quaternion.Euler(currentRotation, -60, 0);
    }

    // Trigga a cada hora.
    [ObserversRpc(BufferLast = true)]
    void HourTick()
    {
        currentHour++;

        // Muda o contador de dias caso passe da meia-noite e reseta a hora.
        if (currentHour == 24)
        {
            currentDay++;
            calendar.text = $"Day {currentDay}";
            currentHour = 0;
            isDay = false;
        }
        // Atualiza o horario na HUD.
        hourText.text = $"{currentHour}:00";

        if (isDay) DayHourTick();
        else NightHourTick();
    }

    // Trigga a cada hora se for dia.
    [ObserversRpc(BufferLast = true)]
    public void DayHourTick()
    {
        // Trigga apenas na primeira hora do dia.
        if (!dayStart)
        {
            dayStart = true;
            nightStart = false;
            buildButton.SetActive(true);
            radioButton.SetActive(true);
        }
        CureProgression();
    }

    // Trigga a cada hora se for noite.
    [ObserversRpc(BufferLast = true)]
    public void NightHourTick()
    {
        // Trigga apenas na primeira hora da noite.
        if (!nightStart)
        {
            nightStart = true;
            dayStart = false;
            radioButton.SetActive(false);
            buildButton.SetActive(false);
        }
    }

    // Progressao da barra de Cura Research.
    [ObserversRpc(BufferLast = true)]
    void CureProgression()
    {
        cureMeter += 0.5f;
        float preenchimentoNormalizado = cureMeter / 100f;
        cureMeterHud.fillAmount = preenchimentoNormalizado;
    }
}
