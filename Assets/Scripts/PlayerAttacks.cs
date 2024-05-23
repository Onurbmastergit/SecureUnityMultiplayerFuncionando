using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class PlayerAttacks : NetworkBehaviour
{
    #region Variables

    [SerializeField] Transform firingPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] static float fireRate = 0.2f;
    float lastTimeShot = 0;

    [SerializeField] static float slashRate = 0.5f;
    float lastTimeSlash = 0;

    #endregion

    #region Initialization

    void Update()
    {
        if (!base.IsOwner) return;

        ShowGun();
        InputManager();
    }

    #endregion

    #region Functions

    void ShowGun()
    {
        if (LevelManager.instance.isDay)
        {
            InputControllers.pistol = false;
            return;
        }
        InputControllers.pistol = true;
       
    }

    void InputManager()
    {
        if (LevelManager.instance.isDay) return;

        // Shoot Input.
        if (Input.GetButton("Fire1"))
        {
            if (lastTimeShot + fireRate <= Time.time)
            {
                lastTimeShot = Time.time;
                Shoot();
            }
        }

        // Slash Input.
        if (Input.GetButton("Fire2"))
        {
            if (lastTimeSlash + slashRate <= Time.time)
            {
                lastTimeSlash = Time.time;
                Slash();
            }
        }
    }

    [ServerRpc]
    void Shoot()
    {
        GameObject bulletInstance = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        base.Spawn(bulletInstance);
    }

    void Slash()
    {
        
    }

    #endregion
}
