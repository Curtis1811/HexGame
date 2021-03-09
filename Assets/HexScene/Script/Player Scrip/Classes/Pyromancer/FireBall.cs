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
        ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        timer = Time.time;
        this.transform.position = GetComponent<Transform>().position;
        
        //if (isLocalPlayer)

    }

    // Update is called once per frame
    void Update()
    { 
        Vector3 targetPoint = ray.GetPoint(0);
        ProjectileDirection = ProjectileDirection.normalized;
        this.transform.Translate(ProjectileDirection.x, 0, ProjectileDirection.z, Space.World);
        //This does work but we will need to add a new camera

        if (isServer) {
            RpcTimerDestroy();
            RpcUpdateFireBallPosition(this.transform.position);
        }
        if (isClient)
        {
            //CmdMoveToMouse(ProjectileDirection);
            //CmdDespawnFireBall();
        }
        //MoveToMouse(ProjectileDirection);
    }


    //This will be where the ability has its effect
    public void AbilityEffect()
    {
        Debug.Log("The Ability has hit");
    }

    public void setMousePosition(Vector3 mousePos)
    {
        //Vector3 aimDirection = mousePos - //player Transform.Position
        //ProjectileDirection = mousePos;
        //pyromancer.onMouseClick -= setMousePosition;
        Debug.Log(mousePos);
    }


    #region Client 

    [Command]   
    public void CmdMoveToMouse(Vector3 Direction)
    {
        Debug.Log(Direction);
        //Normalizing we get the distrance We can add a scaler on top of it.
        this.transform.Translate(new Vector3(Direction.x, 0, Direction.y), Space.World);
        //This updates the Position on the server Side.
        RpcUpdateFireBallPosition(this.transform.position);
        
    }

    [ClientRpc]
    public void RpcSpanwFireBall()
    {
        //ClientScene.RegisterPrefab(this.gameObject);
        Debug.Log("RPC SpawnFireBall on Client Scene");
    }

    #endregion


    #region Server
    
    [ServerCallback]
    private void OnCollisionEnter(Collision collision)
    {
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
        Debug.Log(other.tag);
        //NetworkServer.Destroy(this.gameObject);
        //ClientScene.UnregisterPrefab(this.gameObject);
        //Destroy(this.gameObject);
    }

    [ClientRpc]
    public void RpcUpdateFireBallPosition(Vector3 Location)
    {
        if (isLocalPlayer)
            return;

        this.transform.position = Location;
        //We will need to update all of the clients aswell
        //Here the Vector Direction And Add this to the clients 
    }
    
    [Command]
    public void CmdDespawnFireBall()
    {
        Destroy(this.gameObject); 
        ClientScene.UnregisterPrefab(this.gameObject);
    }

    [ClientRpc]
    public void RpcTimerDestroy()
    {
        if (Time.time >= timer + 2 /*filerball scripable prefab*/)
        {
            Debug.Log("boom");
            //Here I will have to remove the object from the client aswell
            NetworkServer.Destroy(this.gameObject);
            CmdDespawnFireBall();
            timer = Time.time;
        }
    }

    #endregion
    //Make a functioin to destroy after time.
}
