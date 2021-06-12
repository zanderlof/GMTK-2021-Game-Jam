using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    

    public void Pause()
    {
        Time.timeScale = 0f;
        
        pauseMenu.SetActive(true);
    }
}
