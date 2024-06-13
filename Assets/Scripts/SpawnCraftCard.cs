using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCraftCard : NetworkBehaviour
{
    Transform craftcardContainer;
    [SerializeField] GameObject craftcardPrefab;

    [SerializeField] Craft barricada;
    [SerializeField] Craft arame;
    [SerializeField] Craft mina;
    [SerializeField] Craft torreta;
    [SerializeField] Craft faca;
    [SerializeField] Craft rifle;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            craftcardContainer = GameObject.FindWithTag("CraftContainer").transform;

            GameObject craftcardBarricada = Instantiate(craftcardPrefab, craftcardContainer);
            craftcardBarricada.GetComponent<CraftCard>().craft = barricada;
            base.Spawn(craftcardBarricada);

            GameObject craftcardArame = Instantiate(craftcardPrefab, craftcardContainer);
            craftcardArame.GetComponent<CraftCard>().craft = arame;
            base.Spawn(craftcardArame);

            GameObject craftcardMina = Instantiate(craftcardPrefab, craftcardContainer);
            craftcardMina.GetComponent<CraftCard>().craft = mina;
            base.Spawn(craftcardMina);

            GameObject craftcardTorreta = Instantiate(craftcardPrefab, craftcardContainer);
            craftcardTorreta.GetComponent<CraftCard>().craft = torreta;
            base.Spawn(craftcardTorreta);

            GameObject craftcardFaca = Instantiate(craftcardPrefab, craftcardContainer);
            craftcardFaca.GetComponent<CraftCard>().craft = faca;
            base.Spawn(craftcardFaca);

            GameObject craftcardRifle = Instantiate(craftcardPrefab, craftcardContainer);
            craftcardRifle.GetComponent<CraftCard>().craft = rifle;
            base.Spawn(craftcardRifle);

            Transform craftPopup = GameObject.FindWithTag("CraftPopup").transform;
            craftPopup.gameObject.SetActive(false);
        }
    }
}
