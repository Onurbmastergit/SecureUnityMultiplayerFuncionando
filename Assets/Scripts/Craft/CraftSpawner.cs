using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class CraftSpawner : NetworkBehaviour
{
    [SerializeField] GameObject barricadaPrefab;
    [SerializeField] GameObject aramePrefab;
    [SerializeField] GameObject minaPrefab;
    [SerializeField] GameObject torretaPrefab;

    [ServerRpc]
    public void Spawn(Craft craft, Transform position)
    {
        if (craft.Id == 1)
        {
            GameObject barricadaInstance = Instantiate(barricadaPrefab, position);
            base.Spawn(barricadaInstance);
        }
        if (craft.Id == 2)
        {
            GameObject arameInstance = Instantiate(aramePrefab, position);
            base.Spawn(arameInstance);
        }
        if (craft.Id == 3)
        {
            GameObject minaInstance = Instantiate(minaPrefab, position);
            base.Spawn(minaInstance);
        }
        if (craft.Id == 4)
        {
            GameObject torretaInstance = Instantiate(torretaPrefab, position);
            base.Spawn(torretaInstance);
        }
    }
}
