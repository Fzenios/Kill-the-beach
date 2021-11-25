using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRun : MonoBehaviour
{
    public float Speed;
    bool CanRunBool;
    int RandomY;
    public float Timer;
    public SpriteRenderer SpriteRendererr;
    Transform PlayerPos;
    void Start()
    {
        CanRunBool = false;
        Invoke("CanRun",Timer);  
        RandomY = Random.Range(1,3);  

        PlayerPos = GameObject.FindWithTag("Player").transform;
        Vector3 direction = PlayerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
        //EnemyRb.rotation = angle;    
        SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,angle);  
    }

    void Update()
    {

    }

    void FixedUpdate() 
    {
                if(CanRunBool)
        {
            if(RandomY == 1)        
            {
                transform.position += new Vector3(Speed + Time.deltaTime,0,0);
                SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,-90);  
                //transform.rotation = new Quaternion(90,0,0,0);
            }
            else if (RandomY == 2) 
            {
                transform.position -= new Vector3(Speed + Time.deltaTime,0,0);
               SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,90); 
            }
        }
    }
    void CanRun()
    {
        CanRunBool = true;
    }
}
