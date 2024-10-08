using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float followSpeed = 5f; // Velocidade de seguir a câmera
    public Vector3 offset; // Distância do objeto em relação à câmera

    private Transform mainCameraTransform;
    void Update()
    {
        if(mainCameraTransform == null)
        {
            mainCameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        if(mainCameraTransform != null)
        {
        // Calcular a posição desejada com o offset
        Vector3 targetPosition = mainCameraTransform.position + offset;

        // Mover o objeto em direção à posição da câmera com atraso
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
       }
    }
}
