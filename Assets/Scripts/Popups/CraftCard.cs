using FishNet.Object;
using TMPro;
using UnityEngine;

public class CraftCard : NetworkBehaviour
{
    #region Variables

    Craft craft;

    [SerializeField] TextMeshProUGUI craftName;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI woodCost;
    [SerializeField] TextMeshProUGUI stoneCost;
    [SerializeField] TextMeshProUGUI metalCost;
    [SerializeField] TextMeshProUGUI tecnologyCost;

    [SerializeField] GameObject placeBuild;
    [SerializeField] Transform placeContainer;

    #endregion

    #region Functions

    /// <summary>
    /// Checks if there is enough material for the craft.
    /// </summary>
    [ServerRpc]
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
                GameObject reference = Resources.Load<GameObject>("Prefabs/Craft/PlaceBuild");
                GameObject instance = Instantiate(reference, placeContainer);
                instance.GetComponent<PlaceBuild>().craft = craft;
                base.Spawn(instance);
                
                Close();
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

    /// <summary>
    /// Destroys the GatherPopup.
    /// </summary>
    public void Close()
    {
        Destroy(GameObject.Find("CraftPopup(Clone)"));
    }

    #endregion

    #region Instatiation

    /// <summary>
    /// Add collection card inside parent.
    /// </summary>
    public static CraftCard Add(Transform _parent, Craft _craft, Transform _buildContainer)
    {
        CraftCard reference = Resources.Load<CraftCard>("Prefabs/Popups/CraftCard");
        CraftCard instance = Instantiate(reference, _parent);

        instance.craft = _craft;

        instance.placeContainer = _buildContainer;

        instance.craftName.text = _craft.Title;
        instance.description.text = _craft.Description;
        instance.woodCost.text = _craft.WoodCost.ToString();
        instance.stoneCost.text = _craft.StoneCost.ToString();
        instance.metalCost.text = _craft.MetalCost.ToString();
        instance.tecnologyCost.text = _craft.TecnologyCost.ToString();

        return instance;
    }

    #endregion
}
