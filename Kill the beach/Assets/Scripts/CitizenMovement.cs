using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenMovement : MonoBehaviour
{
    GameObject Player;
    Transform PlayerPos;
    public SpriteRenderer SpriteRendererr;
    public float EnemySpeed;
    public float EnemySafeDistance;
    public float EnemyUnSafeDistance;
    public float Distance;
    bool BlockMovementBool = false;

    void Start()
    {
        Player = GameObject.Find("Player");  
        PlayerPos = Player.transform;
        StartCoroutine(BlockMovement());
    }
    void Update()
    {
        if(BlockMovementBool)
        {
            if(transform.position.x <= -9 )
            transform.position = new Vector3 (-9f,transform.position.y,0);
            if(transform.position.x >= 9 )
            transform.position = new Vector3 (9f,transform.position.y,0);
            if(transform.position.y <= -5 )
            transform.position = new Vector3 (transform.position.x,-5,0);
            if(transform.position.y >= 2.3 )
            transform.position = new Vector3 (transform.position.x,2.3f,0);
        }
        if(Vector2.Distance(transform.position,PlayerPos.position) > EnemySafeDistance )
            transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, Time.deltaTime * EnemySpeed);
        else if (Vector2.Distance(transform.position,PlayerPos.position) < EnemyUnSafeDistance )
            transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, -Time.deltaTime * EnemySpeed);

        Vector3 direction = PlayerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
        //EnemyRb.rotation = angle;    
        SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,angle);    

        Distance = Vector2.Distance(transform.position, PlayerPos.position); 
    } 

    IEnumerator BlockMovement()
    {
        yield return new WaitForSeconds(4);
        BlockMovementBool = true;
    }
}
