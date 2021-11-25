using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossBulletScr : MonoBehaviour
{
    EnemyDamage EnemyDamage;
    PlayerScr PlayerScr ;

    void OnTriggerEnter2D(Collider2D other) 
    {       
        GameObject Player = GameObject.Find("Player");
        if(Player != null)
        {
            PlayerScr = Player.GetComponent<PlayerScr>(); 
        }
        if(other.tag == "Player")
        {
            EnemyDamage EnemyDamage = GameObject.FindObjectOfType<EnemyDamage>();
            PlayerScr.PlayerTakeDamage(EnemyDamage.EnemyBossProjectileDamage);
            
            Destroy(gameObject);
        }
    }
}
