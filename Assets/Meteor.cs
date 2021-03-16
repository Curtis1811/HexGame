using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Meteor : NetworkBehaviour
{
    //This is the amount of time the Object is on screen (Encse It doersnt Collide with anything.
    public float timer;
    public Vector3 MovementDirection;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        MovementDirection = calculateDirection(MovementDirection);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            CmdMoveMeteor(MovementDirection);
        }
        if (isServer)
        {
            RpcDestroyObject();
        }
    }

    Vector3 calculateDirection(Vector3 Direction)
    {
        Vector3 temp = new Vector3(0, 0, 0);
        float tempX = Mathf.Cos(Direction.y)*Mathf.Cos(Direction.x);
        float tempY = Mathf.Cos(Direction.x)*Mathf.Sin(Direction.y);
        float tempZ = Mathf.Sin(Direction.x);
        temp = new Vector3(tempX, tempY, tempZ);

        Debug.Log(temp);
        return temp;
    }


    #region ClientSide
    [Command]
    public void CmdMoveMeteor(Vector3 Direction)
    {

        Direction += this.transform.position * Time.deltaTime * 3;
        
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
    public void RpcDestroyObject()
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("This is from the server: " + collision);
    }

    #endregion
}
