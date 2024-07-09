using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlertScript : MonoBehaviour
{
   public void DesabilitarAlerta()
   {
    HudManager.instance.DesativarAlerta();
   }
}
