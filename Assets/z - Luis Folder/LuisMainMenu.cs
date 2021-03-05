using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LuisMainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Playing");
        SceneManager.LoadScene("LuisMain");
               
    }

    public void OptionsScreen()
    {
        Debug.Log("Options");
        SceneManager.LoadScene("Options");

    }



}
