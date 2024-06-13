using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnCamera :NetworkBehaviour
{
    public GameObject prefabCamera;
    public GameObject prefabVidaStatus;
    public Transform cameraTransform;
    public Transform hudTransform;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            hudTransform = GameObject.Find("HUD").transform;
            cameraTransform = GameObject.FindWithTag("TransformCamera").transform;
            Instantiate(prefabCamera, cameraTransform);
            Instantiate(prefabVidaStatus, hudTransform);
        }
    }
}
