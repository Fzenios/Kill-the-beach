using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScr : MonoBehaviour
{
    EnemyDamage EnemyDamage;

    void OnTriggerEnter2D(Collider2D other) 
    {       
       GameObject Player = GameObject.Find("Player");
       PlayerScr PlayerScr = Player.GetComponent<PlayerScr>();
       
        if(other.tag == "Player")
        {
            EnemyDamage EnemyDamage = GameObject.FindObjectOfType<EnemyDamage>();
            PlayerScr.PlayerTakeDamage(EnemyDamage.EnemyProjectileDamage);
            
            Destroy(gameObject);
        }
    }
}