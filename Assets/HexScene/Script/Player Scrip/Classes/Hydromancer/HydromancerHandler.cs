using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HydromancerHandler : MonoBehaviour
{
    
    public string[] abilityData = new string[4];
    public HydromancerHandler(string[] AbilityData)
    {
        this.abilityData = AbilityData;
    }

    public List<WaterAbilities> WaterList;
    public List<GameObject> HydromancerGameObjectPrefabs;

    [SerializeField] public List<WaterAbilities> WaterChosenList = new List<WaterAbilities>();

    public void LoadResources()
    {
        //This is essentially the resource folder for Abilities
        WaterList = Resources.LoadAll<WaterAbilities>("Abilities/Hydromancer").ToList(); // This load the abilities that are in the Resource folder In the ability List.
        HydromancerGameObjectPrefabs = Resources.LoadAll<GameObject>("ClassPrefabs/Hydromancer").ToList(); // This is load all the GameObjects That will be spawned on the server
        Debug.Log("Loaded");
        //WaterList = Resources.LoadAll<Waterabilities>("Abilities/Hydromancer").ToList(); 
        //AirList = Resources.LoadAll<Aireabilities>("Abilities/Aeromancer").ToList(); 
    }
    private void Awake()
    {
        int index=0;
        AssignGameObjects(HydromancerGameObjectPrefabs);
        LoadResources();
        foreach ( WaterAbilities water in WaterList)
        {
            WaterList[index].SpellId = index;
            index++;
        }
    }

    private void Start()
    {
        //loadDataFromServer(abilityData);
        //if (!this.gameObject.GetComponent<Pyromancer>()) { 
        //    this.gameObject.AddComponent<Pyromancer>();
        //}
    }

    public void loadDataFromServer(string[] AbilityData)
    {
        //This is a test.
        addAbilities(AbilityData);
    }

    public void addAbilities(string[] data)
    {
        List<WaterAbilities> testList = new List<WaterAbilities>();
        Debug.Log("Adding Water Abilities");

        for (int i = 0; i < data.Length; i++)
        {
            Debug.Log(data[i]);
            testList.Add(WaterList.Find(x => x.Name == data[i]));
            Debug.Log(testList[i]);
        }
        WaterChosenList = testList;
    }
    //This will be changed to reflect what the player whants to change the keys to. For now this is fine.

    public void AssignGameObjects(List<GameObject> HydromancerGameObjectPrefabs)
    {

    }

    public void getAbilitylist()
    {
        
    }

}
