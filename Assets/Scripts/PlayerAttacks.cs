using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacks : NetworkBehaviour
{
    #region Variables

    [SerializeField] Transform firingPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] static float fireRate = 0.2f;
    float lastTimeShot = 0;

    [SerializeField] static float slashRate = 0.5f;
    float lastTimeSlash = 0;

    [SerializeField] ParticleSystem muzzleFlash;

    #endregion

    #region Initialization

    void Update()
    {
        if (!base.IsOwner) return;

        Server_ShowGun( base.Owner );
        Server_InputManager( base.Owner );
    }

    #endregion

    #region Functions

    [ServerRpc] // Mostrar a arma agora � processada no servidor, pois a vari�vel isDay s� � computada l�
    void Server_ShowGun( NetworkConnection conn )
    {
        if (LevelManager.instance.isDay.Value)
        {
            Target_ShowGun(conn, false);
            return;
        }

        Target_ShowGun(conn, true);

    }
    // O servidor transfere a vari�vel de l� para cada um dos clients
    [TargetRpc]
    void Target_ShowGun( NetworkConnection conn, bool draw )
    {
        InputControllers.pistol = draw;
    }

    [ServerRpc]
    void Server_InputManager(NetworkConnection conn)
    {
        if (LevelManager.instance.isDay.Value == false)
            Target_InputManager(conn);
    }

    [TargetRpc]
    void Target_InputManager(NetworkConnection conn)
    {
        // Shoot Input.
        if (Input.GetButton("Fire1") || Gamepad.current != null && Gamepad.current.rightTrigger.isPressed)
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
    public void Shoot()
    {
        GameObject bulletInstance = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        base.Spawn(bulletInstance);
        bulletInstance.GetComponent<Projectile>().isPlayer = true;

        muzzleFlash.Play();
    }

    void Slash()
    {
        
    }

    #endregion
}
