using FishNet.Connection;
using FishNet.Object;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
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

    [SerializeField] GameObject craftPopup;

    Transform buildsContainer;

    #endregion

    #region Initialization

    public override void OnStartClient()
    {
        base.OnStartClient();

        buildsContainer = GameObject.FindWithTag("PlaceBuild").transform;
        UpdateCard();
        craftPopup.SetActive(false);

    }

    void Update()
    {
        if (LevelManager.instance.woodTotal.Value < craft.WoodCost ||
            LevelManager.instance.stoneTotal.Value < craft.StoneCost ||
            LevelManager.instance.metalTotal.Value < craft.MetalCost ||
            LevelManager.instance.tecnologyTotal.Value < craft.TecnologyCost)
        {
            background.color = Color.gray;
            return;
        }
        else background.color = Color.white;

        if (craft.Id != 5) return;
        if (LevelManager.instance.playerDamage.Value >= 25)
        {
            background.color = Color.gray;
        }
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
    [ServerRpc(RequireOwnership = false)]
    public void Server_CheckMaterial( NetworkConnection conn )
    {
        
        // Confere se o player tem materiais suficientes para construicao da Build
        if (LevelManager.instance.woodTotal.Value >= craft.WoodCost
            && LevelManager.instance.stoneTotal.Value >= craft.StoneCost
            && LevelManager.instance.metalTotal.Value >= craft.MetalCost
            && LevelManager.instance.tecnologyTotal.Value >= craft.TecnologyCost)
        {

            // Fecha Menu Build apos selecionar uma Build para construir
            if (craft.Id != 5 && craft.Id != 6)
            {
                GameObject placeBuild = Instantiate(placeBuildPrefab, buildsContainer);
                base.Spawn(placeBuild, conn);
                placeBuild.GetComponent<PlaceBuild>().craft = craft;
                // A antiga fun��o CreatePlaceBuild n�o era funcional, pois ela instanciava o craft apenas no client,
                // mas � necess�rio t�-la no servidor para que o server possa instanciar o craft do pr�prio client
                // CreatePlaceBuild(conn); 

                CloseCraftPopup(conn);
            }
            else
            {
                if (LevelManager.instance.playerDamage.Value >= 25) return;

                // Atualiza nova quantidade de Recursos apos instancia
                LevelManager.instance.SetWoodTotal(LevelManager.instance.woodTotal.Value - craft.WoodCost);
                LevelManager.instance.SetStoneTotal(LevelManager.instance.stoneTotal.Value - craft.StoneCost);
                LevelManager.instance.SetMetalTotal(LevelManager.instance.metalTotal.Value - craft.MetalCost);

                UpdateCraftPopup(conn);
                if (craft.Id == 5) UpgradePistol();
            }
        }
        else Debug.Log("Not enought Materials for this Craft.");
    }

    [ServerRpc(RequireOwnership = false)]
    void UpgradePistol()
    {
        LevelManager.instance.SetPlayerDamage(LevelManager.instance.playerDamage.Value + 5);

        craft.WoodCost += 1;
        craft.StoneCost += 1;
        craft.MetalCost += 1;
        craft.TecnologyCost += 1;

        UpdateCard();
    }

    [TargetRpc]
    void UpdateCraftPopup(NetworkConnection conn)
    {
        craftPopup.GetComponent<CraftPopup>().UpdateMaterials();
    }

    [TargetRpc]
    void CloseCraftPopup(NetworkConnection conn)
    {
        craftPopup.SetActive(false);
    }

    #endregion
}
