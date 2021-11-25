using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankScr : MonoBehaviour
{
    public GameObject TankTrail;
    GameObject Player;
    Transform PlayerPos;
    PlayerScr PlayerScr;
    public GameObject Soldier;
    GameObject Soldier1, Soldier2, Soldier3;
    public Transform SoldSpownPos1, SoldSpownPos2, SoldSpownPos3;
    public EnemyHealthScr EnemyHealthScr;
    public SpriteRenderer SpriteRendererr;
    public float SlowSpeed, FastSpeed;
    float TankSpeed;
    public int TankColliderDamage;
    bool GoingDown = false;
    bool GoLeft = true;
    int RandomAttack;
    bool Attacking;
    bool ChangePhase;
    public GameObject EnemyBullet;
    public Transform GunPoint;
    public float BulletSpeed;    
    float EnemyWeaponFireTimer;
    public float EnemyWeaponFireRate;
    int ShootCounter;
    public int ShootCounterLimit;
    bool Reload; 
    public bool CanShoot;
    public float ReloadTimer;
    public Animator animator;

    void Start()
    {
        ChangePhase = false;
        Player = GameObject.Find("Player");  
        PlayerPos = Player.transform;
        PlayerScr = Player.GetComponent<PlayerScr>();

        transform.position = new Vector3(13f,1.3f,0);
        Attacking = true;
        RandomAttack = 1;
        CanShoot = true;

       // transform.rotation = new Quaternion(0,180,0,0);
       // InvokeRepeating("ShowTrail",0f,0.08f);
    }

    void Update()
    {
        Vector3 direction = PlayerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;  
        SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,angle); 

        if(EnemyHealthScr.EnemyHp <= EnemyHealthScr.MaxHp/2 && !ChangePhase)
        {
            ChangePhase = true;
            StartCoroutine(PhaseChange());
        }

        if(!EnemyHealthScr.Immune)
        {
            if(RandomAttack == 0 && Attacking)
            {
                TankSpeed = FastSpeed;
                CanShoot = false;
                Movement();      
            }
            else if(RandomAttack == 1 && Attacking)
            {
                TankSpeed = SlowSpeed;
                CanShoot = true;
                Movement();      
            }
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

    IEnumerator WaitForDown()
    {
        yield return new WaitForSeconds(0.5f);
        GoingDown = false;
    }
    IEnumerator ResetAttack()
    {
        transform.position = new Vector3(13f,1.3f,0);
        GoLeft = true;
        CanShoot = false;
        yield return new WaitForSeconds(3f);
        Attacking = true;
        RandomAttack = Random.Range(0,2);
        CanShoot = true;
        EnemyHealthScr.Immune = false;
    }
    IEnumerator PhaseChange()
    {
        SpriteRendererr.enabled = false;
        animator.SetBool("Phase2", true);
        EnemyHealthScr.Immune = true;
        CanShoot = false;
        yield return new WaitForSeconds(7f);
        Soldier1 = Instantiate(Soldier, SoldSpownPos1, SoldSpownPos1);
        Soldier1.transform.position = SoldSpownPos1.transform.position;
        Soldier2 = Instantiate(Soldier, SoldSpownPos2, SoldSpownPos2);
        Soldier2.transform.position = SoldSpownPos2.transform.position;
        Soldier3 = Instantiate(Soldier, SoldSpownPos3, SoldSpownPos3);
        Soldier3.transform.position = SoldSpownPos3.transform.position;

        FastSpeed += 3;
        SlowSpeed += 2;
        BulletSpeed += 0.8f;
            
        CanShoot = true;
        EnemyHealthScr.Immune = false;
        SpriteRendererr.enabled = true;
        animator.SetBool("Phase2", false);
    }

    void Movement()
    {
        if(GoLeft)
            {
                transform.position += new Vector3(-Time.deltaTime * TankSpeed,0,0); 
            }
        else
            {
                transform.position += new Vector3(Time.deltaTime * TankSpeed,0,0); 
            }
        
        ShowTrail();

        if(transform.position.x < -12.9  || transform.position.x > 13.5)
        {
            if(!GoingDown)
            {
                transform.position -= new Vector3(0,2.05f,0);
                GoLeft = !GoLeft;
                /*if(GoLeft)
                    transform.rotation = new Quaternion(0,180,0,0);
                else
                    transform.rotation = new Quaternion(0,0,0,0);*/

                GoingDown = true;
                StartCoroutine(WaitForDown());
            }
        }
        if(transform.position.y <= -6.8f)
        {
            EnemyHealthScr.Immune = true;
            Attacking = false;
            StartCoroutine(ResetAttack());
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

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            PlayerScr.PlayerTakeDamage(TankColliderDamage);
        }
    }

    void ShowTrail()
    {
        GameObject TankTrailObj = Instantiate(TankTrail, transform.position, transform.rotation);
        //TankTrailObj.transform.SetParent(null);
        Destroy(TankTrailObj,2.5f);        
    }
}
