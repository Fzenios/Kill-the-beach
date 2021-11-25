using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScr : MonoBehaviour
{
    public int i;
    public GameObject[] Instractions;
    //public GameObject InstractionsObj;
    private void Start() 
    {
        i = 0;        
    }
    void OnEnable() 
    {
        Instractions[i].SetActive(true);    
    }

    public void NextInstr()
    {
        if(i==7 || i==9)
        {
            Instractions[i].SetActive(false);
            gameObject.SetActive(false);
            Time.timeScale = 1;
            return;
        }
        Instractions[i].SetActive(false);
        i++;
        Instractions[i].SetActive(true);
    }
}
