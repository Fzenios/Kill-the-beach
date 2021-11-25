using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCitizenMeleeHit : MonoBehaviour
{
    IsMelee IsMelee;
    CitizenMovement CitizenMovement; 
    public float Timer;
    public float AttackSpeed;
    public bool Hit;
    EnemyDamage EnemyDamage;
    GameObject Player;
    PlayerScr PlayerScr;
    void Start()
    {
        IsMelee = gameObject.GetComponent<IsMelee>();
        CitizenMovement = gameObject.GetComponent<CitizenMovement>();
        Timer = 0;
        Player = GameObject.Find("Player");
        PlayerScr = Player.GetComponent<PlayerScr>();
        EnemyDamage EnemyDamage = GameObject.FindObjectOfType<EnemyDamage>();

    }
    void Update()
    {
        if(CitizenMovement.Distance <= CitizenMovement.EnemySafeDistance)
        {   
            if(Timer >= AttackSpeed)
            {
                Hit = true;
                Timer = 0;
            }
            else
            {
                Hit = false;
                Timer += Time.deltaTime;
            }
        }
        else 
        {
            Hit = false;
            Timer = 0;
        }
    }
    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            if(Hit)
            {
                //if(EnemyDamage != null)
                PlayerScr.PlayerTakeDamage(EnemyDamage.EnemyMeleeDamage);
            }     
        }   
    }
}
