using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class FireBall : SpellBehavior
{
    //Everything IN this class will only calculate what the Spells are doing THEY DO NOT NEED TO HAVE DATA;
    public Fireabilities fireabilities;
    [SerializeField] float speed;
    
    [SyncVar]
    public float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        ProjectileDirection = new Vector3(x,y,z);
        //timer = Time.time;
        abilities = fireabilities;
        //if (isLocalPlayer
        //ProjectileDirection = CalculateDirection(ProjectileDirection,this.gameObject);
        ProjectileDirection = MathFunctions.calculateDirection(this.transform.position, ProjectileDirection);
        abilities.SPE[0].effectData.onApplyDamageAndKnockBack += SpellHandler.OnApplyKnockBack;
        
        //fireabilities.Execute(this.gameObject, this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, ProjectileDirection, Color.green, Mathf.Infinity);
        if (hasAuthority)
            Direction(ProjectileDirection);
            
            //This works now but the the Object will not move toward the Proectile Direction as there is no Target Direction on the newly Spawned player camera
        if (isServer) 
            RpcTimerDestroy(false);  
        //MoveToMouse(ProjectileDirection);
    }

    void Unsubscribe()
    {
        abilities.SPE[0].effectData.onApplyDamageAndKnockBack -= SpellHandler.OnApplyKnockBack;
    }

    void Direction(Vector3 vector3)
    {
        this.transform.position -= new Vector3(vector3.x, 0 ,vector3.z) * Time.deltaTime * speed;// 0.1f;
        CmdUpdateFireBallPosition(transform.position);
        //CmdUpdateFireBallPosition(this.transform.position);
    }

    

    #region Client 

    //Therse calculations may be better as a static Variable.
    [Command]
    public void CmdUpdateFireBallPosition(Vector3 Direction)
    {
        //Here we need to get the direction of the Direciton vector from the player vector.
        RpcMoveToMouse(Direction);

    }

    [ClientRpc]
    public void RpcMoveToMouse(Vector3 Direction)
    {
        if (hasAuthority)
            return;

        transform.position = Direction;
        //This updates the Position on the server Side.        
    }


    [Command(ignoreAuthority = true)]
    public void CmdDespawnFireBall()
    {
        Debug.Log("Despawned on clients aswell");
        ClientScene.UnregisterPrefab(this.gameObject);
        Destroy(this.gameObject);
    }


    #endregion


    #region Server
    // this will need to check what Object ID "Instantiated" the object to check and see if it should trigger.
    [ServerCallback]
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("This is from the server: " + collision);
        if (collision.transform.tag != "Player")
        {
            RpcTimerDestroy(true);
            
        }
        if (collision.transform.tag == "Player" && collision.transform.GetComponent<NetworkIdentity>().netId != SpawnedNetId)
        {
            Debug.Log("This is from the server: " + collision);
            abilities.SPE[0].effectData.onApplyDamageAndKnockBack?.Invoke(collision.GetComponent<PlayerMovement>(), this.transform.position, fireabilities.KnockBack , fireabilities.Damage);
            //abilities.SPE[0].effectData.on?.Invoke(collision.GetComponent<PlayerMovement>(), this.transform.position, fireabilities.Damage);
        }
        //Destroy Self
        //Check what the FireBall Hit.
        //Deal Damage to Player That has been hit
    }

   
    [ClientRpc]
    public void RpcTimerDestroy(bool NotPlayer)
    {
        // THIS IS CAUSING ISSUES
        if (NotPlayer)
        {
            Debug.Log("ServerSideDestroyed NOT PLAYER");
            CmdDespawnFireBall();
            Unsubscribe();
            NetworkServer.UnSpawn(this.gameObject);
            NetworkServer.Destroy(this.gameObject);
            
        }
        if (NetworkTime.time >= timer + abilities.Duration)
        {
            Debug.Log("ServerSideDestroyed");
            //Here I will have to remove the object from the client aswell
            CmdDespawnFireBall();
            Unsubscribe();
            NetworkServer.UnSpawn(this.gameObject);
            NetworkServer.Destroy(this.gameObject);
            Unsubscribe();
        }
    }

    #endregion

    
    //Make a functioin to destroy after time.
}
