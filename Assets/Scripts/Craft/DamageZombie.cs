using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class DamageZombie : NetworkBehaviour
{
 public GameObject targetZombie;
 public SpriteRenderer lightGun;
 public GameObject Gun;
[SerializeField] Transform firingPoint;
[SerializeField] GameObject projectilePrefab;
 public float velocity;
 Color32 corVermelho = new Color32(249, 6, 0, 155);
 Color32 corVerde = new Color32(0, 249, 22, 155);

 void Update()
 {
    if(targetZombie == null)
    {
        Gun.transform.Rotate(0,velocity * Time.deltaTime,0);
        lightGun.color = corVerde;
    }
    else 
    {
        Shoot();
    }
     
    
 }
   void OnTriggerEnter(Collider collider)
   {
    if(collider.CompareTag("Zombie"))
    {
        SelectionTarget();
        Gun.transform.LookAt(targetZombie.transform);    
    }
    else
    {
        targetZombie = null;
    }
   }
    void SelectionTarget()
   {
    lightGun.color = corVermelho;
     GameObject[] targets = GameObject.FindGameObjectsWithTag("Zombie");
      if(targets == null)
    {
        targetZombie = null;
        return;
    }
     targetZombie = targets[0];
   
     foreach( GameObject target in targets)
     {
        //O jOGADOR QUE O INIMIGO ESTA SEGUINDO ESTÁ MAIS LONGE DO QUE O ALVO DO LOOP
        if( Vector3.Distance(transform.position, targetZombie.transform.position)       
            >           
            Vector3.Distance(transform.position, target.transform.position)
          )
        {
            //SE ESTIVER TROCA O JOGADOR QUE O INIMIGO ESTÁ SEGUINDO PELO NOVO ALVO
            targetZombie = target;

        }
     }
   }

   public void Shoot()
    {
        GameObject bulletInstance = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        base.Spawn(bulletInstance);
    }    

}
