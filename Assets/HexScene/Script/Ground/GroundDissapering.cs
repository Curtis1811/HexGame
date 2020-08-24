using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Mirror;
using Mirror.Websocket;
using System.Security.Policy;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System.Management.Instrumentation;


public class GroundDissapering : NetworkBehaviour
{
    //GameObject[] groundObjects;

    [SerializeField] GameObject HexPrefab;
    [SerializeField] List<GameObject> HexPrefabList = new List<GameObject>();


    [SerializeField] List<GameObject> hex;

    [SerializeField] GameObject[] SolidGround;

    [SyncVar]
    [SerializeField] int storedRand;

    float time;


    public override void OnStartServer()
    {
        base.OnStartServer();
        hex = GameObject.FindGameObjectsWithTag("Ground").ToList<GameObject>();
        time = Time.time;
       
        if(isServer)
            RpcSpawnHex();
       

        int rand = Random.Range(0, HexPrefabList.Count);
        storedRand = rand;
    }

    public override void OnStartClient()
    {
        
        //base.OnStartClient();
        SolidGroundColor();
        hex = GameObject.FindGameObjectsWithTag("Ground").ToList<GameObject>();
        CmdSpawnHex(); 

    }

    [Server]
    private void Start()
    {
       

    }


    [Server]
    private void Update()
    {
        
        // Change this to check if All Clients are Ready; then run
        if (isServer) {
            //RpcchangeColor();
            if (Time.time <= time + 5)
            {
                //RpcchangeColor();
                RpcChangeColor();
                return;
            }
            else
            {
                time = Time.time;
                //hexFunc();
                RpcClientHexRemoval(storedRand);
                
            }
        }
        else
        {
           
        }

    }

   

    [Server]
    public void hexFunc()
    {
        float time;
        time = Time.time;

        if (hex.Count > 0)
        {
            int rand;

            rand = Random.Range(0, hex.Count);
            storedRand = rand;

            if (isServer)
                
                RpcChangeColor();



            hex[rand].gameObject.GetComponentInChildren<Collider>().enabled = false;
            hex[rand].gameObject.GetComponentInChildren<Renderer>().enabled = false;
            hex.RemoveAt(rand);
            
            RpcClientHexRemoval(rand);
            
            
        }
    }

    [ClientRpc]
    void RpcChangeColor()
        {
       

        if (HexPrefabList[storedRand].gameObject.GetComponent<Renderer>().material.color != Color.red)
            {
                HexPrefabList[storedRand].gameObject.GetComponent<Renderer>().material.color = Color.red;

            }
           
        }

    
    void SolidGroundColor()
    {
        int count = 0;
        SolidGround = GameObject.FindGameObjectsWithTag("SolidGround");
        
        foreach (GameObject GO in SolidGround)
        {
            count++;
            SolidGround[count-1].GetComponentInChildren<Renderer>().material.color = Color.yellow;
        }
    }


  


    [ClientRpc]
    public void RpcSpawnHex()
    {
        while (HexPrefabList.Count < 3) {
            
            GameObject go = Instantiate(HexPrefab, new Vector3(4, 4, 4), Quaternion.identity);
            go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            ClientScene.RegisterPrefab(go);
            NetworkServer.Spawn(go);
            HexPrefabList.Add(go);
            HexPrefabList[HexPrefabList.Count-1].transform.position += new Vector3(HexPrefabList.Count, HexPrefabList.Count, HexPrefabList.Count);
            Debug.Log("Pog");
        }
        int rand = Random.Range(0, HexPrefabList.Count);
        storedRand = rand;

    }

    [ClientRpc]
    public void RpcClientHexRemoval(int StoredRand)
    {
        if (HexPrefabList.Count > 0 ) { 
            
            HexPrefabList[StoredRand].gameObject.GetComponent<Renderer>().enabled = false;
            NetworkServer.UnSpawn(HexPrefabList[StoredRand]);
            HexPrefabList.RemoveAt(StoredRand);
            int rand = Random.Range(0, HexPrefabList.Count);
            storedRand = rand;

        }


    }

    public void CmdSpawnHex() {
        RpcSpawnHex();
    }

}

