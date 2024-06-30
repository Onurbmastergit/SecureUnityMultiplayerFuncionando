using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] GameObject bite;
    [SerializeField] RectTransform transformSpawn; // TransformSpawn deve ser um RectTransform

    private float lastZRotation = 0f; // Variável para armazenar a última rotação

    void Start()
    {
        // Encontra o RectTransform apenas uma vez na inicialização
        transformSpawn = GameObject.FindWithTag("HudZombie").GetComponent<RectTransform>();
    }

    public void SpawnRandomBite()
    {
     // Pega os limites do RectTransform
        Vector2 rectSize = transformSpawn.rect.size;

        // Define o tamanho do bite
        Vector2 biteSize = new Vector2(0.5f, 0.5f);

        // Gera uma posição aleatória dentro dos limites do RectTransform, ajustando para o tamanho do bite
        float randomX = Random.Range(biteSize.x / 2, rectSize.x - biteSize.x / 2);
        float randomY = Random.Range(biteSize.y / 2, rectSize.y - biteSize.y / 2);

        // Cria uma posição local aleatória
        Vector2 localPoint = new Vector2(randomX, randomY);

        // Ajusta a posição local relativa ao pivô
        localPoint -= rectSize * transformSpawn.pivot;

        // Converte a posição local ajustada para coordenadas mundiais
        Vector3 spawnPosition = transformSpawn.TransformPoint(localPoint);

        // Gera uma rotação aleatória no eixo Z entre -45 e 45 graus com base na última rotação
        float randomZRotation = Random.Range(-45f, 45f);
        float newZRotation = lastZRotation + randomZRotation;

        // Mantém a rotação dentro do intervalo de -45 a 45 graus
        if (newZRotation < -45f) newZRotation = -45f;
        if (newZRotation > 45f) newZRotation = 45f;

        // Atualiza a última rotação
        lastZRotation = newZRotation;

        // Instancia o GameObject 'bite' na posição aleatória
        GameObject newBite = Instantiate(bite, spawnPosition, Quaternion.identity);
        newBite.transform.SetParent(transformSpawn, false); // Define o transformSpawn como pai para manter a hierarquia do UI

        // Ajusta a posição do 'bite' para coordenadas locais do 'transformSpawn'
        newBite.transform.localPosition = localPoint;

        // Aplica a rotação aleatória no eixo Z no RectTransform
        newBite.transform.localRotation = Quaternion.Euler(0, 0, newZRotation);

        // Ajusta a escala do 'bite' para o tamanho desejado
        newBite.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        // Define o tempo de vida do 'bite' para 1.5 segundos
        Destroy(newBite, 1.5f);
    }
}