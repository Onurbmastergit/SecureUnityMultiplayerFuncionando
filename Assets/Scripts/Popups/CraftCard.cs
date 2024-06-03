using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftCard : MonoBehaviour
{
    #region Variables

    Craft craft;

    [SerializeField] TextMeshProUGUI craftName;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI woodCost;
    [SerializeField] TextMeshProUGUI stoneCost;
    [SerializeField] TextMeshProUGUI metalCost;
    [SerializeField] TextMeshProUGUI tecnologyCost;

    GameObject buildingPrefab;

    #endregion

    #region Initialization

    void Update()
    {
        CraftBuild();
    }

    #endregion

    #region Functions

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
                buildingPrefab = Instantiate(craft.Prefab, Vector3.zero, Quaternion.identity);
                Hide();
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

    void CraftBuild()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mouseInWorld = Vector3.zero;

        if (Physics.Raycast(ray, out hit))
        {
            // Arredonda a posição X e Z do ponto de colisão para o múltiplo de 4 mais próximo
            mouseInWorld = new Vector3(Mathf.Round(hit.point.x / 4) * 4, 0, Mathf.Round(hit.point.z / 4) * 4);
            buildingPrefab.transform.position = mouseInWorld;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject buildInstance = Instantiate(craft.Prefab, mouseInWorld, Quaternion.identity);

            // Atualiza nova quantidade de Recursos apos instancia
            LevelManager.instance.woodTotal -= craft.WoodCost;
            LevelManager.instance.stoneTotal -= craft.StoneCost;
            LevelManager.instance.metalTotal -= craft.MetalCost;

            Destroy(buildingPrefab);
            Close();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            Destroy(buildingPrefab);
            Close();
        }
    }

    /// <summary>
    /// Destroys the GatherPopup.
    /// </summary>
    public void Hide()
    {
        GameObject CraftPopup = GameObject.Find("CraftPopup(Clone)");
        CraftPopup.SetActive(false);
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
    public static CraftCard Add(Transform _parent, Craft _craft)
    {
        CraftCard reference = Resources.Load<CraftCard>("Prefabs/Popups/CraftCard");
        CraftCard instance = Instantiate(reference, _parent);

        instance.craft = _craft;

        instance.craftName.text = _craft.Title;
        instance.description.text = _craft.Description;
        instance.woodCost.text = _craft.WoodCost.ToString();
        instance.stoneCost.text = _craft.StoneCost.ToString();
        instance.metalCost.text = _craft.MetalCost.ToString();
        instance.tecnologyCost.text = _craft.TecnologyCost.ToString();

        if (_craft.Prefab != null) instance.buildingPrefab = _craft.Prefab;
        instance.buildingPrefab.SetActive(false);

        return instance;
    }

    #endregion
}
