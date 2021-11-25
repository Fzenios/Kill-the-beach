using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBulletScr : MonoBehaviour
{
    //public float radius = 1f;
    public GameObject Explosion;
    public GameObject ExplosionAni;
    //public float ExploasionForce = 10f;
    //Collider2D[] colliders;
    void OnTriggerEnter2D(Collider2D other) 
    {    
   
        if(other.tag == "Enemy")
        {            
            GameObject ExplosionEffect = Instantiate(Explosion, transform.position, transform.rotation);
            GameObject ExplosionAnim = Instantiate(ExplosionAni, transform.position, ExplosionAni.transform.rotation);
            Destroy(ExplosionAnim,1);
            Destroy(ExplosionEffect,1);
            Destroy(gameObject);
        }
           /* Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            
            foreach (Collider2D NearbyObject in colliders)
            {
                Vector2 direction = NearbyObject.transform.position - transform.position;
                Rigidbody2D rb = NearbyObject.GetComponent<Rigidbody2D>();
               
                    rb.AddForce(direction * ExploasionForce,ForceMode2D.Impulse );
                
            }
        } */
    } 

    /*void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius); 
    } */
}
