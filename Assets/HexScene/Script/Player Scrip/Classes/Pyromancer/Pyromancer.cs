using Microsoft.Win32;
using Mono.CecilX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//here we will have function delaget to take in the right Abilites to use.
public class Pyromancer : MonoBehaviour
{
    [TextArea(10, 30)]
    public string discription = "Fire is good and strong yes!";
    public new string name = "Pyromancer";
    public GameObject fireballPrefab;

    // Start is called before the first frame update
    public Fireabilities data;
    public List<Fireabilities> ChosenList; // This will be assigned based on what Abilites are sleelcted
    


    void Start()
    {

        KeyAssinging(ChosenList);
    }

    // Update is called once per frame
    void Update()
    {
       
        keyChecker();
    }

    public void keyChecker()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            //This will run the function in first List of the array
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
    }

    public void KeyAssinging(List<Fireabilities> ChosenList)
    {
        
    }

    public void fireball(GameObject Fireball){

      GameObject fireBall =  Instantiate<GameObject>(Fireball);
      fireBall.AddComponent<FireBall>();
    }

     

}


