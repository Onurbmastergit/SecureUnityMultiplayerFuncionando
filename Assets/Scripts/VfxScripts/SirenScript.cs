using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SirenScript : MonoBehaviour
{
    float velocityRotation = 100f;
    public static bool alert;
    private float currentRotationY = 0f;
    public GameObject EffectsSiren;
    float lastLabHealth;

    void Start()
    {
        lastLabHealth = LevelManager.instance.labHealth.Value;
    }

    void Update()
    {
        if (LevelManager.instance.isDay)
        {
            EffectsSiren.SetActive(false);
            return;
        }

        if (LevelManager.instance.labHealth.Value < lastLabHealth)
        {
            lastLabHealth = LevelManager.instance.labHealth.Value;
            SirenAlert();
        }

        if (!alert)
        {
            EffectsSiren.SetActive(false);
        }

        currentRotationY += velocityRotation * Time.deltaTime;
        EffectsSiren.transform.rotation = Quaternion.Euler(0, currentRotationY, 0);
    }

    public void SirenAlert()
    {
        EffectsSiren.SetActive(true);
        alert = true;
        Invoke("DisableAlert", 5f);
    }

    public void DisableAlert()
    {
        alert = false;
    }
}
