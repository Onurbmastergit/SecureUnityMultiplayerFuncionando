using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SirenScript : MonoBehaviour
{
    public float velocityRotation;
    public static bool alert;
    private float currentRotationY = 0f;
    public GameObject EffectsSiren;
    public GameObject LightSiren;

    void Update()
    {
        if (!alert) return;

        if (LevelManager.instance.currentHour.Value >= 6)
        {
            DisableAlert();
            return;
        }

        EffectsSiren.SetActive(true);
        currentRotationY += velocityRotation * Time.deltaTime;
        EffectsSiren.transform.rotation = Quaternion.Euler(0, currentRotationY, 0);
    }

    public void SirenAlert()
    {
        alert = true;
    }

    public void DisableAlert()
    {
        EffectsSiren.SetActive(false);
        alert = false;
    }
}
