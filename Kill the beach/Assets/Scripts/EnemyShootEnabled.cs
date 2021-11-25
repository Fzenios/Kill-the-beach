using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootEnabled : MonoBehaviour
{
   GameObject Player;
    Transform PlayerPos;
    BossTankScr BossTankScr;
    public SpriteRenderer SpriteRendererr;
    public GameObject EnemyBullet;
    public Transform GunPoint;
    public float BulletSpeed;    
    float EnemyWeaponFireTimer;
    public float EnemyWeaponFireRate;
    int ShootCounter;
    public int ShootCounterLimit;
    bool Reload;
    public float ReloadTimer;
    public Animator animator;


    void Start()
    {
        Player = GameObject.Find("Player");  
        PlayerPos = Player.transform;
        BossTankScr = GameObject.FindObjectOfType<BossTankScr>();
    }
    void Update()
    {
        Vector3 direction = PlayerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
        //EnemyRb.rotation = angle;    
        SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,angle);    
        
        if(BossTankScr.CanShoot)
        {
            if(!Reload)
            {
                if(EnemyWeaponFireTimer < EnemyWeaponFireRate)
                EnemyWeaponFireTimer += Time.deltaTime; 

                if(EnemyWeaponFireTimer > EnemyWeaponFireRate)
                {
                    EnemyWeaponFireTimer = 0.0f;
                    EnemyShoot();
                    animator.SetBool("Hit",true);
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
}
