using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject DialogueManager, DialogueScene;
    public GameObject CreditsObj, StartMenuObj;

    void Awake() 
    {
        FindObjectOfType<MusicSystem>().Play("IntroMusic");    
    }
    public void StartBtn()
    {
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        //FindObjectOfType<MusicSystem>().Play("LevelMusic");
        yield return new WaitForSeconds(4);
        DialogueManager.SetActive(true);
        DialogueScene.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Credits()
    {
        FindObjectOfType<MusicSystem>().Stop("IntroMusic");
        FindObjectOfType<MusicSystem>().SoundEffects("CreditsMusic");
        CreditsObj.SetActive(true); 
        StartMenuObj.SetActive(false);
        StartCoroutine(CreditsReset());
    }
    IEnumerator CreditsReset()
    {
        yield return new WaitForSeconds(122);
        CreditsObj.SetActive(false); 
        StartMenuObj.SetActive(true);
        FindObjectOfType<MusicSystem>().Stop("CreditsMusic");
        FindObjectOfType<MusicSystem>().Play("IntroMusic");    
    }
    public void StartMusic()
    {
        FindObjectOfType<MusicSystem>().Stop("CreditsMusic");
        FindObjectOfType<MusicSystem>().Play("IntroMusic");  
        StopAllCoroutines();  
    }
}
