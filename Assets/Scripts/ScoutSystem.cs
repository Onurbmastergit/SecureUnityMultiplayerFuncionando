using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoutSystem : MonoBehaviour
{
    public static ScoutSystem instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject ScoutReturn;
    public TextMeshProUGUI ScoutResult;
    public TextMeshProUGUI ScoutAnswer;

    // 0 = Search for horde | 1 = Search for survivor
    public int scoutAction;
    bool survivorFound;

    public void ScoutAction()
    {
        if (scoutAction == 0)
        {

        }
    }

    void LookForSurvivor()
    {
        int chance = Random.Range(0, 9);
        if (chance > LevelManager.instance.numSurvivorsGatherer + LevelManager.instance.numSurvivorsScientist)
        {
            survivorFound = true;
        }

        if (survivorFound)
        {
            if (chance < 5)
            {
                ++LevelManager.instance.numSurvivorsGatherer;
            }
            else ++LevelManager.instance.numSurvivorsScientist;
        }
    }
}
