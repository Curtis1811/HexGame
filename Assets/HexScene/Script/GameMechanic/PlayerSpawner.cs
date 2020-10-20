using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerSpawner : NetworkBehaviour {

    int temp;
    string tempp;

    private void Start()
    {
        EventScript evnt = new EventScript();
       
    }

    private void Update()
    {

    }

    public  void dd(int PlayerStuff){
        temp = PlayerStuff;
    }
}
