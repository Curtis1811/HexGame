using Microsoft.Win32;
//using Mono.CecilX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//here we will have function delaget to take in the right Abilites to use.
public class Pyromancer : MonoBehaviour//PyromancerHandler
{
    string[] abil = new string[4];
    [TextArea(10, 30)]
    public string discription = "Fire is good and strong yes!";
    public new string name = "Pyromancer";

    public GameObject fireballPrefab;
    public List<string> AbilityNames;
    
    // Start is called before the first frame update
    
     // This will be assigned based on what Abilites are Selected
    
    //Change this to have its own Class that other classes can use as a tempalte.

    //This will need to gain the .txt data on load and recieve the data to use.
   

    void Start()
    {
       //KeyAssinging(PyromancerChosenList);
    }

    // Update is called once per frame
    void Update()
    {  
        //keyChecker();
    }

    /*
    public void keyChecker()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log(PyromancerChosenList[0].name);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(PyromancerChosenList[1].name);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(PyromancerChosenList[2].name);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log(PyromancerChosenList[3].name);
        }
        
    }

    public void KeyAssinging(List<Fireabilities> ChosenList)
    {
        
    }

    public void fireball(GameObject Fireball){

      GameObject fireBall =  Instantiate<GameObject>(Fireball);
      fireBall.AddComponent<FireBall>();
    }
    */
     

}


