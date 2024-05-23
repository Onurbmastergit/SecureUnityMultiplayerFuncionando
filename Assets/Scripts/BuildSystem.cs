using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Build
{
    public GameObject build;
    public Sprite buildThumb;
    public string name;
    public string info;
    public int woodCost;
    public int stoneCost;
    public int metalCost;
    public int tecnologyCost;

    public Build(string name, string info, GameObject build, Sprite buildThumb, int woodCost, int stoneCost, int metalCost, int tecnologyCost)
    {
        this.name = name;
        this.info = info;
        this.build = build;
        this.buildThumb = buildThumb;
        this.woodCost = woodCost;
        this.stoneCost = stoneCost;
        this.metalCost = metalCost;
        this.tecnologyCost = tecnologyCost;
    }
}

public class BuildSystem : MonoBehaviour
{
    #region Variables

    public GameObject barricada;
    public GameObject arame;
    public GameObject mina;
    public GameObject torreta;

    public Sprite barricadaThumb;
    public Sprite arameThumb;
    public Sprite minaThumb;
    public Sprite torretaThumb;
    public Sprite meleeThumb;
    public Sprite rangedThumb;

    public GameObject craftButton;
    public Transform buildPanel;
    Menu menu;
    public List<Build> buildList = new List<Build>();

    int buildIndex;
    public GameObject buildingPrefab;
    bool building;

    #endregion

    #region Initialization

    void Start()
    {
        buildingPrefab.SetActive(false);

        CreateCraftButtons();
    }

    private void Update()
    {
        CraftBuild();
    }

    #endregion

    #region Functions

    void CreateCraftButtons()
    {
        buildList.Add(new Build
        (
            "Barricada",
            "Bloqueia a passagem de zumbis",
            barricada,
            barricadaThumb,
            1, 1, 1, 1
        ));
        buildList.Add(new Build
        (
            "Arame Farpado",
            "Diminui a mobilidade e da dano ao atravessar",
            arame,
            arameThumb,
            2, 2, 2, 1
        ));
        buildList.Add(new Build
        (
            "Mina Terrestre",
            "Explode ao contato. Uso unico",
            mina,
            minaThumb,
            3, 3, 3, 1
        ));
        buildList.Add(new Build
        (
            "Torreta",
            "Atira em inimigos automaticamente. Precisa ser recarregada",
            torreta,
            torretaThumb,
            4, 4, 4, 1
        ));
        buildList.Add(new Build
        (
            "Armamento Faca",
            "Melhora seus ataques corpo a corpo",
            null,
            meleeThumb,
            5, 5, 5, 1
        ));
        buildList.Add(new Build
        (
            "Armamento Rifle",
            "Melhora seus ataques a Distancia",
            null,
            rangedThumb,
            6, 6, 6, 1
        ));

        for (int i = 0; i < buildList.Count; i++)
        {
            int index = i;

            GameObject craftInstance = Instantiate(craftButton, buildPanel);
            craftInstance.transform.Find("Name (TMP)").GetComponent<TextMeshProUGUI>().text = buildList[i].name;
            craftInstance.transform.Find("Info (TMP)").GetComponent<TextMeshProUGUI>().text = buildList[i].info;
            craftInstance.transform.Find("Image").GetComponent<Image>().sprite = buildList[i].buildThumb;
            craftInstance.transform.Find("Wood Material").GetComponentInChildren<TextMeshProUGUI>().text = buildList[i].woodCost.ToString();
            craftInstance.transform.Find("Stone Material").GetComponentInChildren<TextMeshProUGUI>().text = buildList[i].stoneCost.ToString();
            craftInstance.transform.Find("Metal Material").GetComponentInChildren<TextMeshProUGUI>().text = buildList[i].metalCost.ToString();
            craftInstance.transform.Find("Tecnology Material").GetComponentInChildren<TextMeshProUGUI>().text = buildList[i].tecnologyCost.ToString();

            craftInstance.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(CraftButton(index)));
        }
    }

    public IEnumerator CraftButton(int i)
    {
        // Confere se o player tem materiais suficientes para construicao da Build
        if (LevelManager.instance.woodTotal >= buildList[i].woodCost
            && LevelManager.instance.stoneTotal >= buildList[i].stoneCost
            && LevelManager.instance.metalTotal >= buildList[i].metalCost
            && LevelManager.instance.tecnologyTotal >= buildList[i].tecnologyCost)
        {
            // Fecha Menu Build apos selecionar uma Build para construir
            if (buildList[i].build != null)
            {
                buildIndex = i;
                buildingPrefab.SetActive(true);
                Menu.instance.ButtonBuild();
                building = true;
            }
            else
            {
                // Atualiza nova quantidade de Recursos apos instancia
                LevelManager.instance.woodTotal -= buildList[buildIndex].woodCost;
                LevelManager.instance.stoneTotal -= buildList[buildIndex].stoneCost;
                LevelManager.instance.metalTotal -= buildList[buildIndex].metalCost;
                LevelManager.instance.woodCounter.text = LevelManager.instance.woodTotal.ToString();
                LevelManager.instance.stoneCounter.text = LevelManager.instance.stoneTotal.ToString();
                LevelManager.instance.metalCounter.text = LevelManager.instance.metalTotal.ToString();
            }
        }
        yield return null;
    }

    void CraftBuild()
    {
        if (building)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 mouseInWorld = Vector3.zero;
            if (Physics.Raycast(ray, out hit))
            {
                mouseInWorld = new Vector3(Mathf.RoundToInt(hit.point.x), 0, Mathf.RoundToInt(hit.point.z));
                buildingPrefab.transform.position = mouseInWorld;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject buildInstance = Instantiate(buildList[buildIndex].build, mouseInWorld, Quaternion.identity);

                // Atualiza nova quantidade de Recursos apos instancia
                LevelManager.instance.woodTotal -= buildList[buildIndex].woodCost;
                LevelManager.instance.stoneTotal -= buildList[buildIndex].stoneCost;
                LevelManager.instance.metalTotal -= buildList[buildIndex].metalCost;
                LevelManager.instance.woodCounter.text = LevelManager.instance.woodTotal.ToString();
                LevelManager.instance.stoneCounter.text = LevelManager.instance.stoneTotal.ToString();
                LevelManager.instance.metalCounter.text = LevelManager.instance.metalTotal.ToString();

                buildingPrefab.SetActive(false);
                building = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                buildingPrefab.SetActive(false);
                building = false;
            }
        }
    }

    #endregion

}
