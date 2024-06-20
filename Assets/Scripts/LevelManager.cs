using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishNet.Object;
using static UnityEngine.GraphicsBuffer;

public class LevelManager : NetworkBehaviour
{
    #region Variables

    // Contadores de Materiais no Menu.
    public float cureMeter = 0;
    public int woodTotal = 10;
    public int stoneTotal = 5;
    public int metalTotal = 0;
    public int tecnologyTotal = 0;
    public float scientistsHealth = 10;
    float tecno;

    // Materiais coletados pelo GatherPopup.
    public Location selectedLocation;

    // Construcao selecionada pelo CraftPopup.
    public Craft selectedCraft;

    // Botoes da HUD do jogo.
    [SerializeField] GameObject researchButton;
    [SerializeField] GameObject gatherButton;

    // Sistema de passagem de dias e horas dentro do jogo.
    public int currentDay = 1;
    public int currentHour = 6;
    int lastHour;
    public TextMeshProUGUI calendar;
    public TextMeshProUGUI hourText;
    public bool isDay = true;
    public bool dayStart;
    public bool nightStart;
    public bool cureResearch = true;
    float hourDurationDay = 3f; // Duracao de cada hora do dia em segundos.
    float hourDurationNight = 15f; // Duracao de cada hora da noite em segundos.
    float timer; // Tempo decorrido.
    float sunTimer;

    // Rotacao do sun durante as horas.
    public Transform sun;
    float sunRotationTimer;

    // HUD de Cure Research.
    public Image cureMeterHud;
    public TextMeshProUGUI porcentagemCure;

    //HUD de Tecnologia
    public Image tecnoCureMeterHud;
    public TextMeshProUGUI porcentagemTecno; 

    //Hud da base
    public GameObject hudLifeBase;
    public GameObject hudActionsBase;

    // Selection Effects
    public Canvas hudCure;
    public Canvas hudTecnology;
    public GameObject blackOutCure;
    public GameObject blackOutTecnology;    

    public int numSurvivorsGatherer;
    public int numSurvivorsScientist;

    public static LevelManager instance;

    // Endgame
    [SerializeField] GameObject EndgamePopup;

    #endregion

    #region Initialization

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public override void OnStartClient()
    {
        base.OnStartClient();

        lastHour = currentHour;
    }

    [Server]
    [ObserversRpc(BufferLast = true)]
    void Update()
    {
        if(base.ClientManager.Clients.Count == 0) return; 
        TimeSystem();
        UpdateHUD();
        EndgameSystem();
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

        SunRotation();
    }

    /// <summary>
    /// Gira o Sol em relacao ao horario do dia.
    /// </summary>
    [ObserversRpc(BufferLast = true)]
    void SunRotation()
    {
        sunTimer += Time.deltaTime;

        if (lastHour < currentHour)
        {
            lastHour = currentHour;
            sunTimer = 0;
        }

        sunRotationTimer = (isDay) ? sunTimer / hourDurationDay : sunTimer / hourDurationNight;

        float currentRotation = ((currentHour - 6) + sunRotationTimer) * 15;

        sun.rotation = Quaternion.Euler(currentRotation, -60, 0);
    }

    /// <summary>
    /// Trigga a cada hora.
    /// </summary>
    [ObserversRpc(BufferLast = true)]
    void HourTick()
    {
        ++currentHour;

        // Muda o contador de dias caso passe da meia-noite e reseta a hora.
        if (currentHour == 24)
        {
            ++currentDay;
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
            hudActionsBase.SetActive(true);
            hudLifeBase.SetActive(false);

            researchButton.SetActive(true);
            gatherButton.SetActive(true);

            AddMaterials();
        }

        if(cureResearch == true) CureProgression();    
        else TecnoProgression();
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
            hudActionsBase.SetActive(false);
            hudLifeBase.SetActive(true);

            researchButton.SetActive(false);
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
        cureMeter += 1.25f;
    }

    [ObserversRpc(BufferLast = true)]
    void TecnoProgression()
    {
        tecno += 10f;

        if (tecno >= 100f)
        {
            tecnologyTotal++;
            tecno = 0;
        }

        if (tecnologyTotal >= 3)
        {
            cureResearch = true;
        }
    }

    [ObserversRpc(BufferLast = true)]
    public void MudarPesquisa()
    {
        cureResearch = !cureResearch;
    }

    void UpdateHUD()
    {
        // Atualiza HUD % Cura
        float preenchimentoNormalizado = cureMeter / 100f;
        porcentagemCure.text = ((int)cureMeter) + "%".ToString();
        cureMeterHud.fillAmount = preenchimentoNormalizado;

        // Atualiza HUD % Tecnologia
        float fillAmount = tecno / 100f;
        porcentagemTecno.text = ((int)tecnologyTotal).ToString();
        tecnoCureMeterHud.fillAmount = fillAmount;

        if (cureResearch)
        {
            // Atualiza HUD Cura
            blackOutCure.SetActive(false);
            blackOutTecnology.SetActive(true);
            hudCure.sortingOrder = 1;
            hudTecnology.sortingOrder = 0;
        }
        else
        {
            // Atualiza HUD Tecnologia
            blackOutCure.SetActive(true);
            blackOutTecnology.SetActive(false);
            hudCure.sortingOrder = 0;
            hudTecnology.sortingOrder = 1;
        }
    }

    #endregion

    #region GatherMaterial System

    /// <summary>
    /// Adiciona os materias coletados no local selecionado para a base.
    /// </summary>
    void AddMaterials()
    {
        if (selectedLocation == null) return;

        woodTotal += selectedLocation.Wood;
        stoneTotal += selectedLocation.Stone;
        metalTotal += selectedLocation.Metal;
    }

    #endregion

    #region EndGame System

    /// <summary>
    /// Verifica se as condicoes de Fim de jogo ocorrerem.
    /// </summary>
    void EndgameSystem()
    {
        if (cureMeter >= 100)
        {
            Endgame();
            EndgamePopup.GetComponent<EndgamePopup>().UpdateEndgamePopup(true);
        }

        if (scientistsHealth <= 0)
        {
            Endgame();
            EndgamePopup.GetComponent<EndgamePopup>().UpdateEndgamePopup(false);
        }
    }

    /// <summary>
    /// Todas as funcoes que devem ocorrer ao Fim do jogo.
    /// </summary>
    void Endgame()
    {
        EndgamePopup.SetActive(true);
    }

    #endregion
}
