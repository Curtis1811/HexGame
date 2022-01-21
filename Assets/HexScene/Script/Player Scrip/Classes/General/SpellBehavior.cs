using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//THIS CLASS IS PURLEY FOR DATA
public class SpellBehavior : NetworkBehaviour
{
    [Header("Spell Data")]
    public Vector3 ProjectileDirection;
    public Abilities abilities;
    
    public GameObject playerWhoSpawned;
    //This may have to be changed to Scripable Objects
    [SyncVar]
    public double timer;
    [SyncVar]
    public uint SpawnedNetId; 
    
    
}
