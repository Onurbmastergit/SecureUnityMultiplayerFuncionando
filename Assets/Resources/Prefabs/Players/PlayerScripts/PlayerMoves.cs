using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoves : NetworkBehaviour
{
    #region Variables

    [Header("Player Velocity")]
    public float moveSpeed; // Velocidade de movimento do jogador
    float walkSpeed = 9f;
    float runSpeed = 12f;

    [Header("Corrections")]
    public float rotationSpeed = 0f; // Velocidade de rotação do jogador
    public float rotationCorrection = 0f;
    Vector3 movement;

    [Header("Player Options")]
    public bool acessibilidade;

    [Header("Player Rotation")]
    public GameObject rotacao;

    CharacterController controller; // Componente CharacterController do jogador
    InputControllers inputController;

    #endregion

    #region Initialization

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner == false) return;
        transform.GetComponent<PlayerStatus>().vidaAtual = transform.GetComponent<PlayerStatus>().vidaTotal;
        GameObject.Find("RandomCamera").SetActive(false);
        controller = GetComponent<CharacterController>();
        inputController = GetComponent<InputControllers>();
    }

    void Update()
    {
        if (base.IsOwner == false) return;

        MovePlayer(transform.GetComponent<PlayerMoves>().Owner); // Movimentar o jogador
        RotatePlayer(transform.GetComponent<PlayerMoves>().Owner); // Rotacionar o jogador em direção ao cursor
    }

    #endregion

    #region Functions

    void MovePlayer(NetworkConnection connection)
    {
        if (base.IsOwner == false) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveY = (transform.position.y > 0.14) ? -0.1f : 0;

        Vector3 movement = new Vector3(moveHorizontal, moveY, moveVertical);

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        if (inputController.Run)
        {
            RunPlayer();
        }
        else
        {
            moveSpeed = walkSpeed;
        }
        movement *= moveSpeed * Time.deltaTime;// Não queremos mover no eixo Y

        controller.Move(movement);

    }

    void RunPlayer()
    {
        moveSpeed = runSpeed;
    }

    void RotatePlayer(NetworkConnection connection)
    {
        Vector3 mousePosition = Input.mousePosition;

        // Converter a posição do mouse de pixels para coordenadas do mundo
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.position.y - transform.position.y));

        // Calcular a direção para a qual o jogador deve se orientar
        Vector3 lookDirection = mousePosition - transform.position;
        lookDirection.y = 0f; // Garantir que o jogador não rotacione no eixo Y

        // Rotacionar o jogador diretamente em direção à direção calculada sem suavização
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        // Aplicar a correção de rotação
        Quaternion correctionRotation = Quaternion.Euler(0f, rotationCorrection, 0f);
        transform.rotation = targetRotation * correctionRotation;
    }

    #endregion
}