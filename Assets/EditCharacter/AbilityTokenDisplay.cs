using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityTokenDisplay : AbilitySelector
{
    //public Abilities Ability;
    // Start is called before the first frame update
   
    //This loop though the FireAbilities List and displayes them to the player

    
    public string FindObject(List<Fireabilities> FireList, string name)
    {
        for (int i =0; i < FireList.Count; i++)
        {
            if(FireList[i].name == name)
            {
                return FireList[i].Discription;
            }
        }

        return null;
    }

    //This is simply for displaying text.
    public void Onhover(GameObject button)
    {
        Debug.Log(button.name);
        //Debug.Log(FindObject(AbilityList, button.name));

    }

    
}
