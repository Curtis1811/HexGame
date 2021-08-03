using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Whirlpool : SpellBehavior
{
   
    // Start is called before the first frame update
    public WaterAbilities WaterAbilities;
    public List<GameObject> CollidedPlayer;
    public int testInt;
    [SyncVar]
    public float x, y, z;

    void Start()
    {
        ProjectileDirection = new Vector3(x, y, z);
        //CmdSpawnedPosition();
        abilities.SPE[0].effectData.onEffectEnd += SpellHandler.OnSpeedUpOff;
        abilities.SPE[0].effectData.onEffectBegin += SpellHandler.OnSpeedUp;
        abilities.SPE[0].effectData.onApplyDamageAndKnockBack += SpellHandler.OnApplyKnockBack;
        
        //CmdSpawnedPosition();
        //Testing both approaches
        //this.waterAbilities.SPE[0].onEffect += SpellHandler.OnEffect_Heal;
        //SpellHandler.onEffect += SpellHandler.OnEffect_Heal;
        //waterAbilities.SPE[0].onEffect += SpellHandler.OnEffect_Stun;
    }

   

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
            Direction(ProjectileDirection);

        if (isServer)
            RpcDetroyGameobject();
    }

    void Unsubscribe()
    {
        abilities.SPE[0].effectData.onEffectEnd -= SpellHandler.OnSpeedUpOff;
        abilities.SPE[0].effectData.onEffectBegin -= SpellHandler.OnSpeedUp;
    }

    void Direction(Vector3 vector)
    {
        this.transform.position = vector;
        CmdSpawnedPosition(this.transform.position);
    }
    #region Client 

    [Command(ignoreAuthority = true)]
    public void CmdDespawnGameObject()
    {
        Debug.Log("DEspawned on clients aswell");
        ClientScene.UnregisterPrefab(this.gameObject);
        Destroy(this.gameObject);
    }
    #endregion


    #region Sever
    [ClientRpc]
    void RpcSpawnedPosition(Vector3 spawnedpos)
    { 
        this.transform.position = spawnedpos;
    }

    [Command]
    void CmdSpawnedPosition(Vector3 vector3)
    {
        RpcSpawnedPosition(vector3);
    }

    [ClientRpc]
    void RpcDetroyGameobject()
    {
        if (NetworkTime.time >= timer + WaterAbilities.Duration)
        {
            for (int i=0; i < CollidedPlayer.Count; i++)
            {
                abilities.SPE[0].effectData.onEffectEnd?.Invoke(CollidedPlayer[i].GetComponent<PlayerMovement>(), abilities, 1f, true);
                abilities.SPE[0].effectData.onApplyDamageAndKnockBack?.Invoke(CollidedPlayer[i].GetComponent<PlayerMovement>(), this.transform.position, WaterAbilities.KnockBack, WaterAbilities.Damage);
            }

            //THis will unsub the GameObject to the Attack MAY NEED TO BE LOOKED INTO.
            Unsubscribe();
            Debug.Log("ServerSideDestroyed");
            //Here I will have to remove the object from the client aswell
            CmdDespawnGameObject();
            NetworkServer.UnSpawn(this.gameObject);
            NetworkServer.Destroy(this.gameObject);
                
        }
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player") // && collision.transform.GetComponent<NetworkIdentity>().netId != playerNID)
        {
            CollidedPlayer.Add(collision.gameObject);
            Debug.Log("Collision Detected");
            //waterAbilities.Execute(playerWhoSpawned.GetComponent<PlayerMovement>(), collision.transform.GetComponent<PlayerMovement>());
            abilities.SPE[0].effectData.onEffectBegin?.Invoke(collision.GetComponent<PlayerMovement>(), abilities, 0.5f,true); // This is to slow    
        }
    }

    [ServerCallback]
    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag == "Player") // && collision.transform.GetComponent<NetworkIdentity>().netId != playerNID)
        {
            CollidedPlayer.Remove(collision.gameObject);
            Debug.Log("Collision Detected");
            //waterAbilities.Execute(playerWhoSpawned.GetComponent<PlayerMovement>(), collision.transform.GetComponent<PlayerMovement>());
            abilities.SPE[0].effectData.onEffectEnd?.Invoke(collision.GetComponent<PlayerMovement>(), abilities, 1f,true);

        }

    }

    //WE are going to need to Sync come Events across Clients to make this work

    #endregion
}
