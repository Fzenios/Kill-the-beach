using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBazookaScr : MonoBehaviour
{
    public GameObject Explosion;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" || other.tag == "Enemy")
        {
            GameObject ExplosionEffect = Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(ExplosionEffect,1);
            Destroy(gameObject);
        }    

    }



}
