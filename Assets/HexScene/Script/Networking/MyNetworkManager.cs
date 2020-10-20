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
    private List<int> PlayerID;
    //EventScript evs;
    [Scene] [SerializeField] public string LobbyScene = string.Empty;




    public EventScript evnt;

    public int tempInt;
    public string[] tempString = new string[4];


    public delegate void GameReady(NetworkConnection con); //
    public event GameReady GameIsReady; // this is kinda like a list of people that are listening to the event

      

  // -- Server -- \\
    public override void OnStartServer()
    {
        base.OnStartServer();
        SpawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
               
    }

    public override void OnServerAddPlayer(NetworkConnection con)
    {
        evnt = GetComponent<EventScript>();
        GameObject player = Instantiate(playerPrefab, playerSpawn);
        assingPlayerClass(player);
        //Here we need to add class component to the player. May have to take more permaters in to AssignPlayerClass 
        NetworkServer.AddPlayerForConnection(con, player);
        tempInt = 0;
        tempString = null;
        evnt.data -= Evnt_DataAssing;
    }

    // -- Client -- \\

    public override void OnClientConnect(NetworkConnection con)
    {
        base.OnClientConnect(con);
        GameIsReady?.Invoke(con);
        evnt = GetComponent<EventScript>();

        // When a client Connects a Ready event is fired (Invoked)
        //Debug.Log("ClientConnected!");
       
        evnt.data += Evnt_DataAssing;
        
        //evnt.SetReady += Evnt_SetReady;
        
        //ClientScene.AddPlayer(con);

        //The Client Connects this runs
        //evnt.data += DataAssing;
        //PlayerID.Add(con.connectionId);
        //This is to keep trach of the connection ID may not need
        
    }

    
    public override void OnStartClient()
    {
        base.OnStartClient();
        evnt = GetComponent<EventScript>();
        evnt.data += Evnt_DataAssing;

    }



    //here is where we assing players their Class From the Server
    public void assingPlayerClass(GameObject player)
    {
        Debug.Log(tempString);
        switch(tempInt)
        {
            case 1:
                Debug.Log("Pyromancer");
                //PyromancerHandler pro = new PyromancerHandler(tempString);
                player.AddComponent<PyromancerHandler>();
                player.GetComponent<PyromancerHandler>().abilityData = tempString;//.abilityData = tempString;
                //player.GetComponent<Pyromancer>().AbilityNames = tempString;
                break;
            
            case 2:
                Debug.Log("New Class Added Hydromancer");
                break;
            
            case 3:
                break;
            
            case 4:
                break;
            
            case 5:
                break;
            default:
                Debug.Log("NoClass");
                break;
        }
    }



    // -- Events -- \\

    private void Evnt_DataAssing(int ttempInt, string[] ttempString)
    {
        tempInt = ttempInt;
        tempString = ttempString;
    }

    private void Evnt_SetReady(NetworkConnection con)
    {
        if (!con.isReady)
        {
            Debug.Log("not ready");
        }
        else
        {
            EventScript evnt = GetComponent<EventScript>();
            evnt.SetReady -= Evnt_SetReady;
            Debug.Log("ready");
        }
    }

}

