using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class Mina : NetworkBehaviour 
{
    public GameObject explosioArea;
    #region Instatiation

    /// <summary>
    /// Create Mina inside parent.
    /// </summary>
    public static Mina Create(Transform _parent, Vector3 _position)
    {
        Mina reference = Resources.Load<Mina>("Prefabs/Craft/BuildMina");
        Mina instance = Instantiate(reference, _parent);


        instance.transform.position = _position;

        return instance;
    }

    #endregion
     void OnTriggerEnter(Collider collider)
     {
        if(collider.CompareTag("Zombie")) explosioArea.SetActive(true);
     }

     public void DestroyMine()
     {
        Destroy(gameObject);
     }
     
}
