using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManagerScript : MonoBehaviour
{

    public Slider _musicSlider, _sfxSlider;
    //public GameObject LvSelectUI;j
    //public GameObject OptionUI;

    public GameObject PauseGameUI;
    public static bool GameIsPaused = false;
    //NEW INPUT SYSTEM:
    [SerializeField] private InputAction pauseButton;
    //[SerializeField] private Canvas canvas;
    //private bool paused = false;

    private void OnEnable ()
    {
        pauseButton.Enable();
    }

    private void OnDisable ()
    {
        pauseButton.Disable();
    }



    public void Start(){
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
        //pauseButton.performed += _ => NewPause();

        _musicSlider.value = Audio_Manager.Instance.musicSource.volume;
        _sfxSlider.value = Audio_Manager.Instance.sfxSource.volume;

        MusicVolume();
        SFXVolume();

    }

    // Update is called once per frame
    void Update()
    {

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

    //public void ToggleLvUI()
    //{
    //    if(LvSelectUI.activeInHierarchy == true)
    //    {
    //        LvSelectUI.SetActive(false);
    //    }
    //    else
    //    {
    //        LvSelectUI.SetActive(true);
    //    }
    //}

    //    public void ToggleOptionUI()
    //{
    //    if(OptionUI.activeInHierarchy == true)
    //    {
    //        OptionUI.SetActive(false);
    //    }
    //    else
    //    {
    //        OptionUI.SetActive(true);
    //    }
    //}

    //public void NewPause()
    //{
    //    paused = !paused;
    //    if (paused)
    //    {
    //        PauseGameUI.SetActive(true);
    //        Time.timeScale = 0;
    //        //canvas.enabled = true;  
    //    }
    //    else
    //    {
    //        PauseGameUI.SetActive(false);   
    //        Time.timeScale = 1;
    //        //canvas.enabled = false; 
    //    }
    //}

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
        PlayerPrefs.SetFloat("musicVolume", _musicSlider.value);
    }

    public void SFXVolume()
    {
        Audio_Manager.Instance.SFXVolume(_sfxSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", _sfxSlider.value);
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
