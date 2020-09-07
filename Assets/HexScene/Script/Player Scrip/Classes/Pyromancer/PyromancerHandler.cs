 using Mirror.Examples.Basic;
using Mono.CecilX.Cil;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

//Here will be where all the functions for the Pyromancer are created.
public class PyromancerHandler : MonoBehaviour
{
    
    //public GameObject FireBallPrefab = GameObject.FindGameObjectWithTag("FireBall");
    public List<Fireabilities> FireList;
    //public List<Waterabilities> WaterList; 
    //public List<Aireabilities> AirList;
    public List<Fireabilities> PyromancerChosenList;

  
    public void LoadResources()
    {
        //This is essentially the resource folder for Abilities
        FireList = Resources.LoadAll<Fireabilities>("Abilities/Pyromancer").ToList(); // This load the abilities that are in the Resource folder In the ability List.

        //WaterList = Resources.LoadAll<Waterabilities>("Abilities/Hydromancer").ToList(); 
        //AirList = Resources.LoadAll<Aireabilities>("Abilities/Aeromancer").ToList(); 
    }

   
    private void Start()
    {
        
        LoadResources();
        loadData();
    }

    public void loadData()
    {
        //This is a test.
        PlayerData data = SaveData.loadData();
        Debug.Log(data.Class);
        addAbilities(data.SaveAbilites);
        
    }
    public void addAbilities(string[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            PyromancerChosenList.Add(FireList.Find(x => x.name == data[i]));
            Debug.Log(data[i]);
        }
    }


    //This will be changed to reflect what the player whants to change the keys to. For now this is fine.


}
