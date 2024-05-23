using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class InputControllers : NetworkBehaviour
{
  public bool Attack;
  public bool Run;
  public bool build;
            
  
  public float movimentoHorizontal;
  public float movimentoVertical;
  public static bool pistol;  
  public override void OnStartClient()
  {
    base.OnStartClient();
    if(base.IsOwner == false) return;
    pistol = false;
  }
  void Update()
  {
    if(base.IsOwner == false) return;
    movimentoHorizontal = Input.GetAxis("Horizontal");

    movimentoVertical = Input.GetAxis("Vertical");

    build = Input.GetKeyDown(KeyCode.B);

    Attack = Input.GetKey(KeyCode.Mouse1);

    Run = Input.GetKey(KeyCode.LeftShift);

  }

  
}
