using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public CamerZoom cameraZoom;
    public static bool IsPaused=false;
    public GameObject healthBar;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
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
        PauseMenuUI.SetActive(false);
        cameraZoom.enabled = true;
        healthBar.SetActive(true);
        Time.timeScale = 1;
        IsPaused = false;

    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        healthBar.SetActive ( false);
        cameraZoom.enabled = false;
        Time.timeScale = 0;
        IsPaused = true;

    }
    public void loadmenu()
    {
        Debug.Log("loading menu");
    }
    public void Options()
    {

    }


}
