using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceBuild : MonoBehaviour
{
    #region Variables

    public Craft craft;

    [SerializeField] GameObject barricada;
    [SerializeField] GameObject arame;
    [SerializeField] GameObject mina;
    [SerializeField] GameObject torreta;

    public Transform placeContainer;

    #endregion

    #region Initialization

    void Start()
    {
        barricada.SetActive(craft.Title == "Barricada");
        arame.SetActive(craft.Title == "Arame Farpado");
        mina.SetActive(craft.Title == "Mina Terrestre");
        torreta.SetActive(craft.Title == "Torreta");
    }

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
            // Arredonda a posicao X e Z do ponto de colisao para o multiplo de 4 mais proximo
            mouseInWorld = new Vector3(Mathf.Round(hit.point.x / 4) * 4, 0, Mathf.Round(hit.point.z / 4) * 4);
            transform.position = mouseInWorld;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Instancia a build selecionada na posicao do mouse
            //transform.GetComponent<CraftSpawner>().Spawn(craft, placeContainer);

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

    #endregion
}
