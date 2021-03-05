using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionMenu : MonoBehaviour
{

    AbilitySelector AbilitySelectorPointer;
    public GameObject PyromancerUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerCharacterClassSelector(GameObject Class)
    {

        switch (Class.name)//.ToString()
        {
            case "Pyromancer":
                AbilitySelectorPointer.characterClass = 1;
                Debug.Log("Pyromancer");
                break;
            case "Hydromancer":
                AbilitySelectorPointer.characterClass = 2;
                Debug.Log("Hydromancer");
                break;
            case "Areomancer":
                AbilitySelectorPointer.characterClass = 3;
                Debug.Log("Areomancer");
                break;
            case "Geomancer":
                AbilitySelectorPointer.characterClass = 4;
                Debug.Log("Geomancer");
                break;
        }

    }
}
