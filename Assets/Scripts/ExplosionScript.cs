using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] int danoExplosao;
    [SerializeField] GameObject buildMina;

    void Start()
    {
        buildMina.transform.position = new Vector3(buildMina.transform.position.x, -2, buildMina.transform.position.z);
        transform.GetComponent<Collider>().enabled = true;
        Invoke("DestroyerMine", 1f);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Zombie"))
        {
            collider.GetComponent<EnemyStatus>().ReceberDano(danoExplosao);
            collider.GetComponent<NavMeshAgent>().speed *= 4f;
        }
    }

    void DestroyerMine()
    {
        transform.parent.GetComponent<Mina>().DestroyMine();
    }
}
