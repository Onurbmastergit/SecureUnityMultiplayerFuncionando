using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class ObjectStatus : NetworkBehaviour
{
    #region Variables

    public int vidaAtual;
    float fillAmount;
    public int vidaTotal;
    public UnityEngine.UI.Image vidaObject;
    public VfxColor color;
    public Collider colliderObj;

    #endregion

    #region Initialization

    private void Start()
    {
        vidaAtual = vidaTotal;
        color = gameObject.GetComponent<VfxColor>();
    }

    void Update() // teste
    {
        fillAmount = (float)vidaAtual / vidaTotal;
        vidaObject.fillAmount = fillAmount;
        color.ChangeColor();
    }

    #endregion

    #region Functions

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;

        // Atualiza A vida no canvas
        fillAmount = (float)vidaAtual / vidaTotal;
        vidaObject.fillAmount = fillAmount;

        // Atualiza a cor da barra de vida de acordo com a quantidade de vida
        color.ChangeColor();

        VerificarMorte();
    }

    void VerificarMorte()
    {
        if (vidaAtual <= 0)
        {
            transform.Translate(0, -10, 0);
            Invoke("Morte", 0.5f);
        }
    }

    void Morte()
    {
        Destroy(gameObject);
        base.Despawn(gameObject);
    }

    #endregion
}