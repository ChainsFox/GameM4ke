using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int level;
    public Text levelText;
    void Start()
    {
        levelText.text = level.ToString(); //auto chuyen text cua tung o vuong level sang so da dc dat tu truoc(de y script trong editor)
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("Level " + level.ToString()); //Level + so nguyen duoc chuyen hoa thanh string(ten scene)
    }
}
    