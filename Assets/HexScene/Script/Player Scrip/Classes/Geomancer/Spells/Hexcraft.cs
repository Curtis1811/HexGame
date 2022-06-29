using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Hexcraft : SpellBehavior
{
    // Start is called before the first frame update
    public GameObject PlayerWhoSpawned;
    public EarthAbilities earthAbilities;
    public float SpawnedNID;
    
    void Start()
    {
        timer = NetworkTime.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
            CmdHexcraft();
        
        if(isServer)
            RpcDestroyGameObject();
    }

    [Command]
    void CmdHexcraft(){
        Debug.Log("The object has been spawned. We are going to instnatiate it on the client screne");
        //This tells the server what to do
    }

    [Command]
    void CmdDespanwnGameObject(){
        //This tell the client what to do FROM the server
        ClientScene.UnregisterPrefab(this.gameObject);
        Destroy(this.gameObject);

    }

    [ClientRpc]
    void RpcDestroyGameObject(){
        if(NetworkTime.time >= timer + earthAbilities.Duration){
            Debug.Log("Server Side Destroyed Mudwall");
            CmdDespanwnGameObject();
            //renderer.material.SetFloat("FadeAmount", Mathf.Lerp(1,0,Time.deltaTime));

            NetworkServer.UnSpawn(this.gameObject);
            NetworkServer.Destroy(this.gameObject);
        }
        
    }
    

}
