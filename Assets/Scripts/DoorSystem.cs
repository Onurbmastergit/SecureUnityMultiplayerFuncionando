using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public Animator animator;
    void Update()
    {
        animator.SetBool("open",LevelManager.instance.isDay);
    }
}
