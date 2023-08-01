using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    public Slider _musicSlider, _sfxSlider;
    public GameObject LvSelectUI;
    public GameObject OptionUI;

    public GameObject PauseGameUI;
    public static bool GameIsPaused = false;


    void Start()
    {
        //Cursor.visible = false; //hide the cursor
        //Cursor.lockState = CursorLockMode.Locked; //the cursor is locked in the center of the screen

        //if (!PlayerPrefs.HasKey("musicVolume") && !PlayerPrefs.HasKey("sfxVolume")) //if playerprefs dont have key music volume, then set it to the default = 1 and load.
        //{
        //    PlayerPrefs.SetFloat("musicVolume", 1);
        //    PlayerPrefs.SetFloat("sfxVolume", 1);
        //    Load();
        //}
        //else if (!PlayerPrefs.HasKey("musicVolume"))
        //{
        //    PlayerPrefs.SetFloat("musicVolume", 1);
        //    Load();
        //}
        //else if (!PlayerPrefs.HasKey("sfxVolume"))
        //{
        //    PlayerPrefs.SetFloat("sfxVolume", 1);
        //    Load();
        //}
        //else //if it do have a playerprefs save, load the stuff.
        //{
        //    Load();
        //}

    }

    // Update is called once per frame
    void Update()
    {
        //if (gameOverUI.activeInHierarchy) //if game overui is active at anypoint then use cursor
        //{
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //}
        //else
        //{
        //    Cursor.visible = false; //if we are playing and still alive, locked the cursor  
        //    Cursor.lockState = CursorLockMode.Locked;
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }


        }
    }

    public void ToggleLvUI()
    {
        if(LvSelectUI.activeInHierarchy == true)
        {
            LvSelectUI.SetActive(false);
        }
        else
        {
            LvSelectUI.SetActive(true);
        }
    }

        public void ToggleOptionUI()
    {
        if(OptionUI.activeInHierarchy == true)
        {
            OptionUI.SetActive(false);
        }
        else
        {
            OptionUI.SetActive(true);
        }
    }

    public void pauseMenu()
    {
        PauseGameUI.SetActive(true); //you disable it in the editor(remember that), !!! need fix
    }

    public void Resume()
    {
        PauseGameUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseGameUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void mainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start Screen");
    }

    public void lvSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level Select");
    }

    //public void OptionsShow()
    //{
    //    if(OptionsUI != null)
    //    {
    //        bool isActive = OptionsUI.activeSelf;

    //        OptionsUI.SetActive(!isActive);
    //    }
    //}

    public void quit()
    {
        Application.Quit();
        Debug.Log("you quit the game");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options Screen");
    }

    //volume pause setting:
    public void ToggleMusic()
    {
        Audio_Manager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        Audio_Manager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        Audio_Manager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        Audio_Manager.Instance.SFXVolume(_sfxSlider.value);
    }

    //player prefs:


    //public void Load ()
    //{
    //    _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    //    _sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");


    //}

    //public void SaveMusicVolume ()
    //{
    //    PlayerPrefs.SetFloat("musicVolume", _musicSlider.value);

    //}

    //public void SaveSfxVolume ()
    //{
    //    PlayerPrefs.SetFloat("sfxVolume", _sfxSlider.value);

    //}




}
