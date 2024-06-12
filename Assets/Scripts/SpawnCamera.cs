using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnCamera :NetworkBehaviour
{
  public GameObject prefabCamera;
  public GameObject prefabPlaceBuild;
  public Transform cameraTransform; 
   public Transform buildTransform;     
  public override void OnStartClient()
  {
    base.OnStartClient();
    if(base.IsOwner)
    {
        cameraTransform = GameObject.FindWithTag("TransformCamera").transform;
        buildTransform = GameObject.FindWithTag("PlaceBuild").transform;
        Instantiate(prefabCamera, cameraTransform);
        GameObject placeBuild = Instantiate(prefabPlaceBuild, buildTransform);
        base.Spawn(placeBuild);
    }

  }
}
