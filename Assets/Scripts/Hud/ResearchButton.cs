using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResearchButton : MonoBehaviour
{
    #region Variables

    [SerializeField] GameObject cure;
    [SerializeField] GameObject tecnology;
    [SerializeField] GameObject locked;
    [SerializeField] GameObject meter;
    [SerializeField] Image fill;

    #endregion

    #region Initialization

    void Update()
    {
        ChangeColor();
    }

    #endregion

    #region Functions

    void ChangeColor()
    {
        if (LevelManager.instance.tecnologyTotal.Value == LevelManager.instance.tecnologyCap.Value)
        {
            cure.SetActive(false);
            tecnology.SetActive(false);
            locked.SetActive(true);

            meter.SetActive(true);
            fill.fillAmount = (LevelManager.instance.cureMeter.Value % 10) / 10;

            return;
        }
        
        if (LevelManager.instance.cureResearch.Value)
        {
            cure.SetActive(true);
            tecnology.SetActive(false);
            locked.SetActive(false);
        }
        else
        {
            cure.SetActive(false);
            tecnology.SetActive(true);
            locked.SetActive(false);
        }

        meter.SetActive(false);
    }

    #endregion
}
