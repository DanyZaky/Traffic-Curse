using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void OnClickPause()
    {
        Time.timeScale = 0;
    }

    public void OnClickResume()
    {
        Time.timeScale = 1;
    }   
    
    public void OnClickMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }    
}
