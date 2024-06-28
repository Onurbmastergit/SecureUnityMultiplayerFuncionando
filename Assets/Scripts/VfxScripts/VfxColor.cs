using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxColor : MonoBehaviour
{
    public ObjectStatus status;
    public EnemyStatus enemyStatus;
    public PlayerStatus playerStatus;
    public LabStatus labStatus;
    Color32 corVermelho = new Color32(249, 6, 0, 255); // Red = F90600
    Color32 corVerde = new Color32(0, 249, 22, 255);   // Green = 00F916
    Color32 corAmarelo = new Color32(249, 192, 0, 255);// Yellow = F9C000

    void Start()
    {
        status = gameObject.GetComponent<ObjectStatus>();
    }

    public void ChangeColor()
    {
        if (playerStatus != null) Player();

        if (enemyStatus != null) Enemy();

        if (status != null) Object();

        if (labStatus != null) Base();

    }

    void Base()
    {
        if (LevelManager.instance.labHealth.Value <= labStatus.vidaBase * 0.2f) // Menos de 20% de vida (Vermelho)
        {
            labStatus.lifeBaseStatus.color = corVermelho;
            labStatus.bgIconLifeStatus.color = corVermelho;
            labStatus.neoBgLifeHud.color = corVermelho;
        }
        else if (LevelManager.instance.labHealth.Value >= labStatus.vidaBase * 0.8f) // Mais de 80% de vida (Verde)
        {
            labStatus.lifeBaseStatus.color = corVerde;
            labStatus.bgIconLifeStatus.color = corVerde;
            labStatus.neoBgLifeHud.color = corVerde;
        }
        else // Entre 20% e 80% de vida (Amarelo)
        {
            labStatus.lifeBaseStatus.color = corAmarelo;
            labStatus.bgIconLifeStatus.color = corAmarelo;
            labStatus.neoBgLifeHud.color = corAmarelo;
        }
    }

    void Player()
    {
        if (playerStatus.vidaAtual <= playerStatus.vidaTotal * 0.2f) // Menos de 20% de vida (Vermelho)
        {
            playerStatus.BarLifeStatus.color = corVermelho;
        }
        else if (playerStatus.vidaAtual >= playerStatus.vidaTotal * 0.8f) // Mais de 80% de vida (Verde)
        {
            playerStatus.BarLifeStatus.color = corVerde;
        }
        else // Entre 20% e 80% de vida (Amarelo)
        {
            playerStatus.BarLifeStatus.color = corAmarelo;
        }
    }

    void Object()
    {
        if (status.vidaAtual <= status.vidaTotal * 0.2f) // Menos de 20% de vida (Vermelho)
        {
            status.vidaObject.color = corVermelho;
        }
        else if (status.vidaAtual >= status.vidaTotal * 0.8f) // Mais de 80% de vida (Verde)
        {
            status.vidaObject.color = corVerde;
        }
        else // Entre 20% e 80% de vida (Amarelo)
        {
            status.vidaObject.color = corAmarelo;
        }
    }

    void Enemy()
    {
        if (enemyStatus.vidaAtual <= enemyStatus.vidaBase * 0.2f) // Menos de 20% de vida (Vermelho)
        {
            enemyStatus.LifeBar.color = corVermelho;
        }
        else if (enemyStatus.vidaAtual >= enemyStatus.vidaBase * 0.8f) // Mais de 80% de vida (Verde)
        {
            enemyStatus.LifeBar.color = corVerde;
        }
        else // Entre 20% e 80% de vida (Amarelo)
        {
            enemyStatus.LifeBar.color = corAmarelo;
        }
    }
}
