using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScr : MonoBehaviour
{    
    GameObject Player;
    Transform PlayerPos;
    PlayerScr PlayerScr;
    public EnemyHealthScr EnemyHealthScr;
    public SpriteRenderer SpriteRendererr;
    public float TankSpeed;
    public int TankColliderDamage;
    bool ResetPos = false;
    
    public GameObject EnemyBullet;
    public Transform GunPoint;
    public float BulletSpeed;    
    float EnemyWeaponFireTimer;
    public float EnemyWeaponFireRate;
    int ShootCounter;
    public int ShootCounterLimit;
    bool Reload;
    public bool CanShoot = true;
    public float ReloadTimer;

    void Start()
    {
        CanShoot = true;
        Player = GameObject.Find("Player");  
        PlayerPos = Player.transform;
        PlayerScr = Player.GetComponent<PlayerScr>();
        transform.position = new Vector3(11,-4,0);
    }

    void Update()
    {
        Vector3 direction = PlayerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;  
        SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,angle);  

        transform.position += new Vector3(-Time.deltaTime * TankSpeed,0,0); 
        
        if(transform.position.x < -11 && ResetPos == false)
        {
            CanShoot = false;
            ResetPos = true;
            StartCoroutine(ResetPosition());
        }  

        if(CanShoot)
        {
            if(!Reload)
            {
                if(EnemyWeaponFireTimer < EnemyWeaponFireRate)
                EnemyWeaponFireTimer += Time.deltaTime; 

                if(EnemyWeaponFireTimer >= EnemyWeaponFireRate)
                {
                    EnemyWeaponFireTimer = 0.0f;
                    EnemyShoot();
                    ShootCounter++; 
                }
                if(ShootCounter >= ShootCounterLimit)
                {
                    Reload = true;
                    StartCoroutine("ReloadEnemyWeapon");
                }
            }
        }
    }
    void EnemyShoot()
    {
        GameObject Bullet = Instantiate(EnemyBullet, GunPoint.position, GunPoint.rotation);
        Rigidbody2D BulletRb = Bullet.GetComponent<Rigidbody2D>();
        BulletRb.AddForce(GunPoint.up * BulletSpeed, ForceMode2D.Impulse); 
        Destroy(Bullet,6);
    }
    
    IEnumerator ReloadEnemyWeapon()
    {        
        yield return new WaitForSeconds(ReloadTimer);
        ShootCounter = 0;
        Reload = false;
    } 

    IEnumerator ResetPosition()
    {
        EnemyHealthScr.Immune = true;
        yield return new WaitForSeconds(3);
        transform.position = new Vector3(11,-4,0);
        ResetPos = false;
        yield return new WaitForSeconds(1);
        EnemyHealthScr.Immune = false;
        CanShoot = true;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            PlayerScr.PlayerTakeDamage(TankColliderDamage);
        }
    }

}
