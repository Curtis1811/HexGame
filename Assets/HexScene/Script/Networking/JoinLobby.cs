using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Mirror;

public class JoinLobby : NetworkBehaviour
{

    //HERE WE will be listenning for lobbie connect
    // Start is called before the first frame update
    [SerializeField] MyNetworkManager networkmanager = null;
    [SerializeField] private Button ReadyButton = null;

    public delegate void GetData(int tempInt, string[] tempString);
    public event GetData data;
    

    private void OnEnable()
    {
        PlayerData pb = SaveData.loadData();
        networkmanager.GameIsReady += Event_GameIsReady;

    }

   
    private void Event_GameIsReady(NetworkConnection con)
    {
        PlayerData pb = SaveData.loadData();
        data?.Invoke(pb.Class, pb.SaveAbilites);
        Debug.Log("PlayersHaveJoinedGameIsReady " + con.address);
        
    }

    private void getPlayerData()
    {
        PlayerData pb = SaveData.loadData();
        data?.Invoke(pb.Class, pb.SaveAbilites);
    }
        
}
