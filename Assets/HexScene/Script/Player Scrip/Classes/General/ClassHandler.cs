using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ClassHandler : MonoBehaviour
{
    public Classes ClassData;
    public string[] abilityData = new string[4];
    List<Abilities> ListOfAllAbilities; //This is a list of all earth Skills.
    List<GameObject> GameObjectPrefab; //This is a list of gameObject Prefabs used to spawn game objects(Abilites)
    [SerializeField] public List<Abilities> ChosenListOfAbilities = new List<Abilities>();
    
    public void LoadResources(string pathOfAbilities, string pathOfGameObjects){
        ListOfAllAbilities = Resources.LoadAll<Abilities>(pathOfAbilities).ToList();
        GameObjectPrefab = Resources.LoadAll<GameObject>(pathOfGameObjects).ToList();
        ChosenListOfAbilities = Resources.LoadAll<Abilities>(pathOfAbilities).ToList();
    }

    //THis adds the abilites to the chosen List
    public void addAbilities(string[] data){
        List<Abilities> addAbilityList = new List<Abilities>();

        Debug.Log("HandlderWorking");
        
        for(int i = 0; i < data.Length; i++){
            addAbilityList.Add(ListOfAllAbilities.Find(x => x.Name == data[i]));
        }
        ChosenListOfAbilities = addAbilityList;
    }

    public void assignGameObjects(){
        ClassData.AbilityOnePrefab = ChosenListOfAbilities[0].GameObjectPrefab;
        ClassData.AbilityTwoPrefab = ChosenListOfAbilities[1].GameObjectPrefab;
        ClassData.AbilityThreePrefab = ChosenListOfAbilities[2].GameObjectPrefab;
        ClassData.AbilityFourPrefab = ChosenListOfAbilities[3].GameObjectPrefab;
    }

   

}
