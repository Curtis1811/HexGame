using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FireBall : NetworkBehaviour
{
    //This may have to be changed to Scripable Objects
    Vector3 ProjectileDirection;
    Ray ray; 
    public float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        timer = Time.time;
        this.transform.position = GetComponent<Transform>().position;
        ProjectileDirection = Input.mousePosition.normalized;

        if (isLocalPlayer)
            CmdSpawnFireBall();

    }

    // Update is called once per frame
    void Update()
    {
        if (isServer) {

            RpcTimerDestroy();
        }   

        if (isClient)
        {
            CmdMoveToMouse(ProjectileDirection);
            CmdDespawnFireBall();
        }
                
        //MoveToMouse(ProjectileDirection);
    }


    //This will be where the ability has its effect
    public void AbilityEffect()
    {
        Debug.Log("The Ability has hit");
    }

    

    #region Client 

    [Command]   
    public void CmdMoveToMouse(Vector3 Direction)
    {
        //Normalizing we get the distrance We can add a scaler on top of it.
        this.transform.Translate(new Vector3(Direction.x, 0, Direction.y), Space.World);
        RpcUpdateFireBallPosition(this.transform.position);
        Debug.Log(Direction);
    }

    [Command]
    public void CmdSpawnFireBall()
    {
        NetworkServer.Spawn(this.gameObject);
        Debug.Log("Gello");
        RpcSpanwFireBall();
    }
    [ClientRpc]
    public void RpcSpanwFireBall()
    {
        ClientScene.RegisterPrefab(this.gameObject);

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
        //Here the Vector Direction And Add this to the clients 
    }
    

    
    [Command]
    public void CmdDespawnFireBall()
    {
        Destroy(this.gameObject);
    }

    [ClientRpc]
    public void RpcTimerDestroy()
    {
        if (Time.time >= timer + 2 /*filerball scripable prefab*/)
        {
            Debug.Log("boom");
            //Here I will have to remove the object from the client aswell
            NetworkServer.Destroy(this.gameObject);
            ClientScene.UnregisterPrefab(this.gameObject);
            CmdDespawnFireBall();
            timer = Time.time;
        }
    }

    #endregion

    //Make a functioin to destroy after time.

}
