using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeavyBulletScr : MonoBehaviour
{
    EnemyDamage EnemyDamage;

    void Start()
    {
       GameObject Player = GameObject.Find("Player");
       PlayerScr PlayerScr = Player.GetComponent<PlayerScr>();
       EnemyDamage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyDamage>();
      // Vector2 Target = new Vector2 (Player.transform.position.x, Player.transform.position.y);

    }

    void OnTriggerEnter2D(Collider2D other) 
    {       
       GameObject Player = GameObject.Find("Player");
       PlayerScr PlayerScr = Player.GetComponent<PlayerScr>();
       
        if(other.tag == "Player")
        {
            EnemyDamage EnemyDamage = GameObject.FindObjectOfType<EnemyDamage>();
            PlayerScr.PlayerTakeDamage(EnemyDamage.EnemyHeavyProjectileDamage);
            Destroy(gameObject);
        }
    }
}
