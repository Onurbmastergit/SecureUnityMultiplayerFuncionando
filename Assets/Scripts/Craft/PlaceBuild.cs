using System.Collections;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceBuild : NetworkBehaviour
{
    #region Variables

    public Craft craft;

    [SerializeField] GameObject barricadaHologram;
    [SerializeField] GameObject arameHologram;
    [SerializeField] GameObject minaHologram;
    [SerializeField] GameObject torretaHologram;

    [SerializeField] GameObject barricadaPrefab;
    [SerializeField] GameObject aramePrefab;
    [SerializeField] GameObject minaPrefab;
    [SerializeField] GameObject torretaPrefab;

    public Transform placeContainer;

    #endregion

    #region Initialization

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(base.IsOwner == false)return;
    }

    void Update()
    {
        CraftBuild();
    }

    #endregion

    #region Functions

    /// <summary>
    /// Update PlaceBuild Informations.
    /// </summary>
    public void UpdatePlaceBuild()
    {
        barricadaHologram.SetActive(craft.Title == "Barricada");
        arameHologram.SetActive(craft.Title == "Arame Farpado");
        minaHologram.SetActive(craft.Title == "Mina Terrestre");
        torretaHologram.SetActive(craft.Title == "Torreta");
    }

    [ObserversRpc(BufferLast = true)]
    void CraftBuild()
    {
        if (!base.IsOwner) return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mouseInWorld = Vector3.zero;

        if (Physics.Raycast(ray, out hit))
        {
            // Arredonda a posicao X e Z do ponto de colisao para o multiplo de 4 mais proximo
            mouseInWorld = new Vector3(Mathf.Round(hit.point.x / 4) * 4, 0, Mathf.Round(hit.point.z / 4) * 4);
            transform.position = mouseInWorld;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Instancia a Build selecionada no mouseInWorld position.
            InstantiateBuilds();

            // Atualiza nova quantidade de Recursos apos instancia
            LevelManager.instance.woodTotal -= craft.WoodCost;
            LevelManager.instance.stoneTotal -= craft.StoneCost;
            LevelManager.instance.metalTotal -= craft.MetalCost;

            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Instantiate Build in Mouse Position.
    /// </summary>
    [ServerRpc]
    void InstantiateBuilds()
    {
        if (craft.Id == 1)
        {
            GameObject barricadaInstance = Instantiate(barricadaPrefab, placeContainer);
            base.Spawn(barricadaInstance);

            barricadaInstance.transform.position = transform.position;
        }
        if (craft.Id == 2)
        {
            GameObject arameInstance = Instantiate(aramePrefab, placeContainer);
            base.Spawn(arameInstance);

            arameInstance.transform.position = transform.position;
        }
        if (craft.Id == 3)
        {
            GameObject minaInstance = Instantiate(minaPrefab, placeContainer);
            base.Spawn(minaInstance);

            minaInstance.transform.position = transform.position;
        }
        if (craft.Id == 4)
        {
            GameObject torretaInstance = Instantiate(torretaPrefab, placeContainer);
            base.Spawn(torretaInstance);

            torretaInstance.transform.position = transform.position;
        }
    }

    #endregion
}
