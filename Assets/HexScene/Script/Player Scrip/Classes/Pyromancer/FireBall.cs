using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FireBall : NetworkBehaviour
{
    public Pyromancer pyromancer;
    //This may have to be changed to Scripable Objects
    public Vector3 ProjectileDirection;
    Ray ray; 
    public float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        //if (isLocalPlayer
        ProjectileDirection = CalculateDirection(ProjectileDirection);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 targetPoint = ray.GetPoint(0);
        //ProjectileDirection = ProjectileDirection.normalized;
        //this.transform.Translate(ProjectileDirection.x, 0, ProjectileDirection.z, Space.World);
        //This does work but we will need to add a new camera
        if (hasAuthority)
        {
            //This works now but the the Object will not move toward the Proectile Direction as there is no Target Direction on the newly Spawned player camera
            CmdUpdateFireBallPosition(ProjectileDirection);
        }
        if (isServer) { 
            RpcTimerDestroy();  
        }
        if (isClient)
        {
            //CmdUpdateFireBallPosition(ProjectileDirection.normalized);
            //CmdMoveFireBallOnClient(ProjectileDirection.normalized);
            //RpcMoveToMouse(ProjectileDirection.normalized);

            //CmdDespawnFireBall();
        }
        //MoveToMouse(ProjectileDirection);
    }

    //This will be where the ability has its effect
    public void AbilityEffect()
    {
        Debug.Log("The Ability has hit");
    }

    #region Client 

    [ClientRpc]
    public void RpcMoveToMouse(Vector3 Direction)
    {
        //Normalizing we get the distrance We can add a scaler on top of it.
        //this.transform.Translate(new Vector3(Direction.x, 0, Direction.y), Space.World);
        this.transform.position = Direction;
        //This updates the Position on the server Side.
        
        
    }

    Vector3 CalculateDirection(Vector3 Direction)
    {
        float temp = Direction.x / Direction.z;
        
        //Vector3.Magnitude(Vector3.Distance(this.transform.position, Direction));
        float tempX = this.transform.position.x - Direction.x;
        float tempZ = this.transform.position.z - Direction.z;
        //Here we are calculating the Vector from the Angle
        temp = Mathf.Atan(tempX / tempZ);
        tempX = Mathf.Sin(temp);
        tempZ = Mathf.Cos(temp);
        
        return new Vector3(tempX,0,tempZ);
    }

    #endregion


    #region Server
    [ServerCallback]
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        //destroy Self
        //Check what the FireBall Hit.
        //Deal Damage to Player That has been hit
        AbilityEffect();
        //Destroy(this);
    }

    // this will need to check what Object ID "Instantiated" the object to check and see if it should trigger.
    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        //NetworkServer.Destroy(this.gameObject);
        //ClientScene.UnregisterPrefab(this.gameObject);
        //Destroy(this.gameObject);
    }

    [Command]
    public void CmdUpdateFireBallPosition(Vector3 Direction)
    {
        this.transform.position += Direction * Time.deltaTime * 5;
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

    [ClientRpc]
    public void RpcTimerDestroy()
    {
        if (Time.time >= timer + 2 /*filerball scripable prefab*/)
        {
            Debug.Log("boom");
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
