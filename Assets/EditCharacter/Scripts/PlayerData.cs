using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // This saves the PLayer Data allowing us to use this on the server
   
    public string[] SaveAbilites;
    public int Class;
    public PlayerData(AbilitySelector ab)
    {
        Class = ab.characterClass;
        SaveAbilites = ab.SaveAbilites;
    }

    // Start is called before the first frame update

    /*
    public void populateString(List<Abilities> list)
    {
        SaveAbilites = new string[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log("Name: " + list[i].name);
            SaveAbilites[i] = list[i].Name;
        }
    }*/

    
}
