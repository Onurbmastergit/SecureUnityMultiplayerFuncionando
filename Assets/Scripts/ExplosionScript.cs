using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] int danoExplosao;

    void Start()
    {
        transform.GetComponent<Collider>().enabled = true;
        Invoke("DestroyerMine", 3);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Zombie"))
        {
            collider.GetComponent<EnemyStatus>().ReceberDano(danoExplosao);
        }
    }

    void DestroyerMine()
    {
        transform.parent.GetComponent<Mina>().DestroyMine();
    }
}
