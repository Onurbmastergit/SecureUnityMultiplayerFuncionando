
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    Transform player;
    float offsetDistanceY;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        offsetDistanceY = transform.position.y;
    }

    void Update()
    {
        // Follow player - camera offset
        transform.position = player.position + new Vector3(0, offsetDistanceY, 0);
    }
}