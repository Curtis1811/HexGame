using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class EventScript : NetworkBehaviour
{
    //The messagges will eventually be moved to a lobby system
    public delegate void GetData(int tempInt ,string []tempString);
    public event GetData data;

    //public delegate void clientReady(NetworkConnection con);
    //public event clientReady SetReady;
    //NetworkConnection con;


    public void Start()
    {   
        PlayerData pd = SaveData.loadData();
        SendThisToServer(pd.Class, pd.SaveAbilites);

        Debug.Log("Running");
    }



 /* 
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        PlayerData pd = SaveData.loadData();
        SendThisToServer(pd.Class, pd.SaveAbilites);
        Debug.Log("Running");
    }

     
         public override void OnStartClient()
       {
          // base.OnStartClient();
           PlayerData pd = SaveData.loadData();
           SendThisToServer(pd.Class, pd.SaveAbilites);

           Debug.Log("Running");
           //data?.Invoke(pd.Class, pd.SaveAbilites);
       }

    

       public override void OnStartClient()
       {
           base.OnStartClient();
           PlayerData pd = SaveData.loadData();
           SendThisToServer(pd.Class,pd.SaveAbilites);


           //CmdPlayerData(pd.Class, pd.SaveAbilites);

           Debug.Log("Running");
       }*/
    /*public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        PlayerData pd = SaveData.loadData();
        PlayerData(pd.Class);//, pd.SaveAbilites);

        Debug.Log("Running");
    }

    [Command]
    void CmdPlayerData(int tempInt, string[] tempString)
    {
        Debug.Log("Player Data Sent");
        //data?.Invoke(tempInt, tempString);
        //SendThisToServer();
    }*/


    private void Update()
    {
        
            //PlayerData pd = SaveData.loadData();
            //data?.Invoke(pd.Class, pd.SaveAbilites);
            //SetReady?.Invoke(con);
                
            //GetPlayerData?.Invoke(this, new DataForConnectionArgs { Class = pd.Class}); 
    }

    public void SendThisToServer(int tempInt, string[] tempString)
    {
        Notification lol = new Notification();
        
        lol.An = tempString;
        lol.Class = tempInt;
        
        //NetworkClient.Send<Notification>(lol);
        
        //Debug.Log("Data Has Been Sent " + tempString[0] + " " + tempInt );
    }
}

