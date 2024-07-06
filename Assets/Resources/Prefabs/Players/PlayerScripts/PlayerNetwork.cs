using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Use este script para transportar o Owner e os IDs de cada usuário para outros componentes que exijam um dono (owner).
/// </summary>
public class PlayerNetwork : NetworkBehaviour
{

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner == false) return;

        // Loop até 6 porque é o número de crafts disponíveis. Se precisar, reduza esse número
        for (int i = 0; i < 6; i++)
        {
            // Adiciona ao botão craft a referência deste cliente. Ao clicar me craftar, é possível saber qual client está usando esta opção
            CraftCard cc = GameObject.FindWithTag("CraftPopup").transform.Find("Panel").Find("Container").Find("CraftCard" + i).GetComponent<CraftCard>();
            cc.GetComponent<Button>().onClick.AddListener(() => {
                cc.Server_CheckMaterial(base.Owner);                
            });
        }

        // Animação de construção ao abrir a interface. Porém não está parando a animação ao fechar a interface.
        // Também não funciona no client, apenas no host
        GameObject.FindWithTag("HudZombie").transform.parent.Find("Craft (Button)").GetComponent<Button>().onClick.AddListener(() =>
        {
            transform.GetComponent<InputControllers>().build = true;
        });

    }

}
