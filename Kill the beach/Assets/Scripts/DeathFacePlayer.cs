using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFacePlayer : MonoBehaviour
{
    public SpriteRenderer SpriteRendererr;

    void Start()
    {
        Transform PlayerPos = GameObject.FindWithTag("Player").transform;
        Vector3 direction = PlayerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
        //EnemyRb.rotation = angle;    
        SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,angle);  
         
    }
}
