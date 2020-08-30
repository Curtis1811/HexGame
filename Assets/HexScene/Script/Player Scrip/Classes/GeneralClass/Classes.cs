using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Classes : MonoBehaviour
{
    public string ClassString;
    //delegate void selectClass();
    //selectClass sc;
    
    //List<class> CharacterClasses;
    // Start is called before the first frame update
    
    void Start()
    {
        Debug.Log("Running ClassPicker");
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("Running ClassPicker");
    }

    public void ClassSelection()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ClassString = "Pyromancer";
        }else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ClassString = "Hydromancer";

        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ClassString = "Areomancer";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {

        }

    }

}

