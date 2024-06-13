using FishNet.Object;
using TMPro;
using UnityEngine;

public class CraftCard : NetworkBehaviour
{
    #region Variables

    public Craft craft;

    [SerializeField] TextMeshProUGUI craftName;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI woodCost;
    [SerializeField] TextMeshProUGUI stoneCost;
    [SerializeField] TextMeshProUGUI metalCost;
    [SerializeField] TextMeshProUGUI tecnologyCost;

    [SerializeField] GameObject placeBuildPrefab;

    [SerializeField] Transform craftPopup;

    Transform buildsContainer;

    #endregion

    #region Initialization

    public override void OnStartClient()
    {
        base.OnStartClient();

        buildsContainer = GameObject.FindWithTag("PlaceBuild").transform;
        UpdateCard();
    }

    #endregion

    #region Functions

    /// <summary>
    /// Update CraftCard Informations.
    /// </summary>
    public void UpdateCard()
    {
        craftName.text = craft.Title;
        description.text = craft.Description;
        woodCost.text = craft.WoodCost.ToString();
        stoneCost.text = craft.StoneCost.ToString();
        metalCost.text = craft.MetalCost.ToString();
        tecnologyCost.text = craft.TecnologyCost.ToString();
    }

    /// <summary>
    /// Checks if there is enough material for the craft.
    /// </summary>
    public void CheckMateral()
    {
        // Confere se o player tem materiais suficientes para construicao da Build
        if (LevelManager.instance.woodTotal >= craft.WoodCost
            && LevelManager.instance.stoneTotal >= craft.StoneCost
            && LevelManager.instance.metalTotal >= craft.MetalCost
            && LevelManager.instance.tecnologyTotal >= craft.TecnologyCost)
        {

            // Fecha Menu Build apos selecionar uma Build para construir
            if (craft.Title != "Armamento Faca" && craft.Title != "Armamento Rifle")
            {
                CreatePlaceBuild();
                CloseCraftPopup();
            }
            else
            {
                // Atualiza nova quantidade de Recursos apos instancia
                LevelManager.instance.woodTotal -= craft.WoodCost;
                LevelManager.instance.stoneTotal -= craft.StoneCost;
                LevelManager.instance.metalTotal -= craft.MetalCost;

                UpdateCraftPopup();
            }
        }
        else Debug.Log("Not enought Materials for this Craft.");
    }

    [ObserversRpc(BufferLast = true)]
    public void CreatePlaceBuild()
    {
        GameObject placeBuild = Instantiate(placeBuildPrefab, buildsContainer);
        placeBuild.GetComponent<PlaceBuild>().craft = craft;
        base.Spawn(placeBuild);
    }

    void UpdateCraftPopup()
    {
        craftPopup = GameObject.FindWithTag("CraftPopup").transform;
        craftPopup.GetComponent<CraftPopup>().UpdateMaterials();
    }

    void CloseCraftPopup()
    {
        craftPopup = GameObject.FindWithTag("CraftPopup").transform;
        craftPopup.gameObject.SetActive(false);
    }

    #endregion
}
