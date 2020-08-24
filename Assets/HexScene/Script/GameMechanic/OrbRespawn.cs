using Mirror;
using Mirror.Websocket;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class OrbRespawn : NetworkBehaviour {

    //[SyncVar]
    [SerializeField] GameObject orb;    
    [SerializeField] List<GameObject> spawnpoint = new List<GameObject>();

    

    [SyncVar]
    int random;

    private void Start()
    {
        
        
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        spawnpoint = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList<GameObject>();
        //InvokeRepeating("SpwnOrb", 1f, 3f);

    }

    [Server]
    public void Update()
    {

        RpcSpwnOrb();
    }


    [ClientRpc]
    public void RpcSpwnOrb()
    {
        
        if (GameObject.FindWithTag("Orb") == null)
        {
            random = Random.Range(0, 4);
            
            Debug.Log(random);

            GameObject orbInstance = Instantiate(orb, spawnpoint[random].GetComponentInChildren<Transform>().transform.position, Quaternion.identity);
            ClientScene.RegisterPrefab(orbInstance);

            //orbInstance.AddComponent<NetworkTransform>();
            NetworkServer.Spawn(orbInstance);
            
        }


    }

   
}
