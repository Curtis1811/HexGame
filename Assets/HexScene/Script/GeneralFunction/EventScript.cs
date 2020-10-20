using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;
public class EventScript : NetworkBehaviour
{

    public delegate void GetData(int tempInt,string []tempString);
    public delegate void clientReady(NetworkConnection con);
    
    public event GetData data;
    public event clientReady SetReady;

    NetworkConnection con;
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        PlayerData pd = SaveData.loadData();
        data?.Invoke(pd.Class, pd.SaveAbilites);

    }
  
   

    private void Update() { 
    
            //PlayerData pd = SaveData.loadData();
            //data?.Invoke(pd.Class, pd.SaveAbilites);
            SetReady?.Invoke(con);
                
        //GetPlayerData?.Invoke(this, new DataForConnectionArgs { Class = pd.Class}); 
    }

  
}

