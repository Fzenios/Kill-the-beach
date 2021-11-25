using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCitizenBossScr : MonoBehaviour
{
    public GameObject EnemyBullet, EnemyBullet2, EnemyHeavyBullet;
    public Transform GunPoint1, GunPoint2, HeavyGunPoint;
    public float BulletSpeed, HeavyBulletSpeed;    
    float EnemyWeaponFireTimer;
    public float EnemyWeaponFireRate;
    int ShootCounter;
    public int ShootCounterLimit;
    int TimeCounter = 0;
    bool Reload, HeavyShot;
    public float ReloadTimer;
    GameObject Bullet, HeavyBullet;
    public Animator animator;  
    int RandomHand;  

    GameObject Player;
    Transform PlayerPos;
    public SpriteRenderer SpriteRendererr;
    public float EnemySpeed;
    public float EnemySafeDistance;
    public float EnemyUnSafeDistance;
    float Distance;
    bool BlockMovementBool = false;
    bool BossEntrance;

    void Start()
    {     
        Player = GameObject.Find("Player");  
        PlayerPos = Player.transform;
        StartCoroutine(BlockMovement());
        RandomHand = 1; 
        BossEntrance = true; 
    }
    void Update()
    {
        if(!BossEntrance)
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

            if(!Reload && !HeavyShot)
            {
                if(EnemyWeaponFireTimer < EnemyWeaponFireRate)
                EnemyWeaponFireTimer += Time.deltaTime; 

                if(EnemyWeaponFireTimer >= EnemyWeaponFireRate)
                {
                    EnemyWeaponFireTimer = 0.0f;
                    EnemyShoot();
                    animator.SetBool("Hit",true);
                    ShootCounter++; 
                }
                if(ShootCounter >= ShootCounterLimit)
                {
                    Reload = true;
                    TimeCounter++;
                    StartCoroutine("ReloadEnemyWeapon");
                }
            }
            if(TimeCounter == 2)
            {
                HeavyShot = true; 
                TimeCounter = 0;
                StartCoroutine(HeavyShootSetup());
            }
        }
    }
    
    void EnemyShoot()
    {
        RandomHand = RandomHand == 2? 1:2 ;

        if(RandomHand == 1 )
        {
            Bullet = Instantiate(EnemyBullet2, GunPoint1.position, GunPoint1.rotation);
        }
        else 
        {
            Bullet = Instantiate(EnemyBullet, GunPoint2.position, GunPoint2.rotation);
        }

        Rigidbody2D BulletRb = Bullet.GetComponent<Rigidbody2D>();
        BulletRb.AddForce(GunPoint1.up * BulletSpeed, ForceMode2D.Impulse); 
        Destroy(Bullet,6);
    }
    
    void HeavyShotShoot()
    {
        HeavyBullet = Instantiate(EnemyHeavyBullet, HeavyGunPoint.position, HeavyGunPoint.rotation);
        Rigidbody2D HeavyBulletRb = HeavyBullet.GetComponent<Rigidbody2D>();
        HeavyBulletRb.AddForce(HeavyGunPoint.up * HeavyBulletSpeed, ForceMode2D.Impulse); 
        Destroy(HeavyBullet,6);
    }
    
    IEnumerator ReloadEnemyWeapon()
    {        
        yield return new WaitForSeconds(ReloadTimer);
        ShootCounter = 0;
        Reload = false;
    } 

    IEnumerator HeavyShootSetup()
    {
        yield return new WaitForSeconds(4);
        HeavyShotShoot();
        yield return new WaitForSeconds(4);
        HeavyShot = false;
    }
    IEnumerator BlockMovement()
    {
        yield return new WaitForSeconds(5);
        BlockMovementBool = true;
        BossEntrance = false;
    }
}
