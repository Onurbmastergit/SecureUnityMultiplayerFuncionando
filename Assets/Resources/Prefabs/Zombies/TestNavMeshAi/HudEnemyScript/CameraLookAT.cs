using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAT : MonoBehaviour
{
    public Transform cameraTransform;
    void Start() 
    {
        cameraTransform = GameObject.FindWithTag("MainCamera").transform;
    }
    void Update()
    {   
            transform.LookAt(cameraTransform.position);
    }
}
