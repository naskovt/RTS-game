using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingame_menu : MonoBehaviour {

    private bool isInMenu = false;

    private Canvas mainMenu;
    private Canvas settingsMenu;


    private void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("ingame_menu").GetComponent<Canvas>();
        settingsMenu = GameObject.FindGameObjectWithTag("settings_menu").GetComponent<Canvas>();
        mainMenu.enabled = false;
        settingsMenu.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (settingsMenu.isActiveAndEnabled)
            {
                settingsMenu.enabled = false;
                mainMenu.enabled = true;
            }
            else
            {
                MainMenuEnterExit();
            }
        }
    }


    public void MainMenuEnterExit()
    {
        if (isInMenu)
        {
            GameManager.PauseGame();
            mainMenu.enabled = true;
            isInMenu = !isInMenu;
        }
        else
        {
            GameManager.ContinueGame();
            mainMenu.enabled = false;
            isInMenu = !isInMenu;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }



    public void SettingsMenuSwitch()
    {
        settingsMenu.enabled = !settingsMenu.enabled;
        mainMenu.enabled = !mainMenu.enabled;

    }

}
