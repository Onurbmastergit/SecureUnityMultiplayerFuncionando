using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using JetBrains.Annotations;
using UnityEngine;

public class SpawnSelection : NetworkBehaviour
{
    #region Variables

    public static SpawnSelection instacia;
    public static int spawn;
    public string spawnDirecao;
    public List<GameObject> spawns;

    bool spawnSelected;
    int ultimoSpawn;

    #endregion

    #region Initialization

    public override void OnStartServer()
    {
        base.OnStartServer();
        InvokeRepeating("Update_Server", 0, 0.1f);
    }

    void Awake()
    {
        instacia = this;
    }

    [Server]
    void Update_Server()
    {
        if (LevelManager.instance.currentHour.Value == 8 && !spawnSelected)
        {
            SelectionSpawn();
        }

        if (LevelManager.instance.currentHour.Value == 6)
        {
            spawnDirecao = null;
            spawnSelected = false;

            for (int i = 0; i < spawns.Count; i++)
            {
                spawns[i].SetActive(false);
                spawns[i].GetComponent<SpawnSystem>().enableSpawn = false;
            }
        }
    }

    #endregion

    #region Functions

    [Server]
    void SelectionSpawn()
    {
        while (spawn == ultimoSpawn)
        {
            RandomizarNumero();
        }

        for (int i = 0; i < spawns.Count; i++)
        {
            if (i == spawn)
            {
                spawns[i].SetActive(true);
                spawnDirecao = spawns[i].GetComponent<SpawnSystem>().direcaoSpawn;
                spawns[i].GetComponent<SpawnSystem>().enableSpawn = true;
            }
            else
            {
                spawns[i].SetActive(false);
                spawns[i].GetComponent<SpawnSystem>().enableSpawn = false;
            }
        }

        ultimoSpawn = spawn;
        spawnSelected = true;
    }

    [Server]
    public void RandomizarNumero()
    {
        spawn = Random.Range(0, 4);
    }

    #endregion
}
