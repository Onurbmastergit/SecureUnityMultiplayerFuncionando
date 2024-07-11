using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HabilitarTutorial : MonoBehaviour
{
  public GameObject tutorial;
  public GameObject manager;
  public GameObject warningTuto;
  public GraphicRaycaster graphicRaycaster;
  bool fecharAviso = false;
  bool startGame = true;

  void Update()
  {
    if(LevelManager.instance.currentDay == 1 && LevelManager.instance.currentHour.Value == 6 && startGame == true)
    {
        warningTuto.SetActive(true);
        graphicRaycaster.enabled = false;
        manager.SetActive(false);
    }
    if(fecharAviso == true)warningTuto.SetActive(false);
  }  
  public void HabilitarTuto ()
  {
    CloseWarning();
    tutorial.SetActive (true);
    tutorial.GetComponent<Animator>().SetTrigger("View");
    manager.SetActive (false);
  }

  public void DesabilitarTuto()
  {
    CloseWarning();
    tutorial.SetActive (false);
    graphicRaycaster.enabled = true;
    manager.SetActive (true);
  }

  public void CloseWarning()
  {
    fecharAviso = true;
    startGame = false;
    warningTuto.SetActive (false);
  }
}
