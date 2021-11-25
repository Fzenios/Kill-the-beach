using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScr : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject SettingsMenu, StartMenu, Instructions, Credits1, Credits2;
    public static bool PauseMenuEnabled = false;
    public static bool AllGamePauseEnabled = false;
    private void Update() {
        
    
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!PauseMenu.activeSelf && !SettingsMenu.activeSelf && !StartMenu.activeSelf && !Instructions.activeSelf && !Credits1.activeSelf && !Credits2.activeSelf)
            {
                PauseMenu.SetActive(true);
                PauseMenuEnabled = true;
                AllGamePauseEnabled = true;
                Time.timeScale = 0;
            }
            else if(!Instructions.activeSelf)
            {
                PauseMenu.SetActive(false);
                SettingsMenu.SetActive(false); 
                PauseMenuEnabled = false;
                if (!UpgradeSystemScr.UpgradeMenuEnabled)
                    {
                        Time.timeScale = 1;
                        AllGamePauseEnabled = false;
                    }
            }
        }  
    }

    public void ResumeButton()
    {
        //GetPauseScr();
        PauseMenu.SetActive(false); 
        SettingsMenu.SetActive(false); 
        PauseMenuEnabled = false;
        if (!UpgradeSystemScr.UpgradeMenuEnabled)
            {
                Time.timeScale = 1;
                AllGamePauseEnabled = false;
            }      
    }

    public void SettingsMenuButton()
    {
        PauseMenu.SetActive(false); 
        SettingsMenu.SetActive(true); 
    }

    public void ExitButton()
    {

    }

    public void GetPauseScr()
    {
        PauseMenuScr PauseScr = gameObject.GetComponent<PauseMenuScr>();
    }

}
