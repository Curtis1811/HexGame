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

    public GameObject pyromancer;

    //EventScript evs;
    
    [Scene] [SerializeField] public string LobbyScene = string.Empty;

    //public EventScript evnt;
    //Reason why not working is Server is instantiated before Client. Get Server to initialize first.
    
    public int tempInt;
    public string[] tempString = new string[4];

    public delegate void GameReady(NetworkConnection con); 
    public event GameReady GameIsReady; // this is kinda like a list of people that are listening to the event

    
    // -- Server -- \\
    public override void OnStartServer()
    {
        base.OnStartServer();
        SpawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        //evnt = FindObjectOfType<EventScript>();
        NetworkServer.RegisterHandler<Notification>(OnNotification);
        //evnt.data += Evnt_DataAssing;

    }
    //Eventually I would like to store this in variables that take in the player connection and assign the correct player component. This will be donea fter the lobby system.
    public void OnNotification(Notification Message)
    {
        tempString = Message.An;
        tempInt = Message.Class;

        //Debug.Log("Recieved");
        
    }
     

    //This is the script to add the player to the server
    public override void OnServerAddPlayer(NetworkConnection con)
    {
       
        //GameObject player;
        //player = Instantiate(playerPrefab, playerSpawn);
        assingPlayerClass(con);
        //Here we need to add class component to the player. May have to take more permaters in to AssignPlayerClass 
        //ClientScene.RegisterPrefab(player);
        //NetworkServer.AddPlayerForConnection(con, player);
      
    }
    

    // -- Client -- \\
    public override void OnClientConnect(NetworkConnection con)
    {
        base.OnClientConnect(con);
        //GameIsReady?.Invoke(con);
        //evnt = GetComponent<EventScript>();
        Debug.Log("ClientHasConnected");
        ClientScene.AddPlayer(con);


        // When a client Connects a Ready event is fired (Invoked)
        //Debug.Log("ClientConnected!");
        //PlayerID.Add(con.connectionId);
        //evnt.data += Evnt_DataAssing;

        //evnt.SetReady += Evnt_SetReady;
        //The Client Connects this runs
        //evnt.data += DataAssing;
        //This is to keep trach of the connection ID may not need
        
    }

    
    
    public override void OnStartClient()
    {
        
        //Debug.Log("ClientHasStarted");
    }


    
    //here is where we assing players their Class From the Server
    public void assingPlayerClass(NetworkConnection con)
    {
       
        GameObject player;
        switch (tempInt)
        {
            case 1:
                
                player = Instantiate(pyromancer, playerSpawn);
                player.GetComponent<PyromancerHandler>().abilityData = tempString;
                ClientScene.RegisterPrefab(player);
                NetworkServer.AddPlayerForConnection(con, player);
                
                break;
            
            case 2:
                Debug.Log("New Class Added Hydromancer");
                break;
            
            case 3:
                Debug.Log("New Class Added Aeromancer");
                break;
            
            case 4:
                Debug.Log("New Class Added Geomancer");
                break;
      
            default:
                Debug.Log("NoClass");
                break;
        }
    }


    // -- Events -- //
    public void Evnt_DataAssing(int ttempInt , string[] ttempString)
    {
        /*
        for(int i = 0; i < tempString.Length; i++)
        {
            Debug.Log(ttempString[i]);
            tempString[i] = ttempString[i];
        }
        tempInt = ttempInt;
        */
        Debug.Log("Data Assigned");
    }

    /*private void Evnt_SetReady(NetworkConnection con)
    {
        if (!con.isReady)
        {
            Debug.Log("not ready");
        }
    
        else
        {
            //EventScript evnt = GetComponent<EventScript>();
            evnt.SetReady -= Evnt_SetReady;
            Debug.Log("ready");
        }
    }

    */

}

