using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAttack : MonoBehaviour
{
    float Timer;
    public float MaxTimer;
    public int AllyDamage;
    public Animator animator;

    void Start() 
    {
        Timer = 0;  
    }    

    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {    
            Timer += Time.deltaTime;         
            if(Timer > MaxTimer)
            {
                animator.SetBool("Hit",true);
                EnemyHealthScr EnemyHealthScr = other.GetComponent<EnemyHealthScr>();
                EnemyHealthScr.TakeDamage(AllyDamage,false);
                Timer = 0;
            }
        
        }            
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
            animator.SetBool("Hit",false);        
    }

}
