using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor.EventSystems;// This is to get the data from Select and Deselect
using UnityEngine.EventSystems;
using System.Linq.Expressions;
using System;

public class AbilitySelector : MonoBehaviour
{
    public List<Abilities> AbilityList;
    public List<Abilities> SelectedList;

    public int characterClass;
    public List<GameObject> UIElements;

    public event EventHandler AddingTolist;

    // public List<Fireabilities> WaterList;
    // public List<Fireabilities> SelectedWaterList;

    //public List<Fireabilities> AirList;
    //public List<Fireabilities> SelectedAirList;
    public event EventHandler SendList;


    void AddingToListEvent(object sender, EventArgs e)
    {

    }

    private void Start()
    {
        UIElements.Add(GameObject.Find("PyromancerUI"));
        characterClass = 1;
        LoadResources();
    }


    private void Update()
    {
        
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
                //WaterList = Resources.LoadAll<Waterabilities>("Abilities/Hydromancer").ToList();
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

    public void OnSelect(BaseEventData Data)
    {
        SelectedList.Add(AbilityList.Find(x => x.name == Data.selectedObject.name));
        Debug.Log(Data.selectedObject.name);
    }
   
    public void OnDeselect(BaseEventData Data)
    {
        SelectedList.Remove(AbilityList.Find(x => x.name == Data.selectedObject.name));
        Debug.Log(Data.selectedObject.name);
    }

    public void CharacterClassCheck(int characterClass)
    {

    }

    public void Confirm(int CharacterClass, List<Abilities> SelectedList)
    {
        
    }

    
}

