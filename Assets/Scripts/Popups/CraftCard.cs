using FishNet.Object;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] Image image;
    [SerializeField] Image background;

    [SerializeField] GameObject placeBuildPrefab;

    [SerializeField] Transform craftPopup;
    [SerializeField] GameObject popup;

    Transform buildsContainer;

    #endregion

    #region Initialization

    public override void OnStartClient()
    {
        base.OnStartClient();

        buildsContainer = GameObject.FindWithTag("PlaceBuild").transform;
        UpdateCard();
        popup.SetActive(false);
    }

    void Update()
    {
        if (LevelManager.instance.woodTotal.Value < craft.WoodCost ||
            LevelManager.instance.stoneTotal.Value < craft.StoneCost ||
            LevelManager.instance.metalTotal.Value < craft.MetalCost ||
            LevelManager.instance.tecnologyTotal.Value < craft.TecnologyCost)
        {
            background.color = Color.gray;
        }
        else background.color = Color.white;
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
        image.sprite = craft.Image;
    }

    /// <summary>
    /// Checks if there is enough material for the craft.
    /// </summary>
    public void CheckMateral()
    {
        // Confere se o player tem materiais suficientes para construicao da Build
        if (LevelManager.instance.woodTotal.Value >= craft.WoodCost
            && LevelManager.instance.stoneTotal.Value >= craft.StoneCost
            && LevelManager.instance.metalTotal.Value >= craft.MetalCost
            && LevelManager.instance.tecnologyTotal.Value >= craft.TecnologyCost)
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
                LevelManager.instance.SetWoodTotal(LevelManager.instance.woodTotal.Value - craft.WoodCost);
                LevelManager.instance.SetStoneTotal(LevelManager.instance.stoneTotal.Value - craft.StoneCost);
                LevelManager.instance.SetMetalTotal(LevelManager.instance.metalTotal.Value - craft.MetalCost);

                UpdateCraftPopup();
            }
        }
        else Debug.Log("Not enought Materials for this Craft.");
    }

    //[ObserversRpc(BufferLast = true)]
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
