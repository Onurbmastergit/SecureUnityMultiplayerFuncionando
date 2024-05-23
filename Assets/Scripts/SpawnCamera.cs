using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnCamera :NetworkBehaviour
{
  public GameObject prefabCamera;
  public Transform cameraTransform;      
  public override void OnStartClient()
  {
    base.OnStartClient();
    if(base.IsOwner)
    {
        cameraTransform = GameObject.FindWithTag("TransformCamera").transform;
        Instantiate(prefabCamera, cameraTransform);
    }

  }
}
