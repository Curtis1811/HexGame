using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EarthClap : SpellBehavior
{
    // Start is called before the first frame update
    public GameObject PlayerWhoSpawned;
    public EarthAbilities earthAbilities;
    public float SpawnedNID;
    public float SpeedOfObjects;
    public List<GameObject> CollidedPlayer;
    public GameObject Clap;

    

    //These are the hands that will move
    [SyncVar]
    [SerializeField] GameObject left; 

    [SyncVar]
    [SerializeField] GameObject right;
    
    [SyncVar]
    public float x, y, z;

    void Start()
    {
        right = transform.Find("Right").gameObject;
        left = transform.Find("Left").gameObject;


        right.GetComponent<BoxCollider>().isTrigger = true;
        left.GetComponent<BoxCollider>().isTrigger = true;
        
        
        timer = NetworkTime.time;
        ProjectileDirection = new Vector3(x, y, z);

        #region DelegateSubscribe
        abilities.SPE[0].effectData.onEffectBegin += SpellHandler.OnEffect_Stun;
        abilities.SPE[1].effectData.onApplyFlatDamage += SpellHandler.OnFlatDamage;
        //abilities.SPE[0].effectData.onEffectBegin += SpellHandler.OnFlatDamage;
        right.GetComponent<WallClapChild>().onColliding += ActivateEffect;
        left.GetComponent<WallClapChild>().onColliding += ActivateEffect;
        #endregion
        
        //Here we are going to spawn 2 gameobjects witht he trigger colliders.        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
            MoveGameObjects(left, this.transform.position,0.2f);
            MoveGameObjects(right, this.transform.position,-0.2f);
        if (isServer)
            RpcTimerDestroy();
            

        //MoveGameObjects(left, new Vector3(0,0,0));
        //MoveGameObjects(right, new Vector3(0,0,0));
    }

    //We want to move the Right and Left objects with this function. We are going to tell the server to run this function and update the position.
    void MoveGameObjects(GameObject ObjectToMove, Vector3 DirectionToMove,float direction){
        //Here we are going to move the gameobjects
        ObjectToMove.transform.Translate(new Vector3(0,0,direction));
        CmdUpdateGameObjectPositions(ObjectToMove.transform.position,left);
        CmdUpdateGameObjectPositions(ObjectToMove.transform.position,right);
    }

    [Command]
    void CmdUpdateGameObjectPositions(Vector3 Position, GameObject gameObject){
        RpcUpdatePosition(Position,gameObject);
    }

    [ClientRpc]
    void RpcUpdatePosition(Vector3 Position, GameObject gameObject){
        //gameObject.transform.position = Position;
    }
    

    void CheckPosition(){
        //here we are going to check the position
        //if the positon is == to what we need. Continue with program

    }

    private void OnTriggerEnter(Collider other) {
        //here we will stun anyobject that enters the trigger and move the object to the centre of origins of the object.

    }

    [Command]
    public void CmdSpawnClap(){

    }
    

    [Command(requiresAuthority = true)]
    public void CmdDespawnClap(){
        NetworkClient.UnregisterPrefab(this.gameObject);
        Destroy(this.gameObject);   
        //Unsubscribe();
        
    }
    void Unsubscribe(){
        
        abilities.SPE[0].effectData.onEffectBegin -= SpellHandler.OnEffect_Stun;
        abilities.SPE[1].effectData.onApplyFlatDamage -= SpellHandler.OnFlatDamage;

        right.GetComponent<WallClapChild>().onColliding -= ActivateEffect;
        left.GetComponent<WallClapChild>().onColliding -= ActivateEffect;
    }


    public void RpcTimerDestroy(){
        if(NetworkTime.time >= timer + abilities.Duration){
            CmdDespawnClap();
            Unsubscribe();
            NetworkServer.UnSpawn(this.gameObject);
            NetworkServer.Destroy(this.gameObject);
        }
    }

    [ServerCallback]
    public void ActivateEffect(GameObject nameOfObject){
        //Currently the direction of the clap is incorrect. Will need to get the Direction of the Moving claps 
        //OR rotate the direction bu 90Degrees <- maybe Easier
        abilities.SPE[0].effectData.onEffectBegin?.Invoke(nameOfObject.GetComponent<PlayerMovement>(),earthAbilities,earthAbilities.Value,false);
        abilities.SPE[1].effectData.onApplyFlatDamage?.Invoke(nameOfObject.GetComponent<PlayerMovement>(), earthAbilities.Damage);

    }
    
    
}
