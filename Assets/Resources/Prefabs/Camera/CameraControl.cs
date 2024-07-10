
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    #region Variables

    Transform player;
    float offsetDistanceY;

    float elapsedTime = 0f;

    #endregion

    #region Initialization

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        offsetDistanceY = transform.position.y;
    }

    void Update()
    {
        if (LevelManager.instance.endgame.Value)
        {
            EndgameCinematic();
            return;
        }

        // Follow player - camera offset
        transform.position = player.position + new Vector3(0, offsetDistanceY, 0);
    }

    #endregion

    #region Funtions

    void EndgameCinematic()
    {
        // Verificar se a interpolação terminou
        if (transform.position != Vector3.zero)
        {
            // Incrementar o tempo decorrido
            elapsedTime += Time.deltaTime;

            // Calcular a posição intermediária usando Lerp
            transform.position = Vector3.Lerp(transform.position, Vector3.zero, elapsedTime / 6f);
        }
    }

    #endregion
}