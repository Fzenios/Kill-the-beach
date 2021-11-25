using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoMovement : MonoBehaviour
{
    GameObject Player;
    Transform PlayerPos;
    public SpriteRenderer SpriteRendererr;
    public float EnemySpeed;
    public float EnemyAttackSpeed;
    public float EnemySafeDistance;
    public float EnemyUnSafeDistance;
    int ChanceOfAttack;
    bool Attacking = false; 
    public EnemyDamage EnemyDamage;
    void Start()
    {
        Player = GameObject.Find("Player");  
        PlayerPos = Player.transform;
        EnemyDamage EnemyDamage = GameObject.FindObjectOfType<EnemyDamage>();

    }
    void Update()
    {
        if(!Attacking)
        {
            if(Vector2.Distance(transform.position,PlayerPos.position) > EnemySafeDistance )
                transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, Time.deltaTime * EnemySpeed);
            else if (Vector2.Distance(transform.position,PlayerPos.position) < EnemyUnSafeDistance )
                transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, -Time.deltaTime * EnemySpeed);
    
            ChanceOfAttack = Random.Range(0,1000);

            if(ChanceOfAttack == 1 )
               Attacking = true;
        }
        
        if(Attacking)        
            transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, Time.deltaTime * EnemyAttackSpeed);
     

        Vector3 direction = PlayerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
        //EnemyRb.rotation = angle;    
        SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,angle);    
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        

        if(other.tag == "Player")
        {
            Player = GameObject.Find("Player");  
            PlayerScr PlayerScr = Player.GetComponent<PlayerScr>();

            Attacking = false; 
            PlayerScr.PlayerTakeDamage(EnemyDamage.EnemyMeleeDamage);
        }        
    }
}
