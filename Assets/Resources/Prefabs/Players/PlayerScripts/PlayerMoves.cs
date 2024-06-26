using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoves : NetworkBehaviour
{
    public float moveSpeed = 10f; // Velocidade de movimento do jogador
    public float rotationSpeed = 10f; // Velocidade de rotação do jogador
     public float rotationCorrection = 0f;
     float velocidadeBase;
    Vector3 movement;
     Vector3 positionPlayer;
    public bool acessibilidade;
    public GameObject rotacao;

    private CharacterController controller; // Componente CharacterController do jogador
    private InputControllers inputController;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(base.IsOwner == false) return;
        transform.GetComponent<PlayerStatus>().vidaAtual = transform.GetComponent<PlayerStatus>().vidaTotal;
        GameObject.Find("RadomCamera").SetActive(false);
        controller = GetComponent<CharacterController>();
        inputController = GetComponent<InputControllers>();
        Vector3 positionPlayer = transform.position;
        positionPlayer.y = 0;
        transform.position = positionPlayer;
        velocidadeBase = moveSpeed;
    }

    void Update()
    {
        if(base.IsOwner == false) return;

       
            MovePlayer(transform.GetComponent<PlayerMoves>().Owner); // Movimentar o jogador
            RotatePlayer(transform.GetComponent<PlayerMoves>().Owner); // Rotacionar o jogador em direção ao cursor
      
        
        //MoveAcess();
    }

    void MovePlayer(NetworkConnection connection)
    {
       if(base.IsOwner == false) return;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Garantir que o movimento seja sempre no espaço mundial
        movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        movement.y = 0; // Não queremos mover no eixo Y

        movement *= moveSpeed * Time.deltaTime;
        controller.Move(movement);


        if (inputController.Run)
        {
            RunPlayer();
        }
        else
        {
            moveSpeed = velocidadeBase;
        }
    }
    void RunPlayer() 
    {
        if(base.IsOwner == false) return;
        moveSpeed = 10 ;
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
        rotacao.transform.rotation = targetRotation * correctionRotation;
    }

    void MoveAcess()
{
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