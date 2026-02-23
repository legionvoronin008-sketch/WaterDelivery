using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload_Level : MonoBehaviour
{
     void Update()
    {
        
    }
    public void Loadlevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    public void Relodlevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void TogglePause()
    {
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
        }
        else if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }
}
