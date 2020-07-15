using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbRespawn : NetworkBehaviour {

    public GameObject Orb;
    public Transform SpawnPoint;


    private void Start()
    {
        
    }

    [Server]
    private void Update()
    {
      
    }

    void spawn()
    {

    }


}
