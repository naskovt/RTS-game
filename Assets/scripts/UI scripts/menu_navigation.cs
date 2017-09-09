using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu_navigation : MonoBehaviour {

    private Canvas mainMenu;
    private Canvas helpMenu;


    private void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("main_menu").GetComponent<Canvas>();
        helpMenu = GameObject.FindGameObjectWithTag("help").GetComponent<Canvas>();
        mainMenu.enabled = true;
        helpMenu.enabled = false;
        Time.timeScale = 0;
    }

    void Update () {
        if (Input.GetKeyDown("escape"))
        {
            if (helpMenu.isActiveAndEnabled)
            {
                helpMenu.enabled = false;
                mainMenu.enabled = true;
            }
        }
	}


    public void ExitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }


    public void HelpMenuSwitch()
    {
        helpMenu.enabled = !helpMenu.enabled;
        mainMenu.enabled = !mainMenu.enabled;
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}
