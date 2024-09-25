using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    Vector3 firingPoint;
    
    [SerializeField] float projectileSpeed;
    [SerializeField] float maxProjectileDistance;

    public bool isPlayer;

    void Start()
    {
        firingPoint = transform.position;
    }

    void Update()
    {
        MovePorjectile();
    }
    
    void MovePorjectile()
    {
        if (Vector3.Distance(firingPoint, transform.position) > maxProjectileDistance)
        {
            Destroy(gameObject);
            base.Despawn(gameObject);
        }

        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Zombie"))
        {
            
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            boxCollider.size *= 2.0f;

            if (isPlayer) collider.GetComponent<EnemyStatus>().ReceberDano(LevelManager.instance.playerDamage.Value);
            else collider.GetComponent<EnemyStatus>().ReceberDano(10);

            Destroy(gameObject);
            
            base.Despawn(gameObject);
        }

        if (collider.transform.CompareTag("Environment"))
        {
            Destroy(gameObject);
            base.Despawn(gameObject);
        }
    }
}
