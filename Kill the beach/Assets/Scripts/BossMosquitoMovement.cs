using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMosquitoMovement : MonoBehaviour
{
    Transform PlayerPos;
    public Transform AttackSide;
    EnemyDamage EnemyDamage;
    public EnemyHealthScr EnemyHealthScr;
    GameObject Player;
    PlayerScr PlayerScr;
    public float RotationSpeed, RotationRadius;
    public float AttackSpeed;
    float PosX, PosY, angle = 0;
    public SpriteRenderer SpriteRenderer;
    bool Roam = true, Attacking = false, Attack = false, Reset = false; 
    int ChanceOfAttack;
    Rigidbody2D Rb;
    bool BossFight;

    void Start()
    {
        Player = GameObject.Find("Player");  
        PlayerScr = Player.GetComponent<PlayerScr>();
        PlayerPos = Player.GetComponent<Transform>();
        EnemyHealthScr.Immune = true;
        EnemyDamage EnemyDamage = GameObject.FindObjectOfType<EnemyDamage>();

        Rb = GetComponent<Rigidbody2D>();
        
        StartCoroutine(StartAfterAnimate());

    }

    void Update()
    {
        if(BossFight)
        {
            if(Roam)
            {
                Movement();
                if(!Attacking && Roam && !Reset)
                {
                    ChanceOfAttack = Random.Range(0,300);

                    if(ChanceOfAttack == 1 )
                    {
                        Attacking = true;
                        StartCoroutine(WaitSeconds());  
                    }
                }
            }
        }
        
        if(Attacking)
        {
            if(RotationRadius < 8)
                RotationRadius += Time.deltaTime + 0.02f;
        }
        
        if(Reset)
        {
            if(RotationRadius >= 4)
                RotationRadius -= Time.deltaTime + 0.03f;
            else  
                Reset = false;  
        }
    }
    
    void FixedUpdate() 
    {
        if(Attack)
        {
            Attack = false;
            Rb.AddForce(AttackSide.up * Time.deltaTime * AttackSpeed, ForceMode2D.Impulse);
        }        
    }

    void Movement()
    {
        PosX = PlayerPos.position.x + Mathf.Cos(angle) * RotationRadius;
        PosY = PlayerPos.position.y + Mathf.Sin(angle) * RotationRadius;
        transform.position = new Vector2(PosX,PosY);
        angle = angle + Time.deltaTime * RotationSpeed;

        if(angle >= 360)
            angle = 0;

        Vector3 direction = PlayerPos.position - transform.position;
        float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;  
        SpriteRenderer.transform.eulerAngles = new Vector3 (0,0,Angle); 
    }
    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(3);
        Roam = false;
        Attack = true;
        Attacking = false;
        yield return new WaitForSeconds(4);
        RotationRadius = 21;
        Reset = true; 
        Roam = true;
    }

    IEnumerator StartAfterAnimate()
    {
        yield return new WaitForSeconds(5);
        BossFight = true;
        EnemyHealthScr.Immune = false;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            PlayerScr.PlayerTakeDamage(EnemyDamage.EnemyBossDamage);
        }        
    }
}
