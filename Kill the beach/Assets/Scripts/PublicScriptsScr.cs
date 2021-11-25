using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicScriptsScr : MonoBehaviour
{
    Vector2 MousePos;
    public GameObject Crosshair;
    public Camera CamStart, CamMain;

    void Start()
    {
        
    }

    void Update()
    {
        if(CamStart.isActiveAndEnabled)
        MousePos = CamStart.ScreenToWorldPoint(Input.mousePosition);
        else
        MousePos = CamMain.ScreenToWorldPoint(Input.mousePosition);

        Crosshair.transform.position = new Vector3(MousePos.x, MousePos.y, 0); 

    }
}
