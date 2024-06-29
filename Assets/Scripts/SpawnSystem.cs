using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnSystem : NetworkBehaviour
{
    #region Variables

    [SerializeField] GameObject zombiePrefab;
    [SerializeField] GameObject sprinterPrefab;
    [SerializeField] GameObject bulkerPrefab;
    public Transform spawnPoint;
    public bool enableSpawn;
    public string direcaoSpawn;

    bool bulkerEnable = true;

    int counter = 0;
    float difficulty;

    #endregion

    #region Initialization

    public override void OnStartServer()
    {
        base.OnStartServer();

        InvokeRepeating("SpawnEnemy", 0, 0.75f);
    }

    #endregion

    #region Functions

    [Server]
    void SpawnEnemy()
    {
        if (!LevelManager.instance.nightStart || !enableSpawn) return;

        if (base.ClientManager.Clients.Count > 0)
        {
            counter++;
            difficulty = (10 + LevelManager.instance.currentDay) / LevelManager.instance.currentDay;

            if (counter >= difficulty)
            {
                for (int i = 0; i < base.ClientManager.Clients.Count; i++)
                {
                    // Obtém a posição aleatória dentro da área de spawn.
                    Vector3 randomPosition = GetRandomSpawnPositionWithinBounds(spawnPoint.position, spawnPoint.localScale);

                    // Seleciona diferentes zumbis para spawnar dependendo da dificuldade.
                    if (LevelManager.instance.cureMeter.Value > 70 && bulkerEnable)
                    {
                        SpawnEnemy(bulkerPrefab, randomPosition);
                        bulkerEnable = false;
                        counter = 0;
                        return;
                    }

                    if (Random.Range(0f, 9f) < difficulty * 2) SpawnEnemy(zombiePrefab, randomPosition);
                    else SpawnEnemy(sprinterPrefab, randomPosition);
                }

                counter = 0;
            }
        }
    }

    void SpawnEnemy(GameObject enemy, Vector3 position)
    {
        GameObject enemyInstatiate = Instantiate(enemy, position, Quaternion.identity);
        base.Spawn(enemyInstatiate);
    }

    Vector3 GetRandomSpawnPositionWithinBounds(Vector3 center, Vector3 size)
    {
        // Calcula uma posição aleatória dentro do limite do transform
        float randomX = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float randomY = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);
        float randomZ = Random.Range(center.z - size.z / 2f, center.z + size.z / 2f);

        return new Vector3(randomX, randomY, randomZ);
    }

    #endregion
}

