using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RockSmash : SpellBehavior
{
    // Start is called before the first frame update
    
    public GameObject PlayerWhoSpawned;
    public float SpawnedNID;
    //public EarthAbilities WaterAbilities;
    public EarthAbilities earthAbilities;
    public List<GameObject> CollidedPlayer;
    
    [SyncVar]
    float x,y,z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
            CmdRockSmash();
        if(isServer)
            RpcTimerDestroy();
    }   

    [Command]
    void CmdRockSmash(){//here is where we are going to 
        //GameObject temp;
        playerWhoSpawned.GetComponent<PlayerMovement>().CanShoot = false;
        
        
    }

    void Unsubscribe(){


    }

    public void CmdDestroy(){
        ClientScene.UnregisterPrefab(this.gameObject);
        Destroy(this.gameObject);
        playerWhoSpawned.GetComponent<PlayerMovement>().CanShoot = true;
    }
    public void RpcTimerDestroy(){
            if(NetworkTime.time >= timer + abilities.Duration){
            CmdDestroy();
            Unsubscribe();
            NetworkServer.UnSpawn(this.gameObject);
            NetworkServer.Destroy(this.gameObject);

        }
    }

}
