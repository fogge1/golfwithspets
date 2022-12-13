using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausemenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject PauseMenuUI;
    // Update is called once per frame
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
    void resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }
    void pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
}
