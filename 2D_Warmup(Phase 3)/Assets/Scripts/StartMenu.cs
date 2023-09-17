using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject OptionUI;
    public GameObject LvSelectUI;
    public void Start()
    {
        GameManagerScript.GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void StartGame ()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Level 1");

    }

    public void Quit ()
    {
        Application.Quit();
        Debug.Log("you quit from the main menu");


    }

    public void OptionsLoad ()
    {
        SceneManager.LoadScene("Options Screen");
    }

    //NEW SOLUTIONS, SETACTIVE UI:
    public void ToggleLvUI ()
    {
        if (LvSelectUI.activeInHierarchy == true)
        {
            LvSelectUI.SetActive(false);
        }
        else
        {
            LvSelectUI.SetActive(true);
        }
    }

    public void ToggleOptionUI ()
    {
        if (OptionUI.activeInHierarchy == true)
        {
            OptionUI.SetActive(false);
        }
        else
        {
            OptionUI.SetActive(true);
        }
    }


}
