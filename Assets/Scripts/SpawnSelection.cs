using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using JetBrains.Annotations;
using UnityEngine;

public class SpawnSelection : NetworkBehaviour
{
    public static SpawnSelection instacia;
    public static int spawn;
    public bool enableRandom;
    public string spawnDirecao;
    public List<GameObject> spawns;


    public override void OnStartServer()
    {
        base.OnStartServer();
        InvokeRepeating("Update_Server",0,0.1f);
    }
   
    void Awake()
    {
        instacia = this;
    }

   [Server]
    void Update_Server()
    {
        if(LevelManager.instance.currentHour == 20)
        {
            RandomizarNumero();
        }
        if(LevelManager.instance.currentHour == 6)
        {
            spawnDirecao = null;
            enableRandom = true;
            for(int i = 0; i < spawns.Count; i++)
            {
                spawns[i].SetActive(false);
                spawns[i].GetComponent<SpawnSystem>().enableSpawn = false;
            }           
        }
    }
    
  [Server]  
  void SelectionSpawn()
  {
    for(int i = 0; i < spawns.Count; i++)
    {    
        if(i == spawn )
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
    enableRandom = false;    

  }

  [Server]
  public void RandomizarNumero()
  {
    if(enableRandom == true)
    {
        spawn = Random.Range(0,4);
    }
    
    SelectionSpawn();
  }
}
