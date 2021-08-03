using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MudWall : NetworkBehaviour {
    public GameObject PlayerWhoSpawned;
    public float SpawnedNID;
    //public EarthAbilities WaterAbilities;
    public EarthAbilities earthAbilities;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (hasAuthority)
            CmdSpawnWall();
    }

    [Command]
    void CmdSpawnWall(){//here is where we are going to 
        //GameObject temp;
    }



}
