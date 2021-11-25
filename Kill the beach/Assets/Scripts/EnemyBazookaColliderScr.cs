using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBazookaColliderScr : MonoBehaviour
{
    EnemyDamage EnemyDamage;

    void OnTriggerEnter2D(Collider2D other) 
    {       
       GameObject Player = GameObject.Find("Player");
       PlayerScr PlayerScr = Player.GetComponent<PlayerScr>();
       
        if(other.tag == "Player")
        {
            //if(EnemyDamage != null)
            EnemyDamage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyDamage>();
            PlayerScr.PlayerTakeDamage(EnemyDamage.EnemyHeavyProjectileDamage);
            
            Destroy(gameObject);
        }
        if(other.tag == "Enemy")
        {
            EnemyDamage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyDamage>();
            EnemyHealthScr EnemyHealthScr = other.GetComponent<EnemyHealthScr>();
            
            if(EnemyHealthScr != null)
                EnemyHealthScr.TakeDamage(EnemyDamage.EnemyHeavyProjectileDamage,false);

            Destroy(gameObject);
        }
    }
}
