using System.Collections;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class LevelManager : NetworkBehaviour
{
    #region Variables

    // Contadores de Materiais no Menu.
    public void SetCureMeter(float value) => cureMeter.Value = value;
    public readonly SyncVar<float> cureMeter = new SyncVar<float>();

    public void SetTecnologyMeter(float value) => tecnologyMeter.Value = value;
    public readonly SyncVar<float> tecnologyMeter = new SyncVar<float>();

    public void SetWoodTotal(int value) => woodTotal.Value = value;
    public readonly SyncVar<int> woodTotal = new SyncVar<int>();

    public void SetStoneTotal(int value) => stoneTotal.Value = value;
    public readonly SyncVar<int> stoneTotal = new SyncVar<int>();

    public void SetMetalTotal(int value) => metalTotal.Value = value;
    public readonly SyncVar<int> metalTotal = new SyncVar<int>();

    public void SetTecnologyTotal(int value) => tecnologyTotal.Value = value;
    public readonly SyncVar<int> tecnologyTotal = new SyncVar<int>();

    public void SetLabHealth(float value) => labHealth.Value = value;
    public readonly SyncVar<float> labHealth = new SyncVar<float>();

    // Atributos do Player.
    public void SetPlayerDamage(int value) => playerDamage.Value = value;
    public readonly SyncVar<int> playerDamage = new SyncVar<int>();

    // Materiais coletados pelo GatherPopup.
    public void SetSelectedLocation(Location location) => selectedLocation.Value = location;
    public readonly SyncVar<Location> selectedLocation = new SyncVar<Location>();


    // Construcao selecionada pelo CraftPopup.
    public Craft selectedCraft;


    // Botoes da HUD do jogo.
    [SerializeField] GameObject researchButton;
    [SerializeField] GameObject mapButton;


    // Sistema de passagem de dias e horas dentro do jogo.
    public int currentDay = 1;

    public readonly SyncVar<int> currentHour = new SyncVar<int>();
    private void SetHour(int value) => currentHour.Value = value;

    public TextMeshProUGUI calendar;
    public TextMeshProUGUI hourText;
    public void SetIsDay(bool value) => isDay.Value = value;
    public readonly SyncVar<bool> isDay = new SyncVar<bool>();

    public bool dayStart;
    public bool nightStart;

    public void SetCureResearch(bool value) => cureResearch.Value = value;
    public readonly SyncVar<bool> cureResearch = new SyncVar<bool>();

    float hourDurationDay = 3f; // Duracao de cada hora do dia em segundos.
    float hourDurationNight = 15f; // Duracao de cada hora da noite em segundos.
    float timer; // Tempo decorrido.
    float sunTimer;

    // Rotacao do sun durante as horas.
    public Transform sun;

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
    public void SetEndgame(bool value) => endgame.Value = value;
    public readonly SyncVar<bool> endgame = new SyncVar<bool>();

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
    
    public override void OnStartServer()
    {
        base.OnStartServer();

        SetHour(6);

        SetCureMeter(0);

        SetWoodTotal(10);
        SetStoneTotal(10);
        SetMetalTotal(5);
        SetTecnologyTotal(0);

        SetPlayerDamage(10);

        SetIsDay(true);

        SetCureResearch(true);

        SetSelectedLocation(Resources.Load<Location>("Locations/01 Parque"));
    }

    void Update()
    {
        if (!base.IsServerInitialized) return;

        if (base.ClientManager.Clients.Count == 0) return;

        if (endgame.Value) return;

        TimeSystem();
        UpdateHUD();
        EndgameSystem();
    }

    #endregion

    #region TimePassing System

    /// <summary>
    /// Comanda toda a passagem de Tempo dentro do jogo.
    /// </summary>
    //[ObserversRpc(BufferLast = true)]
    [Server]
    void TimeSystem()
    {
        // Conta os Segundos em float.
        timer += Time.deltaTime;

        // Caso seja dia, o tempo passa mais rapido.
        if (isDay.Value)
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
        if (sunTimer < 54) sunTimer += Time.deltaTime;
        else if (currentHour.Value == 5) sunTimer += Time.deltaTime;
        else if (currentHour.Value == 6) sunTimer = 0;

        float anguloRotacao = 5f * sunTimer;

        sun.rotation = Quaternion.Euler(anguloRotacao, -60, 0);
    }

    /// <summary>
    /// Trigga a cada hora.
    /// </summary>
    //[ObserversRpc(BufferLast = true)]
    [Server]
    void HourTick()
    {
        SetHour(++currentHour.Value);

        // Muda o contador de dias caso passe da meia-noite e reseta a hora.
        if (currentHour.Value == 24)
        {
            ++currentDay;
            //calendar.text = $"Day {currentDay}";
            ShowSyncCalendar(currentDay); // Atualiza através do Observer, para todos
            SetHour(0);
            //isDay = false;
            //SyncDay(isDay);
        }
        // Atualiza o horario na HUD.
        //hourText.text = $"{currentHour.Value}:00";
        ShowSyncHour(currentHour.Value); // Atualiza através do Observer, para todos

        SetIsDay(currentHour.Value >= 6);

        if (isDay.Value) DayHourTick();
        else NightHourTick();
    }

    // Como o client não tem acesso às informações do que aconteceu no server, é necessário passar a
    // referência do server para o server, vai toma no cu Bruno.
    [ObserversRpc(BufferLast = true)]
    void ShowSyncCalendar( int currentDay ) => calendar.text = $"Day {currentDay}";

    [ObserversRpc(BufferLast = true)]
    void ShowSyncHour( int currentHour) => hourText.text = $"{currentHour}:00";

    //[ObserversRpc(BufferLast = true)]
    //void SyncDay(bool isDay) => this.isDay = isDay;

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

            mapButton.SetActive(true);
            if (tecnologyTotal.Value < 5) researchButton.SetActive(true);

            if (currentDay != 1) AddMaterials();
        }

        if (cureResearch.Value) CureProgression();    
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
            mapButton.SetActive(false);
        }
    }

    #endregion

    #region CureResearch System

    /// <summary>
    /// Progressao da barra de Cura Research.
    /// </summary>
    void CureProgression()
    {
        SetCureMeter(cureMeter.Value + 1.25f);

        // Impede o medidor de cura ultrapassar 100%
        if (cureMeter.Value > 100) SetCureMeter(100);
    }

    [ObserversRpc(BufferLast = true)]
    void TecnoProgression()
    {
        SetTecnologyMeter(tecnologyMeter.Value + 10f);

        if (tecnologyMeter.Value >= 100f)
        {
            SetTecnologyTotal(tecnologyTotal.Value + 1);
            SetTecnologyMeter(0f);

            if ((LevelManager.instance.cureMeter.Value / 10) < (LevelManager.instance.tecnologyTotal.Value + 1))
            {
                SetCureResearch(true);
            }

            if (tecnologyTotal.Value >= 5)
            {
                SetCureResearch(true);
                researchButton.SetActive(false);
            }
        }
    }

    [ObserversRpc(BufferLast = true)]
    public void MudarPesquisa()
    {
        if (tecnologyTotal.Value == 3 && cureMeter.Value < 40) return;
        else if (tecnologyTotal.Value == 4 && cureMeter.Value < 50) return;

        SetCureResearch(!cureResearch.Value);
    }

    [ObserversRpc(BufferLast = true)]
    void UpdateHUD()
    {
        // Atualiza HUD % Cura
        float preenchimentoNormalizado = cureMeter.Value / 100f;
        porcentagemCure.text = ((int)cureMeter.Value) + "%".ToString();
        cureMeterHud.fillAmount = preenchimentoNormalizado;

        // Atualiza HUD % Tecnologia
        float fillAmount = tecnologyMeter.Value / 100f;
        porcentagemTecno.text = ((int)tecnologyTotal.Value).ToString();
        tecnoCureMeterHud.fillAmount = fillAmount;

        if (cureResearch.Value)
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
        SetWoodTotal(woodTotal.Value + selectedLocation.Value.Wood);
        SetStoneTotal(stoneTotal.Value + selectedLocation.Value.Stone);
        SetMetalTotal(metalTotal.Value + selectedLocation.Value.Metal);
    }

    #endregion

    #region EndGame System

    /// <summary>
    /// Verifica se as condicoes de Fim de jogo ocorrerem.
    /// </summary>
    void EndgameSystem()
    {
        if (cureMeter.Value >= 100)
        {
            Endgame();
            EndgamePopup.GetComponent<EndgamePopup>().UpdateEndgamePopup(true);
        }

        if (labHealth.Value <= 0)
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
        SetEndgame(true);
        EndgamePopup.SetActive(true);
    }

    #endregion
}
