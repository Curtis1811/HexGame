using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class FireBall : SpellBehavior
{
    //Everything IN this class will only calculate what the Spells are doing THEY DO NOT NEED TO HAVE DATA;
    public Fireabilities fireabilities;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        abilities = fireabilities;
        //if (isLocalPlayer
        //ProjectileDirection = CalculateDirection(ProjectileDirection,this.gameObject);
        ProjectileDirection = MathFunctions.calculateDirection(this.transform.position, playerWhoSpawned.GetComponent<PlayerMovement>().targetPoint);
        abilities.SPE[0].effectData.onApplyDamageAndKnockBack = SpellHandler.OnApplyKnockBack;
        //fireabilities.Execute(this.gameObject, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       
        Debug.DrawRay(this.transform.position, ProjectileDirection, Color.green, Mathf.Infinity);
     
        if (hasAuthority)
            //This works now but the the Object will not move toward the Proectile Direction as there is no Target Direction on the newly Spawned player camera
            CmdUpdateFireBallPosition(ProjectileDirection);

        if (isServer) 
            RpcTimerDestroy(false);  
        
        //MoveToMouse(ProjectileDirection);
    }

    //This will be where the ability has its effect
    Vector3 CalculateDirection(Vector3 Direction, GameObject gameObject)
    {
        Vector3 tempVector = new Vector3(0, 0, 0);

        float temp = Direction.x / Direction.z;
        float tempX = gameObject.transform.position.x - Direction.x;
        float tempZ = gameObject.transform.position.z - Direction.z;
        //Here we are calculating the Vector from the Angle
        //temp = Mathf.Atan(tempX / tempZ);
        tempVector.x = Mathf.Cos(temp);
        tempVector.z = Mathf.Sin(temp);
        tempVector.y = 0;
        //tempX = Mathf.Cos(Direction.y) * Mathf.Cos(Direction.x);
        //tempZ = Mathf.Cos(Direction.x) * Mathf.Sin(Direction.y);
        //tempZ = Mathf.Sin(Direction.x);
        //tempVector = new Vector3(tempX, 0, tempZ);

        //Debug.Log(tempX + " " + tempZ);
        return tempVector;
    } 

    #region Client 
    //Therse calculations may be better as a static Variable.
    [Command]
    public void CmdUpdateFireBallPosition(Vector3 Direction)
    {
        this.transform.position -= ProjectileDirection * Time.deltaTime * 40; //* Time.deltaTime * 5;
        //Here we need to get the direction of the Direciton vector from the player vector.
        RpcMoveToMouse(this.transform.position);

    }

    [Command(ignoreAuthority = true)]
    public void CmdDespawnFireBall()
    {
        Debug.Log("DEspawned on clients aswell");
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
            abilities.SPE[0].effectData.onApplyDamageAndKnockBack?.Invoke(collision.GetComponent<PlayerMovement>(), this.transform.position, fireabilities.Damage);
        }
        //Destroy Self
        //Check what the FireBall Hit.
        //Deal Damage to Player That has been hit
    }


    [ClientRpc]
    public void RpcMoveToMouse(Vector3 Direction)
    {
        this.transform.position = Direction;
        Debug.Log("Server telling cliunt time to spawn");
        //This updates the Position on the server Side.        
    }

   
    [ClientRpc]
    public void RpcTimerDestroy(bool NotPlayer)
    {
        if (NotPlayer)
        {
            Debug.Log("ServerSideDestroyed NOT PLAYER");
            CmdDespawnFireBall();
            NetworkServer.Destroy(this.gameObject);
        }
        if (Time.time >= timer + abilities.Duration)
        {
            Debug.Log("ServerSideDestroyed");
            //Here I will have to remove the object from the client aswell
            CmdDespawnFireBall();
            NetworkServer.Destroy(this.gameObject);
            timer = Time.time;
        }
    }

    #endregion
    //Make a functioin to destroy after time.
}
