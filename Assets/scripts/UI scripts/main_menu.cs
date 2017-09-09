using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour {

    public Scene gameplayScene;

    private bool isInMenu = true;


    private Canvas mainMenu;
    private Canvas settingsMenu;

    private void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("main_menu").GetComponent<Canvas>();
        settingsMenu = GameObject.FindGameObjectWithTag("settings_menu").GetComponent<Canvas>();
        mainMenu.enabled = true;
        settingsMenu.enabled = false;
    }

    void Update()
    {

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



    public void MainMenuSwitch()
    {
        settingsMenu.enabled = !settingsMenu.enabled;
        mainMenu.enabled = !mainMenu.enabled;

    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

}
