using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class GamepadButtonsSystem : MonoBehaviour
{
   public UnityEngine.UI.Button mapButton;
   public UnityEngine.UI.Button curaButton;
   public UnityEngine.UI.Button radioButton;
   public UnityEngine.UI.Button Startbutton;
   public UnityEngine.UI.Button Backbutton;
   public UnityEngine.UI.Button Solobutton;
   public GameObject radioHUD;
   public GameObject mapHUD;
   public GameObject curaHUD;
   public GameObject mapHUD2;
   public GameObject craftHud;
   public GameObject canvasLogin;
   public UnityEngine.UI.Button craftButton;
   public UnityEngine.UI.Button closeMapButton;
   public UnityEngine.UI.Button closeCraftButton;


    void Update()
    {
        // Verifica se o controle foi detectado e se o LB foi pressionado
        if (Gamepad.current != null && Gamepad.current.leftShoulder.wasPressedThisFrame && mapHUD2.activeSelf == true)
        {
            mapButton.onClick.Invoke();
        }
        if(Gamepad.current != null && Gamepad.current.rightShoulder.wasPressedThisFrame && curaHUD.activeSelf == true)
        {
            curaButton.onClick.Invoke();
        }
        if(Gamepad.current != null && Gamepad.current.xButton.wasPressedThisFrame && radioHUD.activeSelf == true)
        {
            radioButton.onClick.Invoke();
        }
         if(Gamepad.current != null && Gamepad.current.yButton.wasPressedThisFrame )
        {
            craftButton.onClick.Invoke();
        }
         if(Gamepad.current != null && Gamepad.current.bButton.wasPressedThisFrame && craftHud.activeSelf == true)
        {
            closeCraftButton.onClick.Invoke();
        }
         if(Gamepad.current != null && Gamepad.current.bButton.wasPressedThisFrame && mapHUD.activeSelf == true )
        {
            closeMapButton.onClick.Invoke();
        }
          if(Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame && canvasLogin.activeSelf == true)
        {
            Solobutton.onClick.Invoke();
        }
         if(Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame && canvasLogin.activeSelf == true)
        {
            Startbutton.onClick.Invoke();
        }
         if(Gamepad.current != null && Gamepad.current.bButton.wasPressedThisFrame && canvasLogin.activeSelf == true)
        {
            Backbutton.onClick.Invoke();
        }


    }
}
