using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider _musicSlider, _sfxSlider;

    public Dropdown resolutionDropdown;
    //[SerializeField] Slider volumeSlider;



    Resolution[] resolutions;
    void Start ()
    {
        //if (!PlayerPrefs.HasKey("musicVolume")) //if playerprefs dont have key music volume, then set it to the default = 1 and load.
        //{
        //    PlayerPrefs.SetFloat("musicVolume", 1);
        //    Load();
        //}
        //else //if it do have a playerprefs save, load the stuff.
        //{
        //    Load();
        //}   


            
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
        resolutionDropdown.value = currentResolutionIndex;
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
    }


    //public void Load() //OLD
    //{
    //    volumeSlider.value = PlayerPrefs.GetFloat("musicVolume"); //get the float that you save


    //}

    //public void Save () //OLD
    //{
    //    PlayerPrefs.SetFloat("musicVolume", volumeSlider.value); //musicVolume is the key name you set, and you get the volume slider value.

    //}


    //volume pause setting:

    public void MusicVolume()
    {
        Audio_Manager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        Audio_Manager.Instance.SFXVolume(_sfxSlider.value);
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
