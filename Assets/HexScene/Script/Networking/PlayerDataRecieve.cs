using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerDataRecieve : NetworkBehaviour
{
    [SerializeField]
    int Data;

    [SerializeField]
    string[] Ablist = new string[3];


    //maybe here I will need to create a way for the server to get the player information on player connect and then add the components with the new information
   

}
