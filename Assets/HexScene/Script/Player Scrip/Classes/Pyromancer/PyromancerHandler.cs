using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Here will be where all the functions for the Pyromancer are created.
public class PyromancerHandler : MonoBehaviour
{
    public string[] abilityData = new string[4];
    public PyromancerHandler(string[] AbilityData)
    {
        this.abilityData = AbilityData;
        
    }                                                          
                                                               
    //public GameObject FireBallPrefab = GameObject.FindGameObjectWithTag("FireBall");
    public List<Fireabilities> FireList;
    public List<GameObject> PyromancerGameObjectPrefabs;


    //public List<Waterabilities> WaterList; 
    //public List<Aireabilities> AirList;
    [SerializeField] public List<Fireabilities> PyromancerChosenList = new List<Fireabilities>();

  
    public void LoadResources()
    {
        //This is essentially the resource folder for Abilities
        FireList = Resources.LoadAll<Fireabilities>("Abilities/Pyromancer").ToList(); // This load the abilities that are in the Resource folder In the ability List.
        PyromancerGameObjectPrefabs = Resources.LoadAll<GameObject>("ClassPrefabs/Pyromancer").ToList();
        Debug.Log("Loaded");
        //WaterList = Resources.LoadAll<Waterabilities>("Abilities/Hydromancer").ToList(); 
        //AirList = Resources.LoadAll<Aireabilities>("Abilities/Aeromancer").ToList(); 
    }
    private void Awake()
    {
        AssignGameObjects(PyromancerGameObjectPrefabs);

        LoadResources();
    }

    private void Start()
    {
        //loadDataFromServer(abilityData);
        //if (!this.gameObject.GetComponent<Pyromancer>()) { 
        //    this.gameObject.AddComponent<Pyromancer>();
        //}
    }

    public void loadDataFromServer(string[]AbilityData)
    {
        //This is a test.
        addAbilities(AbilityData);
    }

    public void addAbilities(string[] data)
    {
        List<Fireabilities> testList = new List<Fireabilities>();
        Debug.Log("IN Add ability");
        for (int i = 0; i < data.Length; i++)
        {
            Debug.Log(data[i]);
            testList.Add(FireList.Find(x => x.Name == data[i]));
            Debug.Log(testList[i]);

        }
        PyromancerChosenList = testList;
    }
    //This will be changed to reflect what the player whants to change the keys to. For now this is fine.

    public void AssignGameObjects(List<GameObject> PyromancerGameObjectPrefabs)
    {

    }
    
    public void getAbilitylist()
    {

    }
    //A better way to handle player Classes is to have the handelers grabs whats in the players list. Already on their computers. The server doesnt really need to know.
}
