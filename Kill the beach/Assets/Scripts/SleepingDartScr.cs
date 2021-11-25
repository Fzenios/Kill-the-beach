using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingDartScr : MonoBehaviour
{
    Transform PlayerPos;
    public SpriteRenderer SpriteRendererr;
    PlayerScr PlayerScr;
    public GameObject PlayerCredits;

    void Start()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        PlayerPos = Player.GetComponent<Transform>();
        PlayerScr = Player.GetComponent<PlayerScr>();

        Vector3 direction = PlayerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
        SpriteRendererr.transform.eulerAngles = new Vector3 (0,0,angle); 

        Rigidbody2D BulletRb = transform.GetComponent<Rigidbody2D>();
        BulletRb.AddForce(transform.up * 10 , ForceMode2D.Impulse); 
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            other.gameObject.SetActive(false);
            GameObject PlayerCreditsObj = Instantiate(PlayerCredits, other.transform.position, PlayerCredits.transform.rotation); 
        }
    }
}
