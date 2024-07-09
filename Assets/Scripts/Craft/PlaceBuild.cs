using System.Collections;
using FishNet.Object;
using FishNet.Connection;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceBuild : NetworkBehaviour
{
    #region Variables

    public Craft craft;
    bool isColliding;

    [Header("Holograms")]
    [SerializeField] GameObject barricadaHologram;
    [SerializeField] GameObject arameHologram;
    [SerializeField] GameObject minaHologram;
    [SerializeField] GameObject torretaHologram;

    [Header("Crafts")]
    [SerializeField] GameObject barricadaPrefab;
    [SerializeField] GameObject aramePrefab;
    [SerializeField] GameObject minaPrefab;
    [SerializeField] GameObject torretaPrefab;

    [Header("Materials")]
    [SerializeField] Material validPlace;
    [SerializeField] Material unvalidPlace;

    Vector3 mouseInWorld = Vector3.zero;

    Transform placeContainer;

    #endregion

    #region Initialization

    public override void OnStartClient()
    {
        base.OnStartClient();

        // Pode ser que não seja o client HOST não esteja craftando, sendo assim, o client puro pode continuar a ação
        // o craft acontece no servidor, mas o HOST (que é o server) não vê o craft acontecendo. O mesmo serve pro client
        if (base.IsOwner == false) return;

        placeContainer = GameObject.FindWithTag("PlaceBuild").transform;
        UpdatePlaceBuild();
    }

    void Update()
    {
        if (base.IsOwner == false) return;

        CraftBuild();
    }

    #endregion

    #region Functions

    /// <summary>
    /// Update PlaceBuild Informations.
    /// </summary>
    public void UpdatePlaceBuild()
    {
        if (!craft) return;
        // Aqui está o problema de não aparecer o holograma no client. Tentei diversas formas, mas não deu...
        // Apenas o server tem a referência do que é para ser craftado, mas o client não tem
        // Faça o teste: barricadaHologram.SetActive(true);
        barricadaHologram.SetActive(craft.Id == 1);
        arameHologram.SetActive(craft.Id == 2);
        minaHologram.SetActive(craft.Id == 3);
        torretaHologram.SetActive(craft.Id == 4);
    }

    void CraftBuild()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // Arredonda a posição X e Z do ponto de colisão para o múltiplo de 4 mais próximo
            Vector3 mouseInWorld = new Vector3(Mathf.Round(hit.point.x / 4) * 4, 0, Mathf.Round(hit.point.z / 4) * 4);
            transform.position = mouseInWorld;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isColliding) return;
            // Adicionado uma função nova aqui, pois ela precisa ser rodada no server, e este update é no client
            Server_ActionCraft();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Destroy(gameObject);
            base.ServerManager.Despawn(gameObject);
        }
    }

    [ServerRpc]
    void Server_ActionCraft()
    {
        // Instancia a Build selecionada no mouseInWorld position.
        InstantiateBuilds();

        // Atualiza nova quantidade de Recursos apos instancia
        LevelManager.instance.SetWoodTotal(LevelManager.instance.woodTotal.Value - craft.WoodCost);
        LevelManager.instance.SetStoneTotal(LevelManager.instance.stoneTotal.Value - craft.StoneCost);
        LevelManager.instance.SetMetalTotal(LevelManager.instance.metalTotal.Value - craft.MetalCost);

        base.ServerManager.Despawn(gameObject); // Lembrar sempre de despawnar no server invés de Destroy(gameObject)
    }

    /// <summary>
    /// Instantiate Build in Mouse Position.
    /// </summary>
    void InstantiateBuilds()
    {
        GameObject instance;

        if (craft.Id == 1) instance = Instantiate(barricadaPrefab, placeContainer);
        else if (craft.Id == 2) instance = Instantiate(aramePrefab, placeContainer);
        else if (craft.Id == 3) instance = Instantiate(minaPrefab, placeContainer);
        else instance = Instantiate(torretaPrefab, placeContainer);

        base.Spawn(instance);
        instance.transform.position = transform.position;
    }

    void OnTriggerStay(Collider collider)
    {
        Debug.Log(isColliding);
        if (collider.CompareTag("TriggerIgnore")) return;
        isColliding = true;
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log(isColliding);
        if (collider.CompareTag("TriggerIgnore")) return;
        isColliding = false;
    }

    #endregion
}