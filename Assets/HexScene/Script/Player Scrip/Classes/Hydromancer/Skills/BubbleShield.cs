using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BubbleShield : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayerWhoSpawned;
    public float SpawnedNID;
    public WaterAbilities WaterAbilities;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
            CmdUpdatePosition();
    }

    #region Client
    [Command]
    void CmdUpdatePosition()
    {
        this.transform.position = PlayerWhoSpawned.transform.position;
        RpcUpdatePosition();
    }


    #endregion



    #region Server
    [ClientRpc]
    void RpcUpdatePosition()
    {
        this.transform.position = this.transform.position;
    }

    #endregion


}
