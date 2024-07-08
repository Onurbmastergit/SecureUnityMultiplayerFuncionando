using FishNet.Demo.AdditiveScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public Animator animator;
    bool player = false;
    bool zombie = false;

    void Update()
    {
        if (player) return;
        animator.SetBool("isOpen",LevelManager.instance.isDay.Value);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !zombie)
        {
            player = true;
            animator.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = false;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Zombie"))
        {
            zombie = true;
            Invoke("NoZombie", 2f);
        }
    }

    void NoZombie()
    {
        zombie = false;
    }
}
