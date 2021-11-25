using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaCode : MonoBehaviour
{
    Transform PlayerPos;
    public SpriteRenderer SpriteRendererr;
    PlayerScr PlayerScr;
    void Start()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        PlayerPos = Player.GetComponent<Transform>();
        PlayerScr = Player.GetComponent<PlayerScr>();

        Vector3 direction = PlayerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
        SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,angle); 

        Rigidbody2D BulletRb = transform.GetComponent<Rigidbody2D>();
        BulletRb.AddForce(transform.up * 13 , ForceMode2D.Impulse); 
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<MusicSystem>().SoundEffects("ColaCan"); 
            PlayerScr.PlayerTakeDamage(7);
            Destroy(gameObject);
        }
    
    }

}
