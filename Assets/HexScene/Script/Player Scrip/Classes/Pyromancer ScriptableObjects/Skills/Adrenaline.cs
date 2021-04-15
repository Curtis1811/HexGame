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
        //fireabilities.SPE[0].effectData = new EffectData();
        abilities.SPE[0].effectData.onEffectEnd += SpellHandler.OnSpeedUpOff;
        abilities.SPE[0].effectData.onEffectBegin += SpellHandler.OnSpeedUp;

        //This even works similarly to a speed boost effect.
        abilities.SPE[0].effectData.onEffectBegin?.Invoke(playerWhoSpawned.GetComponent<PlayerMovement>(), abilities, 3, true);
        //abilities.SPE[0].ExceuteEffect(abilities, playerWhoSpawned.GetComponent<PlayerMovement>(), playerWhoSpawned.GetComponent<PlayerMovement>());
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
            CmdUpdatePosition();

        if (isServer)
            RpcDespawnOnServer();
    }

    #region Client
    [Command]
    void CmdUpdatePosition()
    {
        this.transform.position = playerWhoSpawned.transform.position;
        RpcUpdatePosition();
    }

    void CmdDespawnOnClient()
    {
        abilities.SPE[0].effectData.onEffectEnd?.Invoke(playerWhoSpawned.GetComponent<PlayerMovement>(), abilities, 1, true);
        //This it to unsub from the effect Data as the gameObject is about to be destroyed.
        abilities.SPE[0].effectData.onEffectBegin -= SpellHandler.OnSpeedUp;
        abilities.SPE[0].effectData.onEffectEnd -= SpellHandler.OnSpeedUpOff;
        //This is to destroy the GameObject
        ClientScene.UnregisterPrefab(this.gameObject);
        Destroy(this.gameObject);
    }

    #endregion


    #region Server
        [ClientRpc]
    void RpcUpdatePosition()
    {
        this.transform.position = this.transform.position;
    }

    [ClientRpc]
    void RpcDespawnOnServer()
    {
        if (Time.time >= timer + abilities.Duration)
        {
            Debug.Log("Destroying on ServerSide");
            CmdDespawnOnClient();
            NetworkServer.Destroy(this.gameObject);
            timer = Time.time;
        }
    }

    #endregion

}
