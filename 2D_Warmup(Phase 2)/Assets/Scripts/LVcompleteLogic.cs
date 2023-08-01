using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LVcompleteLogic : MonoBehaviour
{
    public GameObject LVcompleteUI;
    //public static bool LViscompleted = false;
    //!!!bug where you pause the game it will let the player moves

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void nextLv()
    {
        Time.timeScale = 1f;
        LVcompleteUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void lvSelect()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void restartLV()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
