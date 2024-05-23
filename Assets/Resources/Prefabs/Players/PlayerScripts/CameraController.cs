using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    #region Variables

    [SerializeField] GameObject camera;
    [SerializeField] GameObject player;

    #endregion

    #region Initialization

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (base.IsOwner)
        {
            player = transform.GetComponentInChildren<GameObject>();
            camera = GameObject.FindWithTag("MainCamera");
            //camera.transform.SetParent(transform);
        }
    }

    void Update()
    {
        FollowPlayer();
    }

    #endregion

    #region Functions

    void FollowPlayer()
    {
        camera.transform.position = player.transform.position;
    }

    #endregion
}
