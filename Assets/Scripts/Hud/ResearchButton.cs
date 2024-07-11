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
        if (LevelManager.instance.tecnologyTotal.Value >= 3)
        {
            if ((LevelManager.instance.cureMeter.Value / 10) < (LevelManager.instance.tecnologyTotal.Value + 1))
            {
                locked.SetActive(true);

                cure.SetActive(false);
                tecnology.SetActive(false);

                meter.SetActive(true);
                if (LevelManager.instance.cureMeter.Value < 40)
                {
                    fill.fillAmount = LevelManager.instance.cureMeter.Value / 40;
                }
                else
                {
                    fill.fillAmount = (LevelManager.instance.cureMeter.Value % 10) / 10;
                }

                return;
            }
        }
        
        if (LevelManager.instance.cureResearch.Value)
        {
            cure.SetActive(true);

            tecnology.SetActive(false);
            locked.SetActive(false);
        }
        else
        {
            tecnology.SetActive(true);

            cure.SetActive(false);
            locked.SetActive(false);
        }

        meter.SetActive(false);
    }

    #endregion
}
