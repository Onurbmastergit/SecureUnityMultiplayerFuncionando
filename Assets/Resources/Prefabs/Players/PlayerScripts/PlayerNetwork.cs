using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Use este script para transportar o Owner e os IDs de cada usu�rio para outros componentes que exijam um dono (owner).
/// </summary>
public class PlayerNetwork : NetworkBehaviour
{

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner == false) return;

        // Loop at� 6 porque � o n�mero de crafts dispon�veis. Se precisar, reduza esse n�mero
        for (int i = 0; i < 6; i++)
        {
            // Adiciona ao bot�o craft a refer�ncia deste cliente. Ao clicar me craftar, � poss�vel saber qual client est� usando esta op��o
            CraftCard cc = GameObject.FindWithTag("CraftPopup").transform.Find("Panel").Find("Container").Find("CraftCard" + i).GetComponent<CraftCard>();
            cc.GetComponent<Button>().onClick.AddListener(() => {
                cc.Server_CheckMaterial(base.Owner);                
            });
        }

        // Anima��o de constru��o ao abrir a interface. Por�m n�o est� parando a anima��o ao fechar a interface.
        // Tamb�m n�o funciona no client, apenas no host
        GameObject.FindWithTag("HudZombie").transform.parent.Find("Craft (Button)").GetComponent<Button>().onClick.AddListener(() =>
        {
            transform.GetComponent<InputControllers>().build = true;
        });

    }

}
