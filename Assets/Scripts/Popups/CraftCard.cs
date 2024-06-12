using FishNet.Object;
using TMPro;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CraftCard : NetworkBehaviour
{
    #region Variables

    [SerializeField] Craft craft;

    [SerializeField] TextMeshProUGUI craftName;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI woodCost;
    [SerializeField] TextMeshProUGUI stoneCost;
    [SerializeField] TextMeshProUGUI metalCost;
    [SerializeField] TextMeshProUGUI tecnologyCost;

    [SerializeField] GameObject placeBuildPrefab;

    #endregion

    #region Initialization

    void Start()
    {
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
    [ObserversRpc(BufferLast = true)]
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
                ActivatePlaceBuild();
            }
            else
            {
                // Atualiza nova quantidade de Recursos apos instancia
                LevelManager.instance.woodTotal -= craft.WoodCost;
                LevelManager.instance.stoneTotal -= craft.StoneCost;
                LevelManager.instance.metalTotal -= craft.MetalCost;
            }
        }
        else Debug.Log("Not enought Materials for this Craft.");
    }

    public void ActivatePlaceBuild()
    {
        placeBuildPrefab.SetActive(true);
        placeBuildPrefab.GetComponent<PlaceBuild>().craft = craft;
        placeBuildPrefab.GetComponent<PlaceBuild>().UpdatePlaceBuild();
    }

    #endregion
}
