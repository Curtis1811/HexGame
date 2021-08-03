using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class WaterWave : SpellBehavior
{
    public WaterAbilities WaterAbilities;
    float SpeedOfProjectile;
    public List<GameObject> CollidedPlayer;
    [SyncVar]
    public float x, y, z;

    //This is the waterWaveConstrustor. This should be added to all Abilities
    // Start is called before the first frame update
    void Start()
    {
        timer = NetworkTime.time;
        ProjectileDirection = new Vector3(x,y,z);
        SpeedOfProjectile = 2.5f;
        ProjectileDirection = MathFunctions.calculateDirection(this.transform.position, ProjectileDirection);
        abilities.SPE[0].effectData.onEffectBegin += SpellHandler.OnEffect_Stun;   //This will be where we will assing the Wave with The Clensing Debuff. 
        abilities.SPE[0].effectData.onApplyDamageAndKnockBack += SpellHandler.OnApplyKnockBack;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, ProjectileDirection, Color.green, Mathf.Infinity);

        if (hasAuthority)
            Direction(ProjectileDirection);
        if (isServer)
            RpcDestroyGameObject();
    }


    void Direction(Vector3 vector)
    {
        this.transform.position -= vector * Time.deltaTime * SpeedOfProjectile;
        CmdUpdatePosition(this.transform.position);
    }

    void Unsubscribe()
    {

    }

    #region Client

    [Command]
    void CmdUpdatePosition(Vector3 vector)
    {
        //this.transform.position -= TransformDirection * Time.deltaTime * SpeedOfProjectile;
        RpcSendPositionToServer(vector);
    }

    #endregion

    #region Server
    [ClientRpc]
    public void RpcSendPositionToServer(Vector3 PositionOnClient)
    {
        this.transform.position = PositionOnClient;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player" && collision.transform.GetComponent<NetworkIdentity>().netId != SpawnedNetId)
        {
            abilities.SPE[0].effectData.onEffectBegin?.Invoke(collision.GetComponent<PlayerMovement>(), WaterAbilities, 2.5f, false);
            abilities.SPE[0].effectData.onApplyDamageAndKnockBack?.Invoke(collision.GetComponent<PlayerMovement>(), this.transform.position, WaterAbilities.KnockBack, WaterAbilities.Damage);
            Debug.Log("WaterWave Collision Activated");
        }
    }

    [ClientRpc]
    void RpcDestroyGameObject()
    {
        if(NetworkTime.time >= timer + WaterAbilities.Duration)
        {
            for(int i = 0; i < CollidedPlayer.Count; i++)
            {

            }

            abilities.SPE[0].effectData.onApplyDamageAndKnockBack -= SpellHandler.OnApplyKnockBack;
            abilities.SPE[0].effectData.onEffectBegin -= SpellHandler.OnEffect_Stun;

            Debug.Log("Server Side Destroyed");
            CmdDespanwnGameObject();
            NetworkServer.UnSpawn(this.gameObject);
            NetworkServer.Destroy(this.gameObject);
        }
    }

    [Command]
    void CmdDespanwnGameObject()
    {
        Debug.Log("Despawned on clients aswell");
        ClientScene.UnregisterPrefab(this.gameObject);
        Destroy(this.gameObject);
    }
    #endregion
}
