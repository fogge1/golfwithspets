using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menucontroller : MonoBehaviour
{
    public static bool paused = false;
    public GameObject PauseMenuUI;
    public int mainmenu;
    public int mapselect;
    public int singleplayer = 3;
    public int mapbuilder;


    public void quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    public void playmapselect()
    {
        SceneManager.LoadScene(mapselect);
        Debug.Log("loading scene mapselect");
    }
    public void playmenu()
    {
        SceneManager.LoadScene(mainmenu);
        Debug.Log("loading scene singleplayer");
    }

    public void playsingleplayer()
    {
        SceneManager.LoadScene(singleplayer);
        Debug.Log("loading scene singleplayer");
    }

    public void playmapbuilder()
    {
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
                pause();
            }
        }
    }
}