using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishNet.Object;

public class LevelManager : NetworkBehaviour
{
    #region Variables

    // Contadores de Materiais no Menu.
    public float cureMeter = 0;
    public int woodTotal = 10;
    public int stoneTotal = 5;
    public int metalTotal = 0;
    public int tecnologyTotal = 1;

    // Materiais coletados pelo GatherPopup.
    public Location selectedLocation;

    // Construcao selecionada pelo CraftPopup.
    public Craft selectedCraft;

    // Botoes da HUD do jogo.
    public GameObject buildButton;
    public GameObject radioButton;
    public GameObject gatherButton;

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

    #endregion

    #region Initialization

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
    }

    [Server]
    void Update()
    {
        if(base.ClientManager.Clients.Count == 0) return; 
        TimeSystem();
    }

    #endregion

    #region TimePassing System

    /// <summary>
    /// Comanda toda a passagem de Tempo dentro do jogo.
    /// </summary>
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

    /// <summary>
    /// Gira o Sol em relacao ao horario do dia.
    /// </summary>
    void SunRotation()
    {
        sunRotationTimer = (isDay) ? sunRotationTimer + Time.deltaTime : sunRotationTimer + Time.deltaTime / 4;
        float currentRotation = sunRotationSpeed * sunRotationTimer;
        sun.rotation = Quaternion.Euler(currentRotation, -60, 0);
    }

    /// <summary>
    /// Trigga a cada hora.
    /// </summary>
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

    /// <summary>
    /// Trigga a cada hora se for dia.
    /// </summary>
    [ObserversRpc(BufferLast = true)]
    void DayHourTick()
    {
        // Trigga apenas na primeira hora do dia.
        if (!dayStart)
        {
            dayStart = true;
            nightStart = false;

            buildButton.SetActive(true);
            radioButton.SetActive(true);
            gatherButton.SetActive(true);

            AddMaterials();
        }
        CureProgression();
    }

    /// <summary>
    /// Trigga a cada hora se for noite.
    /// </summary>
    [ObserversRpc(BufferLast = true)]
    void NightHourTick()
    {
        // Trigga apenas na primeira hora da noite.
        if (!nightStart)
        {
            nightStart = true;
            dayStart = false;

            radioButton.SetActive(false);
            buildButton.SetActive(false);
            gatherButton.SetActive(false);
        }
    }

    #endregion

    #region CureResearch System

    /// <summary>
    /// Progressao da barra de Cura Research.
    /// </summary>
    [ObserversRpc(BufferLast = true)]
    void CureProgression()
    {
        cureMeter += 0.5f;
        float preenchimentoNormalizado = cureMeter / 100f;
        cureMeterHud.fillAmount = preenchimentoNormalizado;
    }

    #endregion

    #region GatherMaterial System

    /// <summary>
    /// Adiciona os materias coletados no local selecionado para a base.
    /// </summary>
    void AddMaterials()
    {
        woodTotal += selectedLocation.Wood;
        stoneTotal += selectedLocation.Stone;
        metalTotal += selectedLocation.Metal;
    }

    #endregion
}
