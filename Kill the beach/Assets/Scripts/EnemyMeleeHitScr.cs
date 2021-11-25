using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeHitScr : MonoBehaviour
{

   /* void OnTriggerEnter2D(Collider2D other) 
    {
        GameObject Player = GameObject.Find("Player");
        PlayerScr PlayerScr = Player.GetComponent<PlayerScr>();
        EnemyDamage EnemyDamage = GameObject.FindObjectOfType<EnemyDamage>();
        
        if(other.tag == "Player")
        {
            PlayerScr.PlayerTakeDamage(EnemyDamage.EnemyNormalDamage);
        }  
        
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthScr>().TakeDamage(5);
            //PlayerScr.PlayerTakeDamage(EnemyDamage.EnemyNormalDamage);
        }  
    }
    /**void OnTriggerStay2D(Collider2D other) 
    {
        GameObject Player = GameObject.Find("Player");
        PlayerScr PlayerScr = Player.GetComponent<PlayerScr>();
        EnemyDamage EnemyDamage = Enemy.GetComponent<EnemyDamage>();       
        
        if(other.tag == "Player")
        {
            PlayerScr.PlayerTakeDamage(EnemyDamage.EnemyNormalDamage);
        }  
        
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthScr>().TakeDamage(1);
            //PlayerScr.PlayerTakeDamage(EnemyDamage.EnemyNormalDamage);
        }  
    }*/
}
