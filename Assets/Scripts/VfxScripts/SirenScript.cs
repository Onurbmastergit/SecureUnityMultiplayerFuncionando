using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SirenScript : MonoBehaviour
{
   public static SirenScript instance;

   public float velocityRotation;
   public static bool alert;
   private float currentRotationY = 0f;
   public GameObject EffectsSiren;
   public GameObject LightSiren;
   void Awake()
   {
    instance = this;  
   }
   void Update()
   {
    if(alert)
    {
    currentRotationY += velocityRotation * Time.deltaTime;
    }
    transform.rotation = Quaternion.Euler(0, 0, currentRotationY);
    if(LevelManager.instance.currentHour == 6)
    {
        DisableAlert();
    }
   }
   public void SirenAlert()
   {
    currentRotationY += velocityRotation * Time.deltaTime;
    EffectsSiren.SetActive(true);
    alert = true;
   }
   
   public void DisableAlert()
   {
    EffectsSiren.SetActive(false);
    alert =  false;
   }
}
