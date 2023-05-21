using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Dethbox : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

    }

	[ServerCallback]
    private void OnTriggerExit(Collider other)
    {
		//Here we need to send an action to the netowrk Manager to destroy them.
		//NetworkServer.Destroy(other.gameObject);
		//RpcRemovePlayerFromClients(other.gameObject);

	}

	[ClientRpc]
	void RpcRemovePlayerFromClients(GameObject gameObject)
    {
		//NetworkClient.UnregisterPrefab(gameObject);
		//Destroy(gameObject, 0.1f);
	}

}
