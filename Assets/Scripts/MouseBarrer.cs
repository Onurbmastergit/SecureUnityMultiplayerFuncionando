using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class MouseBarrer : MonoBehaviour
{
   private VirtualMouseInput virtualMouseInput;
   private void Awake()
   {
    virtualMouseInput = GetComponent<VirtualMouseInput>();

   }

   private void LateUpdate()
   {
        Vector2 virtualMousePosition = virtualMouseInput.virtualMouse.position.value;
        virtualMousePosition.x = Mathf.Clamp(virtualMousePosition.x , 0f, Screen.width);
        virtualMousePosition.x = Mathf.Clamp(virtualMousePosition.y , 0f, Screen.height);
        InputState.Change(virtualMouseInput.virtualMouse.position, virtualMousePosition);
   }
}
