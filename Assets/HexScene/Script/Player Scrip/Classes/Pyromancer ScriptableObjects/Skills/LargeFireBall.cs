using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class LargeFireBall : SpellBehavior
{
    //This may have to be changed to Scripable Objects
    public Fireabilities fireabilities;
    [SerializeField] float speed;

    [SyncVar] public float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        ProjectileDirection = new Vector3(x,y,z);
        abilities = fireabilities;
        //timer = Time.time;
        //if (isLocalPlayer
        //This works out the Direction of the Vector
        ProjectileDirection = MathFunctions.calculateDirection(this.transform.position, ProjectileDirection);
        abilities.SPE[0].effectData.onApplyDamageAndKnockBack += SpellHandler.OnApplyKnockBack;
        
        //Debug.Log(ProjectileDirection);
        //fireabilities.Execute(this.gameObject, this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, ProjectileDirection, Color.green, Mathf.Infinity);
        if (isOwned)
            Direction(ProjectileDirection);
            //This works now but the the Object will not move toward the Proectile Direction as there is no Target Direction on the newly Spawned player camera
        
        if (isServer)
            //TargetRpcMoveToMouse();
            RpcTimerDestroy(false);
            //MoveToMouse(ProjectileDirection);
    }

    void Unsubscribe()
    {
        abilities.SPE[0].effectData.onApplyDamageAndKnockBack -= SpellHandler.OnApplyKnockBack;
    }
    //This will be where the ability has its effect

    void Direction(Vector3 vector3)
    {
        this.transform.position -= new Vector3(vector3.x, 0, vector3.z) * Time.deltaTime * speed;// * 0.1f;
        //Vector3 test = new Vector3(vector3.x, 0, vector3.z) * Time.deltaTime * speed;
        CmdUpdateFireBallPosition(transform.position);
    }

    #region Client 
    [Command]
    public void CmdUpdateFireBallPosition(Vector3 Pos)
    {
        //Here we need to get the direction of the Direciton vector from the player vector.
        RpcMoveToMouse(Pos);
    }

    [ClientRpc]
    public void RpcMoveToMouse(Vector3 Direction)
    {
        if (isOwned)
            return;

        transform.position = Direction;
        //This updates the Position on the server Side.        
    }

    [Command(requiresAuthority = true)]
    public void CmdDespawnFireBall()
    {
        Debug.Log("DEspawned on clients aswell");
        NetworkClient.UnregisterPrefab(this.gameObject);
        Destroy(this.gameObject);
    }
    //Therse calculations may be better as a static Variable.
    #endregion


    #region Server
  
    // this will need to check what Object ID "Instantiated" the object to check and see if it should trigger.
    [ServerCallback]
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.transform.tag != "Player")
        {
            Debug.Log("Collision with something thats not the player: " + collision);
            RpcTimerDestroy(true);
        }
        if (collision.transform.tag == "Player" && collision.transform.GetComponent<NetworkIdentity>().netId != SpawnedNetId)
        {
            Debug.Log("This is from the server: " + collision);
            abilities.SPE[0].effectData.onApplyDamageAndKnockBack?.Invoke(collision.GetComponent<PlayerMovement>(), this.transform.position, fireabilities.KnockBack , fireabilities.Damage);
            // This is where we will do the effect, Maybe changed.
        }
        //Destroy Self
        //Check what the FireBall Hit.
        //Deal Damage to Player That has been hit

    }

    [ClientRpc]
    public void RpcTimerDestroy(bool NotPlayer)
    {
        if (NotPlayer)
        {
            Debug.Log("ServerSideDestroyed NOT PLAYER");
            CmdDespawnFireBall();
            NetworkServer.UnSpawn(this.gameObject);
            NetworkServer.Destroy(this.gameObject);
            Unsubscribe();
        }
        if (NetworkTime.time >= timer + fireabilities.Duration)
        {
            Debug.Log("ServerSideDestroyed");
            //Here I will have to remove the object from the client aswell
            CmdDespawnFireBall();
            NetworkServer.UnSpawn(this.gameObject);
            NetworkServer.Destroy(this.gameObject);
            Unsubscribe();
        }
    }


    #endregion
    //Make a functioin to destroy after time.
}
