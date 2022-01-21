using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GeomancerHandler : ClassHandler
{
    string pathOfAbilities, pathOfGameObjects;
    public List<GameObject> GeomancerGameObjectPrefab; // this is a list of gameObject Prefabs used to spawn game objects(Abilites)
    [SerializeField] public List<Abilities> EarthChosenList = new List<Abilities>();

    private void Start() {
        //abilityData[0] = "Rocksmash";
    }
    private void Awake()
    {
        addAbilities(abilityData);

        ClassData = this.GetComponent<Geomancer>();
        LoadResources("Abilities/Geomancer","ClassPrefabs/Geomancer");
        int index=0;
        //This asignes a spell ID to each of the spells
        foreach (EarthAbilities earth in ChosenListOfAbilities)
        {
            ChosenListOfAbilities[index].SpellId = index;
            index++;
        }
        
        ClassData.GetComponent<Geomancer>().CastPoint = this.transform.Find("CastPoint").gameObject;
    }
    
}
/*
}

   

  public void LoadResources()
    {
        EarthList = Resources.LoadAll<Abilities>("Abilities/Geomancer").ToList();
        GeomancerGameObjectPrefab = Resources.LoadAll<GameObject>("ClassPrefabs/Geomancer").ToList();
        Debug.Log("Loaded");
    }

    public void addAbilities(string[] data){
        List<Abilities> addAbilityList = new List<Abilities>();
        Debug.Log("Geomancer");
        for(int i = 0; i < data.Length; i++){
            addAbilityList.Add(EarthList.Find(x => x.Name == data[i]));
        }
        EarthChosenList = addAbilityList;
        

    }
    
    public GeomancerHandler(string[] AbilityData)
    {
        this.abilityData = AbilityData;
    }
    
    // Start is called before the first frame update

    // Update is called once per frame
    //This is the test to populate the geomancer script with the correct abilities.
    void SelectedAbilityTest(string nameOfAbility){
        
    }
}
*/