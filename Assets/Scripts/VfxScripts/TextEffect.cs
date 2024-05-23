using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TextEffect : MonoBehaviour
{
    public static TextEffect instacia;
    char[] ctr;
    public float duration;
    public int selectionNumber;
    public string nameNpcShow;
    public TextMeshProUGUI[] viewerAlert;
    
    public TextMeshProUGUI nameNpc;
    void Awake()
    {
        instacia = this;
    }

    void Update()
    {
        nameNpc.text = nameNpcShow;
    }

   public IEnumerator ShowText(string text)
   {
     viewerAlert[selectionNumber].text = null;
     ctr = null;  
     ctr = text.ToCharArray();
     int count = 0;
     while(count < ctr.Length)
     {
        yield return new WaitForSeconds(duration);
        viewerAlert[selectionNumber].text += ctr[count];
        count++;
     }
     Invoke("DisbleAlert", 3);  
   }
   void DisbleAlert()
   {
    NpcManager.instacia.npcAlert.SetActive(false);
   }
}
