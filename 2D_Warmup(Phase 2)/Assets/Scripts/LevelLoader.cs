using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    //loading screen
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;



    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex)); //Asynchronously(khong dong bo) 
        //the normal Loadscene method basically pauses the screen to spend all it resources to load the next scene
        //LoadSceneAsync keep the current scene and all the behavior in it running, while it loading our new scene into memory. We can get information about this progress operation to show on the screen.
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";//* 100f to turn it into precentage
            //Debug.Log(progress);
            yield return null;
        }
    }

}
