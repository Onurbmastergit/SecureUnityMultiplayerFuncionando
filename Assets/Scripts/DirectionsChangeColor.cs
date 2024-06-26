using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionsChangeColor : MonoBehaviour
{
    Color32 corVermelho = new Color32(249, 6, 0, 255);
    Color32 corBranca = new Color32(255, 255, 255, 255);

    public string direction;

    void Update()
    {
        if (NpcManager.instacia.showDirectionInMap == true)
        {
            if (direction == SpawnSelection.instacia.spawnDirecao)
            {
                transform.GetComponent<Animator>().SetTrigger("OnSelection");
                transform.GetComponent<UnityEngine.UI.Image>().color = corVermelho;
            }
        }
        else transform.GetComponent<UnityEngine.UI.Image>().color = corBranca;

        if (LevelManager.instance.currentHour.Value == 6) NpcManager.instacia.showDirectionInMap = false;
    }
}
