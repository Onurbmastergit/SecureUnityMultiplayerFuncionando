using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectStatus : MonoBehaviour
{   
    public int vidaAtual;
    float fillAmount;
   public int vidaTotal;
   public UnityEngine.UI.Image vidaObject;
   public VfxColor color;
   public Collider colliderObj;

    private void Start()
   {
    vidaAtual = vidaTotal;
    color = gameObject.GetComponent<VfxColor>();
    transform.Rotate(-90, 0, 0);
   }
   void Update()//teste
   {
    fillAmount = (float)vidaAtual/vidaTotal;
    vidaObject.fillAmount = fillAmount;
    color.ChangeColor();
   }
   public void ReceberDano(int dano)
   {
    vidaAtual -= dano;

    //Atualiza A vida no canvas
    fillAmount = (float)vidaAtual/vidaTotal;
    vidaObject.fillAmount = fillAmount;

    //Atualiza a cor da barra de vida de acordo com a quantidade de vida
    color.ChangeColor();

     VerificarMorte();
   }
   void VerificarMorte()
   {
    if(vidaAtual <= 0)
    {
       transform.Translate(0,-10,0);
        Invoke("Morte", 0.5f);
    }
   }
   void Morte()
   {
    Destroy(gameObject);
   }
}