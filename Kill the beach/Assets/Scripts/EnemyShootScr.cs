using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScr : MonoBehaviour
{
    public GameObject EnemyBullet;
    public Transform GunPoint;
    public CitizenMovement CitizenMovement;
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
       
    }
    void Update()
    {
        if(CitizenMovement.Distance <= CitizenMovement.EnemySafeDistance)
        {
            if(!Reload)
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
