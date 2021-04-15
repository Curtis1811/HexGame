using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Meteor : NetworkBehaviour
{
    public GameObject playerWhoSpawned;

    //This is the amount of time the Object is on screen (Encse It doersnt Collide with anything.
    public float timer;
    public Vector3 MovementDirection;
    public Fireabilities fireabilities; // We can maybe assing these when the game is loaded
    public float SpawnedNetId;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        //MovementDirection = calculateDirection(MovementDirection);
        MovementDirection = MathFunctions.calculateDirection(this.transform.position , playerWhoSpawned.GetComponent<PlayerMovement>().targetPoint);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, MovementDirection, Color.green,Mathf.Infinity);
        
        if (hasAuthority)
        {
            CmdMoveMeteor();
        }
        if (isServer)
        {
            RpcDestroyObject(false);
        }
    }

    Vector3 calculateDirection(Vector3 Direction)
    {

        Vector3 temp = new Vector3(0, 0, 0);
        float tempX = Mathf.Cos(Direction.y) * Mathf.Cos(Direction.x);
        float tempY = Mathf.Cos(Direction.x) * Mathf.Sin(Direction.y);
        float tempZ = Mathf.Sin(Direction.x);
        temp = new Vector3(tempX, tempZ, tempY);
        Debug.DrawRay(this.transform.position, Direction, Color.green);
        Debug.Log(temp);
        return temp;
    }


    #region ClientSide
    [Command]
    public void CmdMoveMeteor()
    {
        this.transform.position -= MovementDirection  * Time.deltaTime * 12;
        RpcUpdateMeteorPosition(this.transform.position);
    }

    //Destroy the GameObject
    [Command]
    public void CmdDespawnFireBall()
    {
        Debug.Log("DEspawned on clients aswell");
        ClientScene.UnregisterPrefab(this.gameObject);
        Destroy(this.gameObject);
        
    }

    #endregion


    #region ServerSide
    [ClientRpc]
    public void RpcUpdateMeteorPosition(Vector3 Direction)
    {
        this.transform.position = Direction;
    }

    [ClientRpc]
    public void RpcDestroyObject( bool NotPlayer)
    {
        if (NotPlayer)
        {
            CmdDespawnFireBall();
            NetworkServer.Destroy(this.gameObject);
        }
        if (Time.time >= timer + 10)
        {
            Debug.Log("ServerSideDestroyed");
            //Here I will have to remove the object from the client aswell
            CmdDespawnFireBall();
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
