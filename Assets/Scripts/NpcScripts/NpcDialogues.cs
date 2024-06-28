using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogues : MonoBehaviour
{
    #region Variables

    public static NpcDialogues instacia;

    #endregion

    #region Initialization

    void Awake()
    {
        instacia = this;
    }

    #endregion

    #region Functions

    public void ShowTextAlert()
    {
        string dialogueAlert = $"Avistamos uma horda se aproximando ao {SpawnSelection.instacia.spawnDirecao.ToUpper()}. Fiquem atentos a esta direção";
        StartFunctions(dialogueAlert);
    }

    public void ShowTextOutsinal()
    {
        string dialogueOutsinal = $"Olá sobrevivente, aqui está o relatório da coleta d{LevelManager.instance.selectedLocation.Artigo} {LevelManager.instance.selectedLocation.Title.ToUpper()}\n+{LevelManager.instance.selectedLocation.Wood} Madeira\n+{LevelManager.instance.selectedLocation.Stone} Pedra\n+{LevelManager.instance.selectedLocation.Metal} Metais";
        StartFunctions(dialogueOutsinal);
    }

    public void ShowTextMap()
    {
        string dialogueMap = $"Coletaremos recursos n{LevelManager.instance.selectedLocation.Artigo} {LevelManager.instance.selectedLocation.Title.ToUpper()}";
        StartFunctions(dialogueMap);
    }

    public void StartFunctions(string dialogueOutsinal)
    {
        StartCoroutine(TextEffect.instacia.ShowText(dialogueOutsinal));
    }

    #endregion
}
