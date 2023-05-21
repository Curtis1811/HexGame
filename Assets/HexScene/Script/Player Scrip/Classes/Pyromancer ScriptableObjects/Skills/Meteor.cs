using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Meteor : SpellBehavior
{
    
    //This is the amount of time the Object is on screen (Encse It doersnt Collide with anything.
    public Fireabilities fireabilities; // We can maybe assing these when the game is loaded
    public float speed;
    [SyncVar]
    public float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        ProjectileDirection = new Vector3(x, y, z);
        //MovementDirection = calculateDirection(MovementDirection);
        ProjectileDirection = MathFunctions.calculateDirection(this.transform.position , ProjectileDirection);
        speed = 20;
        //abilities.SPE[0].effectData.onApplyDamageAndKnockBack += SpellHandler.OnApplyKnockBack;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, ProjectileDirection, Color.green,Mathf.Infinity);
        
        if (hasAuthority)
        {
            Direction(ProjectileDirection);
        }
        if (isServer)
        {
            RpcDestroyObject(false);
        }
    }

    void Direction(Vector3 vector3)
    {
        this.transform.position -= vector3 * Time.deltaTime * speed;// * 0.1f;
        CmdMoveMeteor(this.transform.position);
    }


    #region ClientSide
    [Command]
    public void CmdMoveMeteor(Vector3 movementDirec)
    {
        RpcUpdateMeteorPosition(movementDirec);
    }

    //Destroy the GameObject
    [Command]
    public void CmdDespawnFireBall()
    {
        Debug.Log("DEspawned on clients aswell");
        NetworkClient.UnregisterPrefab(this.gameObject);
        Destroy(this.gameObject);
        
    }

    #endregion


    #region ServerSide
    [ClientRpc]
    public void RpcUpdateMeteorPosition(Vector3 Direction)
    {
        if (!isOwned)
            return;

        this.transform.position = Direction;
    }

    [ClientRpc]
    public void RpcDestroyObject(bool NotPlayer)
    {
        if (NotPlayer)
        {
            CmdDespawnFireBall();
            NetworkServer.UnSpawn(this.gameObject);
            NetworkServer.Destroy(this.gameObject);
        }
        if (NetworkTime.time >= timer + fireabilities.Duration)
        {
            Debug.Log("ServerSideDestroyed");
            //Here I will have to remove the object from the client aswell
            CmdDespawnFireBall();
            NetworkServer.UnSpawn(this.gameObject);
            NetworkServer.Destroy(this.gameObject);
            timer = Time.time;
        }
    }
        

    [ServerCallback]
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("This is from the server: " + collision);
        if (collision.transform.tag == "Player" && collision.transform.GetComponent<NetworkIdentity>().netId != SpawnedNetId)
        {
            Debug.Log("Log");
            
            //fireabilities.Execute(this.transform.gameObject, this.transform.gameObject); // THIS NEED TO BE CHANGED
        }
        if (collision.transform.tag != "Player")
        {
            RpcDestroyObject(true);
        }      
    }
  
    #endregion
}
