using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Here will be where all the functions for the Pyromancer are created.
public class PyromancerHandler : MonoBehaviour
{
    public string[] abilityData = new string[4];
    public int test;
    public PyromancerHandler(string[] AbilityData)
    {
        this.abilityData = AbilityData;
    }

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
        //loadData();
    }

    public void loadDataFromServer(string[]AbilityData)
    {
        //This is a test.
        addAbilities(AbilityData);
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
