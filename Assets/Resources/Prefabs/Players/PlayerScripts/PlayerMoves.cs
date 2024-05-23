using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class PlayerMoves : NetworkBehaviour
{
    public float moveSpeed = 10f; // Velocidade de movimento do jogador
    public float rotationSpeed = 10f; // Velocidade de rotação do jogador
    Vector3 movement;
    public bool acessibilidade;

    private CharacterController controller; // Componente CharacterController do jogador
    private InputControllers inputController;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(base.IsOwner == false) return;
        controller = GetComponent<CharacterController>();
        inputController = GetComponent<InputControllers>();
    }

    void Update()
    {
        if(base.IsOwner == false) return;
        if(acessibilidade == false)
        {
        MovePlayer(transform.GetComponent<PlayerMoves>().Owner); // Movimentar o jogador
        RotatePlayer(transform.GetComponent<PlayerMoves>().Owner); // Rotacionar o jogador em direção ao cursor
        }
       

        if(acessibilidade == true)
        {
            MoveAcess(transform.GetComponent<PlayerMoves>().Owner);
        }
    }

    void MovePlayer(NetworkConnection connection)
    {
       if(base.IsOwner == false) return;
        // Obter entrada do teclado para movimento
        float moveHorizontal = inputController.movimentoHorizontal;
        float moveVertical = inputController.movimentoVertical;

        // Calcular a direção de movimento
        movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = transform.TransformDirection(movement); // Converter para coordenadas locais
        movement *= moveSpeed * Time.deltaTime;
       
        // Mover o jogador usando o CharacterControlle
        controller.Move(movement);
        movement.Normalize();
        if (inputController.Run)
        {
            RunPlayer();
        }
    }
    void RunPlayer() 
    {
        if(base.IsOwner == false) return;
        controller.Move(movement * 5 * Time.deltaTime);
        movement.Normalize();
    }

    void RotatePlayer(NetworkConnection connection)
    {
       // Obter a posição do mouse na tela
    Vector3 mousePosition = Input.mousePosition;

    // Converter a posição do mouse de pixels para coordenadas do mundo
    mousePosition = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, GameObject.Find("Main Camera").GetComponent<Camera>().transform.position.y - transform.position.y));

    // Calcular a direção para a qual o jogador deve se orientar
    Vector3 lookDirection = mousePosition - transform.position;
    lookDirection.y = 0f; // Garantir que o jogador não rotacione no eixo Y

    // Rotacionar o jogador diretamente em direção à direção calculada sem suavização
    Quaternion rotation = Quaternion.LookRotation(lookDirection);
    transform.rotation = rotation;
    }

    void MoveAcess(NetworkConnection connection)
    {
    if(base.IsOwner == false) return;
      // Obter entrada do teclado para movimento
    float moveHorizontal = inputController.movimentoHorizontal;
    float moveVertical = inputController.movimentoVertical;

    // Calcular a direção de movimento
    movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
    movement = transform.TransformDirection(movement); // Converter para coordenadas locais
    movement *= moveSpeed * Time.deltaTime;

    // Mover o jogador usando o CharacterController
    controller.Move(movement);

    if (movement != Vector3.zero)
    {
        // Calcular a direção para a qual o jogador deve se orientar com base no vetor de movimento
        Quaternion targetRotation = Quaternion.LookRotation(movement);

        // Suavizar a rotação usando Slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    if (inputController.Run)
    {
        RunPlayer();
    }
    }
}