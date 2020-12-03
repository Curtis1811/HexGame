using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditorInternal;
using UnityEngine;
using System.Linq.Expressions;
using System;
using UnityEngine.EventSystems;

public class AbilitySelector : MonoBehaviour
{
    public List<Abilities> AbilityList;
    public List<Abilities> SelectedList;
    public string[] SaveAbilites;
    public int [] Test;

    public int characterClass = 1;
    public List<GameObject> UIElements;


    // public List<Fireabilities> WaterList;
    // public List<Fireabilities> SelectedWaterList;

    //public List<Fireabilities> AirList;
    //public List<Fireabilities> SelectedAirList;
   
    private void Start()
    {
        UIElements.Add(GameObject.Find("PyromancerUI"));
        characterClass = 1;
        LoadResources();
    }

 
    public void LoadResources()
    {
        switch (characterClass)
        {
            case 1:
                
                AbilityList.Clear();
                AbilityList = Resources.LoadAll<Abilities>("Abilities/Pyromancer").ToList<Abilities>();
            break;
            case 2:
                AbilityList = Resources.LoadAll<Abilities>("Abilities/Hydromancer").ToList<Abilities>();
                break;
            case 3:
                //AirList = Resources.LoadAll<Aireabilities>("Abilities/Aeromancer").ToList(); 
                break;
            case 4:
                // To-DO
                break;
            default:
                Debug.LogError("No Resources Found.");
                break;
        }
        //This is essentially the resource folder for Abilities
        //FireList = Resources.LoadAll<Fireabilities>("Abilities/Pyromancer").ToList<Fireabilities>(); // This load the abilities that are in the Resource folder In the ability List
        //WaterList = Resources.LoadAll<Waterabilities>("Abilities/Hydromancer").ToList(); 
        //AirList = Resources.LoadAll<Aireabilities>("Abilities/Aeromancer").ToList(); 
    }

    public void onPointerClick(BaseEventData Data)
    {
        
        if (SelectedList.Find(x => x.name == Data.selectedObject.name) == null)
        {
            SelectedList.Add(AbilityList.Find(x => x.name == Data.selectedObject.name));
            
        }
        else
        {
            SelectedList.Remove(AbilityList.Find(x => x.name == Data.selectedObject.name));
            
        }

        //SelectedList.Add(AbilityList.Find(x => x.name == Data.selectedObject.name));
        Debug.Log(Data.selectedObject.name);
    }
    
    public void CharacterClassCheck(int characterClass)
    {

    }

    //This readys and saves the data
    public void Confirm()
    {
        populateString(SelectedList);
        SaveData.saveData(this);
        Debug.Log(Application.persistentDataPath);
    }

    //This readys the save data and put it in Save
    public void populateString(List<Abilities> list)
    {
        SaveAbilites = new string[list.Count];

        for (int i = 0; i < list.Count; i++)
        {
            SaveAbilites[i] = list[i].Name.ToString();
            Debug.Log("Name: " + SaveAbilites[i]);
        }
    }


}

