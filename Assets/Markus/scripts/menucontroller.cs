using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menucontroller : MonoBehaviour
{
    public static bool paused = false;
    public GameObject PauseMenuUI;
    private int mainmenu = 0;
    private int mapselect = 2;
    private int singleplayer = 3;
    private int mapbuilder = 1;


    public void quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    public void playmapselect()
    {
        playsingleplayer();
        //SceneManager.LoadScene(mapselect);
        Debug.Log("loading scene mapselect");
    }
    public void playmenu()
    {   
        paused = false;
        SceneManager.LoadScene(mainmenu);
        Debug.Log("loading scene singleplayer");
        
    }

    public void playsingleplayer()
    {
        paused = false;
        SceneManager.LoadScene(singleplayer);
        Debug.Log("loading scene singleplayer");
    }

    public void playmapbuilder()
    {
        paused = false;
        SceneManager.LoadScene(mapbuilder);
        Debug.Log("loading scene mapbuilder");
    }

    public void resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                resume();

            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                pause();
            }
        }
    }
}