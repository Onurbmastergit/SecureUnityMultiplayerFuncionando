using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
   public static HudManager instance;

    public GameObject hudAlerta;
   void Awake()
   {
    instance = this;
   }

   void Update()
   {
    if(LevelManager.instance.currentHour == 23)
    {
        hudAlerta.SetActive(true);
    }
   }
   public void DesativarAlerta()
   {
    hudAlerta.SetActive(false);
   }
}
