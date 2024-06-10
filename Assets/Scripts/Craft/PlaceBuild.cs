using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceBuild : MonoBehaviour
{
    #region Variables

    Craft craft;

    [SerializeField] GameObject barricada;
    [SerializeField] GameObject arame;
    [SerializeField] GameObject mina;
    [SerializeField] GameObject torreta;

    Transform placeContainer;

    #endregion

    #region Initialization
    void Update()
    {
        CraftBuild();
    }

    #endregion

    #region Functions

    void CraftBuild()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mouseInWorld = Vector3.zero;

        if (Physics.Raycast(ray, out hit))
        {
            // Arredonda a posi��o X e Z do ponto de colis�o para o m�ltiplo de 4 mais pr�ximo
            mouseInWorld = new Vector3(Mathf.Round(hit.point.x / 4) * 4, 0, Mathf.Round(hit.point.z / 4) * 4);
            transform.position = mouseInWorld;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Instancia a build selecionada na posicao do mouse
            if (craft.Id == 1) CreateBarricada(mouseInWorld);
            else if (craft.Id == 2) CreateArame(mouseInWorld);
            else if (craft.Id == 3) CreateMina(mouseInWorld);
            else if (craft.Id == 4) CreateTorreta(mouseInWorld);

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
    /// Instantiate prefab Barricada inside parent at position.
    /// </summary>
    void CreateBarricada(Vector3 position)
    {
        GameObject reference = Resources.Load<GameObject>("Prefabs/Craft/BuildBarricada");
        GameObject instance = Instantiate(reference, placeContainer);

        instance.transform.position = position;
    }

    /// <summary>
    /// Instantiate prefab Arame inside parent at position.
    /// </summary>
    void CreateArame(Vector3 position)
    {
        GameObject reference = Resources.Load<GameObject>("Prefabs/Craft/BuildArame");
        GameObject instance = Instantiate(reference, placeContainer);

        instance.transform.position = position;
    }

    /// <summary>
    /// Instantiate prefab Mina inside parent at position.
    /// </summary>
    void CreateMina(Vector3 position)
    {
        GameObject reference = Resources.Load<GameObject>("Prefabs/Craft/BuildMina");
        GameObject instance = Instantiate(reference, placeContainer);

        instance.transform.position = position;
    }

    /// <summary>
    /// Instantiate prefab Torreta inside parent at position.
    /// </summary>
    void CreateTorreta(Vector3 position)
    {
        GameObject reference = Resources.Load<GameObject>("Prefabs/Craft/BuildTorreta");
        GameObject instance = Instantiate(reference, placeContainer);

        instance.transform.position = position;
    }

    #endregion

    #region Instatiation

    /// <summary>
    /// Add collection card inside parent.
    /// </summary>
    public static PlaceBuild Create(Transform _parent, Craft _craft)
    {
        PlaceBuild reference = Resources.Load<PlaceBuild>("Prefabs/Craft/PlaceBuild");
        PlaceBuild instance = Instantiate(reference, _parent);

        instance.craft = _craft;
        instance.placeContainer = _parent;

        instance.barricada.SetActive(_craft.Title == "Barricada");
        instance.arame.SetActive(_craft.Title == "Arame Farpado");
        instance.mina.SetActive(_craft.Title == "Mina Terrestre");
        instance.torreta.SetActive(_craft.Title == "Torreta");

        return instance;
    }

    #endregion
}
