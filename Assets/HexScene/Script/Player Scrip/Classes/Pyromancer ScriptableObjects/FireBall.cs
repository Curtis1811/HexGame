using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//MUST FIX THE DIRECTION CALCULATION.
public class FireBall : NetworkBehaviour
{
    public Pyromancer pyromancer;
    //This may have to be changed to Scripable Objects
    public Vector3 ProjectileDirection;
    Ray ray; 
    public float timer;
    
    public Fireabilities fireabilities;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        //if (isLocalPlayer
        ProjectileDirection = CalculateDirection(ProjectileDirection,this.gameObject);
        fireabilities.Execute(this.gameObject, this.gameObject);
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
        this.transform.position = Direction;
        //This updates the Position on the server Side.        
    }

    //Therse calculations may be better as a static Variable.
    Vector3 CalculateDirection(Vector3 Direction, GameObject gameObject)
    {
        float temp = Direction.x / Direction.z;
        
        float tempX = gameObject.transform.position.x - Direction.x;
        float tempZ = gameObject.transform.position.z - Direction.z;
        //Vector2()
       //Here we are calculating the Vector from the Angle
        temp = Mathf.Atan(tempX / tempZ);
        Debug.Log(Direction);
        tempX = Mathf.Cos(temp);
        tempZ = Mathf.Sin(temp);
        //Debug.Log(tempX + " " + tempZ);
        return new Vector3(tempZ, 0, tempX);
    }

    #endregion


    #region Server
    [ServerCallback]
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        //Destroy Self
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
