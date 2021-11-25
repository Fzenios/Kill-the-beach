using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreyScr : MonoBehaviour
{    
    GameObject MosRepPos;

    void Start() 
    {
         MosRepPos = GameObject.Find("GunPointRepelant");    
         //MosRepPos = GameObject.FindGameObjectWithTag("")   ;
    }
    
    void Update()
    {
        transform.position = MosRepPos.transform.position;
        transform.rotation = MosRepPos.transform.rotation;
    }
}
