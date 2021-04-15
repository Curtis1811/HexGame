using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class WaterWave : SpellBehavior
{

    public Vector3 TransformDirection;
    public WaterAbilities WaterAbilities;
    float SpeedOfProjectile;
    public List<GameObject> CollidedPlayer;
    //This is the waterWaveConstrustor. This should be added to all Abilities
    // Start is called before the first frame update
    void Start()
    {
        SpeedOfProjectile = 1.5f;
        TransformDirection = MathFunctions.calculateDirection(this.transform.position, playerWhoSpawned.GetComponent<PlayerMovement>().targetPoint);
        abilities.SPE[0].effectData.onEffectBegin += SpellHandler.OnEffect_Stun;   //This will be where we will assing the Wave with The Clensing Debuff. 

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, TransformDirection, Color.green, Mathf.Infinity);

        if (hasAuthority)
            CmdUpdatePosition();
    }


    #region Client

    [Command]
    void CmdUpdatePosition()
    {
        this.transform.position -= TransformDirection * Time.deltaTime * SpeedOfProjectile;
        RpcSendPositionToServer(this.transform.position);
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
            abilities.SPE[0].effectData.onEffectBegin?.Invoke(collision.GetComponent<PlayerMovement>(), WaterAbilities, 2, false);
            Debug.Log("WaterWave Collision Activated");
        }
    }
    #endregion

}
