using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public static int EnemyMeleeDamage;
    public static int EnemyProjectileDamage;
    public static int EnemyBossDamage;
    public static int EnemyBossProjectileDamage;
    public static int EnemyHeavyProjectileDamage;
    public static int EnemyHoundDamage;
    public EnemySpownManager EnemySpownManager;

    void Update() 
    {
        if(EnemySpownManager.Level1)
        {
            EnemyMeleeDamage = 2;
            EnemyBossDamage = 20;
        }
        if(EnemySpownManager.Level2)
        {
            EnemyMeleeDamage = 5;
            EnemyProjectileDamage = 3;
            EnemyBossProjectileDamage = 13;
            EnemyHeavyProjectileDamage = 25;
        }
        if(EnemySpownManager.Level3)
        {
            EnemyMeleeDamage = 6;
            EnemyProjectileDamage = 6;
            EnemyBossProjectileDamage = 5;
            EnemyHoundDamage = 12;
        }
        if(EnemySpownManager.Level4)
        {
            EnemyProjectileDamage = 6;
            EnemyHeavyProjectileDamage = 15;
        }
        if(EnemySpownManager.Level5)
        {
            EnemyMeleeDamage = 12;
            EnemyProjectileDamage = 8;
            EnemyHeavyProjectileDamage = 15;
        }
    }                 
}
