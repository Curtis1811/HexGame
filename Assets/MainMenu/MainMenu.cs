using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;

public class MainMenu : MonoBehaviour
{
    AbilitySelector AbilitySelectorPointer;
    public GameObject PyromancerUI;
    
    // Start is called before the first frame update
    private void Start()
    {
        AbilitySelectorPointer = this.gameObject.GetComponent<AbilitySelector>();
    }

    public void play()
    {
        SceneManager.LoadScene("Curtis_Scene");
        //SceneManager.LoadScene("Game");
        //Create this into a lobby Manager
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void CharacterManager()
    {
        SceneManager.LoadScene("CharacterEdit");

    }

    public void BackToMain()
    {
        
        SceneManager.LoadScene("MainMenu");
    }

 
    //This the menu to send the correct class to AbilitySelector
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
