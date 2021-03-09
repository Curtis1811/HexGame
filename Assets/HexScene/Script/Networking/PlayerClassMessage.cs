using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;

public class Notification : MessageBase
{
    // Start is called before the first frame update
    public string[] An;
    public int Class;
    
    /*
    public override void Deserialize(NetworkReader reader)
    {
        base.Deserialize(reader);
        netID = reader.ReadPackedUInt32();
        test = reader.ReadString();
    }

    public override void Serialize(NetworkWriter writer)
    {
        base.Serialize(writer);
        writer.WritePackedUInt32(netID);
        writer.WriteString(test);
    }*/

}

public class PlayerClassMessage : MonoBehaviour
{
    /*
    MyNetworkManager pog;

    

    private void Start()
    {
        pog = FindObjectOfType<MyNetworkManager>();
        if (!NetworkServer.active) {
            return; 
        }
    
        //This is what happens when we recieve a notification
        NetworkServer.RegisterHandler<Notification>(OnNotification);
        
    }

    public void OnNotification(NetworkConnection con, Notification message)
    {
        pog.testmsg = message.test;       
    }*/
}
