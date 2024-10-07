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
    public bool gamepadController;
    public RectTransform mouseUi; 
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
        mouseUi = GameObject.FindWithTag("MouseUI").GetComponent<RectTransform>();
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
    Vector3 targetPosition = Vector3.zero;
    Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

    if (gamepadController)
    {
        // Se o gamepadController estiver ativo, pegamos a posição do mouseUi
        Canvas canvas = mouseUi.GetComponentInParent<Canvas>();

        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            // Caso o Canvas esteja no modo Screen Space Overlay, convertemos a posição para ScreenPoint diretamente
            targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseUi.position.x, mouseUi.position.y, mainCamera.transform.position.y - transform.position.y));
        }
        else if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
        {
            // Para Screen Space Camera ou World Space, usamos RectTransformUtility para converter
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(mainCamera, mouseUi.position);
            targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, mainCamera.transform.position.y - transform.position.y));
        }
    }
    else
    {
        // Se gamepadController estiver desativado, pegamos a posição normal do mouse
        targetPosition = Input.mousePosition;
        targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(targetPosition.x, targetPosition.y, mainCamera.transform.position.y - transform.position.y));
    }

    // Calcular a direção para a qual o jogador deve se orientar
    Vector3 lookDirection = targetPosition - transform.position;
    lookDirection.y = 0f; // Garantir que o jogador não rotacione no eixo Y

    // Rotacionar o jogador diretamente em direção à direção calculada sem suavização
    Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

    // Aplicar a correção de rotação
    Quaternion correctionRotation = Quaternion.Euler(0f, rotationCorrection, 0f);
    transform.rotation = targetRotation * correctionRotation;
}
    }

    #endregion
