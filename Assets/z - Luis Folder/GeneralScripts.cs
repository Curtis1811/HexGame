using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralScripts : MonoBehaviour
{

    [SerializeField] private Texture2D[] cursorTextureArray;
    [SerializeField] private int frameCount;
    [SerializeField] private float frameRate;

    private int currentFrame;
    private float frameTimer;   
    
    private void Start()
    {
       Cursor.SetCursor(cursorTextureArray[0], new Vector2(0, 0), CursorMode.Auto);
    }

    public static bool IsGamePaused = false;
    public GameObject pauseMenuUi;

    private void FixedUpdate()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0f)
        {
            frameTimer += frameRate;
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(cursorTextureArray[currentFrame], new Vector2(0, 0), CursorMode.Auto);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
