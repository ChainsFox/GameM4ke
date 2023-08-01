using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject LVcompleteUI;
    public StopWatch stopWatch;
    public GameManagerScript gm;
    
    

    public bool levelCompleted = false;

    public void Awake ()
    {
        levelCompleted = false;
        gm.GetComponent<GameManagerScript>().enabled = true;
    }
    void Start()
    {

    }

    public void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted) //if the player cross trigger and if the level is not complete then(check if it false)...
        {
            //gm.enabled = true;
            gm.GetComponent<GameManagerScript>().enabled = false;
            levelCompleted = true;
            LVcompleteUI.SetActive(true); 
            Time.timeScale = 0f;
            stopWatch.timerActive = false;
            //GameManagerScript.GameIsPaused = true;
            Audio_Manager.Instance.PlaySFX("FlagSFX");
            //Invoke("CompleteLevel", 2f); //so that you wait for a bit then it wil run the function name.
        }
        else
        {
            gm.GetComponent<GameManagerScript>().enabled = true;
            levelCompleted = false;
        }

    }


    //private void CompleteLevel()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}


}
