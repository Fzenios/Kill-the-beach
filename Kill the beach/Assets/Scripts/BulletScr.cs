using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BulletScr : MonoBehaviour
{
    GameObject Player;
    GameObject UpgradeSys;
    int WeaponDamage;
    bool DamageUp, Critical;
    bool LifestealUp;
    int Damage;
    float CurrentHp;
    bool IsCritical, IsCrit;
    public GameObject BulletHit;

    void Start() 
    {
        Player = GameObject.Find("Player");
        ShootScr ShootScr = Player.GetComponent<ShootScr>(); 
        WeaponDamage = ShootScr.WeaponDamage;
        PlayerScr PlayerScr = Player.GetComponent<PlayerScr>();             

        UpgradeSys = GameObject.Find("UpgradeSystem");
        UpgradeSystemScr UpgradeSystemScr = UpgradeSys.GetComponent<UpgradeSystemScr>();
        DamageUp = UpgradeSystemScr.DamageUp;
        Critical = UpgradeSystemScr.Critical;
        LifestealUp = UpgradeSystemScr.Lifesteal;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        int CritChance = UnityEngine.Random.Range(1,101);
        IsCritical = CritChance <= 10 ? true : false;
        if(IsCritical && Critical)
            IsCrit = true;

        if(!DamageUp & !Critical)
        {
            Damage = WeaponDamage;
        }
        else if (DamageUp & !Critical)
        {
            Damage = WeaponDamage + ((WeaponDamage * 25)/100);
        }
        else if(!DamageUp & Critical)
        {
            if(IsCritical)
            {
                Damage = WeaponDamage*2;
            }
            else
            {
                Damage = WeaponDamage; 
            }
        }
        else if(DamageUp & Critical)
        {
            if(IsCritical)
            {
                Damage = (WeaponDamage + ((WeaponDamage * 25)/100))*2;
            }
            else
            {
                Damage = WeaponDamage + ((WeaponDamage * 25)/100);
            }
        }

        if(other.tag == "Enemy" )
        {
            EnemyHealthScr EnemyHealthScr = other.GetComponent<EnemyHealthScr>();
            if(EnemyHealthScr != null)
                EnemyHealthScr.TakeDamage(Damage,IsCrit);

            Destroy(gameObject);

            if(LifestealUp)
            {
                PlayerScr.CurrentHp += (Damage * 1.5f)/100f;
            }  
            GameObject BulletHitObj = Instantiate(BulletHit, transform.position, transform.rotation);
            Destroy(BulletHitObj, 0.5f);
        } 

        if(other.tag == "Hound" )
        {
            Destroy(gameObject);
            HoundScr HoundScr = other.GetComponent<HoundScr>();
            HoundScr.TakeDamage(Damage,IsCrit);

            if(LifestealUp)
            {
                PlayerScr.CurrentHp += (Damage * 1.5f)/100f;
            }  
            GameObject BulletHitObj = Instantiate(BulletHit, transform.position, transform.rotation);
            Destroy(BulletHitObj, 0.5f);
        }
    }

}
