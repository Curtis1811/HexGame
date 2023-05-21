using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Adrenaline : SpellBehavior
{
    // Start is called before the first frame update
    public Fireabilities fireabilities;

   
    void Start()
    {
        playerWhoSpawned = NetworkClient.spawned[SpawnedNetId].gameObject;
        
        // I need a way to select the GameObject Via Server
        if (isClient)
            //CmdGetPlayerWhoSpawned();
        if (isServer)
            TargetRpcSetPlayerWhoSpawned();

        //fireabilities.SPE[0].effectData = new EffectData();
        abilities.SPE[0].effectData.onEffectEnd += SpellHandler.OnSpeedUpOff;
        abilities.SPE[0].effectData.onEffectBegin += SpellHandler.OnSpeedUp;
        //This even works similarly to a speed boost effect        
        //abilities.SPE[0].ExceuteEffect(abilities, playerWhoSpawned.GetComponent<PlayerMovement>(), playerWhoSpawned.GetComponent<PlayerMovement>());
                
    }

    // Update is called once per frame
    void Update()
    {
        if (isClient)
            //CmdUpdatePosition(playerWhoSpawned.transform.position);

            if (isServer)
                RpcDespawnOnServer();
                TargetRpcSetTransform();
    }

    #region Client
    
    [Server]
    [TargetRpc]
    void TargetRpcDespawnOnClient()
    {
        abilities.SPE[0].effectData.onEffectEnd?.Invoke(playerWhoSpawned.GetComponent<PlayerMovement>(), abilities, playerWhoSpawned.GetComponent<PlayerMovement>().OriginalScaler, true);
        abilities.SPE[0].effectData.onEffectBegin -= SpellHandler.OnSpeedUp;
        abilities.SPE[0].effectData.onEffectEnd -= SpellHandler.OnSpeedUpOff;
        Debug.Log("Server Destroying on specific Client.");

    }

    [Command]
    void CmdDespawnOnClient()
    {   
        TargetRpcDespawnOnClient();
        abilities.SPE[0].effectData.onEffectEnd?.Invoke(playerWhoSpawned.GetComponent<PlayerMovement>(), abilities, playerWhoSpawned.GetComponent<PlayerMovement>().OriginalScaler, true);
        abilities.SPE[0].effectData.onEffectBegin -= SpellHandler.OnSpeedUp;
        abilities.SPE[0].effectData.onEffectEnd -= SpellHandler.OnSpeedUpOff;

        //This it to unsub from the effect Data as the gameObject is about to be destroyed.
        Debug.Log("Destroying on Destroying on All CLients");
        //This is to destroy the GameObject
        NetworkClient.UnregisterPrefab(this.gameObject);
        Destroy(this.gameObject);
    }

    [ClientRpc]
    void RpcDespawnOnServer()
    {
        if (NetworkTime.time >= timer + abilities.Duration)
        {
            Debug.Log("Stored Time" + timer + abilities.Duration + " Current Time" + Time.time );
            CmdDespawnOnClient();
            NetworkServer.Destroy(this.gameObject);
            timer = Time.time;
        }
    }

    #endregion

    [Server]
    #region Server
    [TargetRpc]
    void TargetRpcSetTransform()
    {
        this.transform.position = NetworkClient.localPlayer.gameObject.transform.position; // The ClientScnee.LocalPlayer Here should be the local players gameobject
        CmdUpdatePosition(this.transform.position);
    }

    [Command]
    void CmdUpdatePosition(Vector3 vector)
    {
        this.transform.position = vector;
        RpcUpdatePosition(this.transform.position);
    }

    [ClientRpc]
    void RpcUpdatePosition(Vector3 pos)
    {
        // This Send all the clients the correct Position for this GameObject
        this.transform.position = pos;
        //Pos is the Position that everyclient will get.
    }

    [Command]
    void CmdGetPlayerWhoSpawned()
    {
        TargetRpcSetPlayerWhoSpawned();
    }

    [TargetRpc]
    void TargetRpcSetPlayerWhoSpawned()
    {
        Debug.Log("PlayerSet");
        playerWhoSpawned = NetworkClient.localPlayer.gameObject;
        abilities.SPE[0].effectData.onEffectBegin?.Invoke(playerWhoSpawned.GetComponent<PlayerMovement>(), abilities, 4, true);
    }

    #endregion
    

    #region Sync Events
    //Here we are going to create a SyncEvent

    #endregion

}
