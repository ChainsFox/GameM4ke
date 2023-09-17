using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider _musicSlider, _sfxSlider;

    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    //[SerializeField] Slider volumeSlider;

    //Toggle fullscreen variable(saveable):
    private bool isFullScreen = false;
    private int screenInt;
    public Toggle fullScreenToggle;
    //resolution/quality save:
    const string resName = "resolutionoption";
    const string qualityName = "qualityName";

    Resolution[] resolutions;

    private void Start ()
    {
        screenInt = PlayerPrefs.GetInt("toggleState");
        
        if(screenInt == 1)
        {
            isFullScreen = true;
            fullScreenToggle.isOn = true;

        }
        else
        {
            fullScreenToggle.isOn = false;
        }

        resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(resName, resolutionDropdown.value);
            PlayerPrefs.Save();
        }));

        qualityDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(qualityName, qualityDropdown.value);
            PlayerPrefs.Save();
        }));


        if (!PlayerPrefs.HasKey("musicVolume") && !PlayerPrefs.HasKey("sfxVolume") ) //if playerprefs dont have key music volume, then set it to the default = 1 and load.
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            PlayerPrefs.SetFloat("sfxVolume", 1);
            Load();
        }
        else //if it do have a playerprefs save, load the stuff.
        {
            Load();
        }

        qualityDropdown.value = PlayerPrefs.GetInt(qualityName, 3);

        _musicSlider.value = Audio_Manager.Instance.musicSource.volume;
        _sfxSlider.value = Audio_Manager.Instance.sfxSource.volume;
        MusicVolume();
        SFXVolume();

            
        resolutions = Screen.resolutions; //to find the computer res and store it in a array

        resolutionDropdown.ClearOptions(); //clear all the option in the editor dropdown

        List<string> options = new List<string>(); //create a list of string that are our options

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) //loop through each element in are option array
        { 
            string option = resolutions[i].width + " x " + resolutions[i].height; //and for each of them we create a nice formating string that display our resolutions
            options.Add(option); //and we add it to our options list

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)//compare width and height to see if we are looking at the right resolutions or not
            {
                currentResolutionIndex = i; //then we store the index of that
            }

        }

        resolutionDropdown.AddOptions(options); //=>add options list to our resolutionDropdown
        //resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex); //to save the resolutions
        resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution (int resolutionIndex) //to update the resolutions
    {
        Resolution resolution = resolutions[resolutionIndex]; //kieu du lieu Resolution co ten resolution = array resolutions tai vi tri index
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //public void setVolume (float volume) //OLD
    //{
    //    //debug.log(volume);
    //    audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20); //name of parameter, and float volume. this has been change to min value 0.0001, and max value 1 and change the code so that it works better.
    //    Save(); // to save the player volume setting.
    //    // !!! setvolume is still in progress, also saving player settings is a whole other problem :)
    //}

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (isFullScreen == false)
        { 
            PlayerPrefs.SetInt("toggleState",0);    
        }
        else
        {
            isFullScreen = true;
            PlayerPrefs.SetInt("toggleState",1);
        }

    }


    public void Load () //OLD->update and reused.
    {
        //volumeSlider.value = PlayerPrefs.GetFloat("musicVolume"); //get the float that you save
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

    }

    //public void Save () //OLD->update and reused.
    //{
    //    //PlayerPrefs.SetFloat("musicVolume", volumeSlider.value); //musicVolume is the key name you set, and you get the volume slider value.
    //    PlayerPrefs.SetFloat("musicVolume", _musicSlider.value);
    //    PlayerPrefs.SetFloat("sfxVolume", _sfxSlider.value);

    //}


    //volume pause setting:

    public void MusicVolume()
    {
        Audio_Manager.Instance.MusicVolume(_musicSlider.value);
        PlayerPrefs.SetFloat("musicVolume", _musicSlider.value);
        //Save();
    }

    public void SFXVolume()
    {
        Audio_Manager.Instance.SFXVolume(_sfxSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", _sfxSlider.value);
        //Save();
    }

    public void ToggleMusic()
    {
        Audio_Manager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        Audio_Manager.Instance.ToggleSFX();
    }


 









}
