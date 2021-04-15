using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//MUST FIX THE DIRECTION CALCULATION.
public class LargeFireBall : NetworkBehaviour
{
    public GameObject playerWhoSpawned;
    //This may have to be changed to Scripable Objects
    public Vector3 ProjectileDirection;
    Ray ray;
    public float timer;
    public Fireabilities fireabilities;
    public float SpawnedNetId;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        //if (isLocalPlayer
        ProjectileDirection = MathFunctions.calculateDirection(this.transform.position, playerWhoSpawned.GetComponent<PlayerMovement>().targetPoint);
        //fireabilities.Execute(this.gameObject, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            //This works now but the the Object will not move toward the Proectile Direction as there is no Target Direction on the newly Spawned player camera
            CmdUpdateFireBallPosition();
        }
        if (isServer)
        {
            RpcTimerDestroy();
        }
        //MoveToMouse(ProjectileDirection);
    }

    //This will be where the ability has its effect
    public void AbilityEffect()
    {
        Debug.Log("The Ability has hit");
    }

    Vector3 CalculateDirection(Vector3 Direction, GameObject gameObject)
    {
        Vector3 tempVector = new Vector3(0, 0, 0);

        float temp = Direction.x / Direction.z;
        float tempX = gameObject.transform.position.x - Direction.x;
        float tempZ = gameObject.transform.position.z - Direction.z;
        //Here we are calculating the Vector from the Angle
        temp = Mathf.Atan(tempX / tempZ);
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
    [Command]
    public void CmdUpdateFireBallPosition()
    {
        this.transform.position -= ProjectileDirection * Time.deltaTime * 30;
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

  
    //Therse calculations may be better as a static Variable.
  

    #endregion


    #region Server
    [ServerCallback]
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("This is from the server: " + collision);
        if (collision.transform.tag == "Player" && collision.transform.GetComponent<NetworkIdentity>().netId != SpawnedNetId)
        {
            Debug.Log("Log");
            collision.transform.gameObject.GetComponent<PlayerMovement>().health = 1;
            //fireabilities.Execute();
        }

    }

    // this will need to check what Object ID "Instantiated" the object to check and see if it should trigger.
    [ServerCallback]
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.transform.tag != "Player")
        {
            //Do stuff
        }
        if (collision.transform.tag == "Player" && collision.transform.GetComponent<NetworkIdentity>().netId != SpawnedNetId)
        {
            Debug.Log("This is from the server: " + collision);
            collision.transform.gameObject.GetComponent<PlayerMovement>().health = 1;
            AbilityEffect();// This is where we will do the effect, Maybe changed.
        }
        //Destroy Self
        //Check what the FireBall Hit.
        //Deal Damage to Player That has been hit

    }


    [ClientRpc]
    public void RpcMoveToMouse(Vector3 Direction)
    {
        this.transform.position = Direction;
        //This updates the Position on the server Side.        
    }

    [ClientRpc]
    public void RpcTimerDestroy()
    {
        if (Time.time >= timer + fireabilities.CoolDown)
        {
            Debug.Log("ServerSideDestroyed");
            //Here I will have to remove the object from the client aswell
            CmdDespawnFireBall();
            NetworkServer.Destroy(this.gameObject);
            timer = Time.time;
        }
    }

    [TargetRpc]
    public void TargetgetCurrentObjectPosition(Vector3 Location)
    {
        Debug.Log(Location);
        //this.transform.position = Location;
    }

    #endregion
    //Make a functioin to destroy after time.
}
