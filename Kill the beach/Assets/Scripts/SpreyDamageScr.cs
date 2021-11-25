using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreyDamageScr : MonoBehaviour
{
    GameObject Player;
    GameObject UpgradeSys;
    int WeaponDamage;
    bool LifestealUp,DamageUp, Critical;
    bool IsCritical, IsCrit;
    int Damage;

    void Start() 
    {
        Player = GameObject.Find("Player");
        ShootScr ShootScr = Player.GetComponent<ShootScr>(); 
        WeaponDamage = ShootScr.WeaponDamage;  
        PlayerScr PlayerScr = Player.GetComponent<PlayerScr>(); 
        UpgradeSys = GameObject.Find("UpgradeSystem");
        UpgradeSystemScr UpgradeSystemScr = UpgradeSys.GetComponent<UpgradeSystemScr>();
        LifestealUp = UpgradeSystemScr.Lifesteal;
        DamageUp = UpgradeSystemScr.DamageUp;  
        Critical = UpgradeSystemScr.Critical;  
    }
    void OnParticleCollision(GameObject other) {

        int CritChance = UnityEngine.Random.Range(1,101);
        IsCritical = CritChance <= 10 ? true : false;
        if(IsCritical && Critical)
            IsCrit = true;
        else 
            IsCrit = false;


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
            int HitChance = Random.Range(1,4);
            if(HitChance == 1)
            {
                EnemyHealthScr EnemyHealthScr = other.GetComponent<EnemyHealthScr>();
                if(EnemyHealthScr != null)
                    EnemyHealthScr.TakeDamage(Damage,IsCrit);

                if(LifestealUp)
                {
                    PlayerScr.CurrentHp += (Damage * 3f)/100f;
                } 
            } 
        } 
    }
}
