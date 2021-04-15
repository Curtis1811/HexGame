using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//THIS CLASS IS PURLEY FOR DATA
public class SpellBehavior : NetworkBehaviour
{
    [Header("Spell Data")]
    [SyncVar]
    public Vector3 ProjectileDirection;
    public Abilities abilities;
    [SyncVar]
    public GameObject playerWhoSpawned;
    //This may have to be changed to Scripable Objects
    public float timer;
    [SyncVar]
    public uint SpawnedNetId;
    
    public float X;
    public float Y;
    public float Z;
    
    
}
