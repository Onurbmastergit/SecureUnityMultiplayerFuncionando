using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
 public GameObject wKey;
   public GameObject sKey;
   public GameObject aKey;
   public GameObject dKey;
   public GameObject shiftKey;

   public RectTransform mouseRectTransform;
    Color32 corVermelho = new Color32(249, 6, 0, 255);
    Color32 corBranca = new Color32(255, 255, 255, 255); 
   void Update()
   {
        PressedButton();
        MoveMouse();
   }

    void PressedButton()
    {
        if(Input.GetKeyDown(KeyCode.W)) wKey.GetComponent<Image>().color = corVermelho;
        if(Input.GetKeyUp(KeyCode.W))wKey.GetComponent<Image>().color = corBranca;  

        if(Input.GetKeyDown(KeyCode.S)) sKey.GetComponent<Image>().color = corVermelho;
        if(Input.GetKeyUp(KeyCode.S))sKey.GetComponent<Image>().color = corBranca;

        if(Input.GetKeyDown(KeyCode.A)) aKey.GetComponent<Image>().color = corVermelho;
        if(Input.GetKeyUp(KeyCode.A))aKey.GetComponent<Image>().color = corBranca;

        if(Input.GetKeyDown(KeyCode.D)) dKey.GetComponent<Image>().color = corVermelho;
        if(Input.GetKeyUp(KeyCode.D))dKey.GetComponent<Image>().color = corBranca;

        if(Input.GetKeyDown(KeyCode.LeftShift)) shiftKey.GetComponent<Image>().color = corVermelho;
        if(Input.GetKeyUp(KeyCode.LeftShift))shiftKey.GetComponent<Image>().color = corBranca;
    }
    void MoveMouse()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mouseRectTransform.parent as RectTransform, 
            Input.mousePosition, 
            null, 
            out localPoint
        );
        mouseRectTransform.localPosition = localPoint;    
    }
}
