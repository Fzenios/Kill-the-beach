using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyScr : MonoBehaviour
{
    GameObject Player;
    Transform PlayerPos;
    Transform EnemyToBeChased;
    public SpriteRenderer SpriteRendererr;
    public float AllySpeed;
    public float AllyToPlayerSafe, AllyToPlayerUnsafe;
    public float AllyToEnemySafe, AllyToEnemyUnsafe;
    float DistancePlayer, DistanceEnemy;
    bool ChasingEnemy; 
    public Animator animator;

    void Start()
    {
        Player = GameObject.Find("Back");  
        PlayerPos = Player.transform;
    }
    void Update()
    {
        BlockMovement();
        if(EnemyToBeChased != null)
            ChasingEnemy = true;
        else 
            ChasingEnemy = false;
        
        if(!ChasingEnemy)
        {
            Stay();
        }
        if(ChasingEnemy)
        {
            Chase();
        }   
        if(DistancePlayer <= AllyToPlayerSafe && !ChasingEnemy)
        {
            animator.SetBool("Walk",false);
            animator.SetBool("Idle",true);
        }
        else if(DistancePlayer > AllyToPlayerSafe && !ChasingEnemy)
        {
            animator.SetBool("Walk",true);
            animator.SetBool("Idle",false);
        }
        else if(ChasingEnemy && DistanceEnemy > AllyToEnemySafe)
        {
            animator.SetBool("Idle",false);
            animator.SetBool("Walk",true);
        }
        else if(ChasingEnemy && DistanceEnemy <= AllyToEnemySafe )
        {
            animator.SetBool("Idle",false);
            animator.SetBool("Walk",false);
        }
    } 

    void Stay()
    {
        if(Vector2.Distance(transform.position,PlayerPos.position) > AllyToPlayerSafe )
            transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, Time.deltaTime * AllySpeed);
        else if (Vector2.Distance(transform.position,PlayerPos.position) < AllyToPlayerUnsafe )
            transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, -Time.deltaTime * AllySpeed);

        Vector3 direction = PlayerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
        //EnemyRb.rotation = angle;    
        SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,angle);    

        DistancePlayer = Vector2.Distance(transform.position, PlayerPos.position);  
    }

    void Chase()
    {
        if(Vector2.Distance(transform.position,EnemyToBeChased.position) > AllyToEnemySafe )
            transform.position = Vector2.MoveTowards(transform.position, EnemyToBeChased.position, Time.deltaTime * AllySpeed);
        else if (Vector2.Distance(transform.position,EnemyToBeChased.position) < AllyToEnemyUnsafe )
            transform.position = Vector2.MoveTowards(transform.position, EnemyToBeChased.position, -Time.deltaTime * AllySpeed);

        Vector3 direction = EnemyToBeChased.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
        //EnemyRb.rotation = angle;    
        SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,angle);    

        DistanceEnemy = Vector2.Distance(transform.position, EnemyToBeChased.position); 
    } 

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            if(EnemyToBeChased == null)
                EnemyToBeChased = other.gameObject.transform;
        }        
    }
    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {  
            if(EnemyToBeChased == null)
                EnemyToBeChased = other.gameObject.transform;
        }         
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            EnemyToBeChased = null;
            ChasingEnemy = false;
        }        
    }

    void BlockMovement()
    {
        if(transform.position.x <= -9 )
        transform.position = new Vector3 (-9f,transform.position.y,0);
        if(transform.position.x >= 9 )
        transform.position = new Vector3 (9f,transform.position.y,0);
        if(transform.position.y <= -5 )
        transform.position = new Vector3 (transform.position.x,-5,0);
    }
}
