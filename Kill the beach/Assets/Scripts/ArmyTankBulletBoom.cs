using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyTankBulletBoom : MonoBehaviour
{
    Transform PlayerPos;
    public SpriteRenderer SpriteRendererr;
    PlayerScr PlayerScr;
    public GameObject Explosion; 
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
        Destroy(gameObject,3);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            PlayerScr.CurrentHp = 1;
            GameObject Explosionobj = Instantiate(Explosion,transform.position, Quaternion.identity);
            Destroy(Explosionobj,1);
            Destroy(gameObject);
        }
    
    }


}
