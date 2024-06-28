using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class DamageZombie : NetworkBehaviour
{
    GameObject targetZombie;
    public SpriteRenderer lightGun;
    public GameObject Gun;
    public float colldownBullets;
    bool canShoot = true;
    [SerializeField] Transform firingPoint;
    [SerializeField] GameObject projectilePrefab;
    public float velocity;
    Color32 corVermelho = new Color32(249, 6, 0, 155);
    Color32 corVerde = new Color32(0, 249, 22, 155);

    void Update()
    {
        if (targetZombie == null)
        {
            Gun.transform.Rotate(0, velocity * Time.deltaTime, 0);
            lightGun.color = corVerde;
        }
        else if (canShoot)
        {
            canShoot = false;
            Shoot();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Zombie"))
        {
            if (collider.GetComponent<EnemyStatus>().vidaAtual <= 0)
            {
                targetZombie = null;
                return;
            }

            lightGun.color = corVermelho;
            Gun.transform.LookAt(collider.transform);
            targetZombie = collider.gameObject;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Zombie"))
        {
            targetZombie = null;
        }
    }

    public void Shoot()
    {
        GameObject bulletInstance = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        base.Spawn(bulletInstance);
        StartCoroutine(bulletsTime());
    }

    IEnumerator bulletsTime()
    {
        yield return new WaitForSeconds(colldownBullets);
        canShoot = true;
    }
}
