using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Runtime.CompilerServices;
using UnityEditor;
using Mirror.Authenticators;
using System;
using Random = UnityEngine.Random;
using UnityEngine.AI;

[AddComponentMenu("")]
public class MyNetworkManager : NetworkManager
{
    GameObject orb;
    
    public Transform playerSpawn;
    public static GameObject[] SpawnPoint = new GameObject[4];
    public delegate GameObject SpawnDelegate(Vector3 Pos, System.Guid assetID);
    public delegate void UnspawnDelegate(GameObject spawned);


   

    public override void OnClientConnect(NetworkConnection con)
    {
        base.OnClientConnect(con);

        //ClientScene.RegisterPrefab(orb);
        NetworkServer.SetClientReady(con);
        Debug.Log("ClientConnected!");

        //GameObject player = Instantiate(playerPrefab, playerSpawn);
        //NetworkServer.AddPlayerForConnection(con, player);
        ClientScene.AddPlayer(con);
        
        
    }
      
   
    public override void OnStartServer()
    {
        base.OnStartServer();
        //orb = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Orb"));
        SpawnPoint =  GameObject.FindGameObjectsWithTag("SpawnPoint");
        //spawnOrb();
        
    }



    public override void OnServerAddPlayer(NetworkConnection con)
    {
        GameObject player = Instantiate(playerPrefab, playerSpawn);
        NetworkServer.AddPlayerForConnection(con, player);
        Debug.Log("PlayerSpawning");
        
    } 

    public void spawnOrb()
    {
        
            if (GameObject.FindWithTag("Orb") == null)
            {
                int tempRand = Random.Range(0, 4); 
                Debug.Log(tempRand);
             
                orb = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Orb"), SpawnPoint[tempRand].GetComponentInChildren<Transform>().transform.position, Quaternion.identity);
                NetworkServer.Spawn(orb);

                RpcOrbPosition(orb.transform.position);
        }

    }

    public void RpcOrbPosition(Vector3 pos)
    {
        orb.transform.position = pos; 
    }
    
    public GameObject orbSpawner(Vector3 pos, System.Guid assetId)
    {
        Debug.Log("Spawing Orbs");

        return Instantiate(orb, pos, Quaternion.identity);

    }

    public void orbUnspawner(GameObject SpawnedObject)
    {
        Destroy(SpawnedObject);
    }



}
