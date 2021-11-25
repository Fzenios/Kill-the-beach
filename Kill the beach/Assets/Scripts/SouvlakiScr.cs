using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouvlakiScr : MonoBehaviour
{
    PlayerScr PlayerScr;
    float Timer;
    public Animator animator;
    public GameObject Cloud;
    bool SouvlakiDead; 
    void Start() 
    {   SouvlakiDead = false;
        PlayerScr = GameObject.FindObjectOfType<PlayerScr>();
    }

    void Update() 
    {
        Timer += Time.deltaTime;
        if(Timer>= 9 && !SouvlakiDead)
        {   SouvlakiDead = true;
            animator.enabled = true;
            StartCoroutine(CloudAnim());
            Destroy(gameObject,5.1f);
        }
        if(transform.position.x <= -9 )
            transform.position = new Vector3 (-9f,transform.position.y,0);
        if(transform.position.x >= 9 )
            transform.position = new Vector3 (9f,transform.position.y,0);
        if(transform.position.y <= -5 )
            transform.position = new Vector3 (transform.position.x,-5,0);
        if(transform.position.y >= 2.3 )
            transform.position = new Vector3 (transform.position.x,2.3f,0);
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            if(!UpgradeSystemScr.SouvlakiHealBool)
                PlayerScr.CurrentHp += 10;
            else
                PlayerScr.CurrentHp += 15;
            
            FindObjectOfType<MusicSystem>().SoundEffects("SouvlakiPickUp"); 
            Destroy(gameObject);
        }        
    }
    IEnumerator CloudAnim()
    {
        yield return new WaitForSeconds(5);
        GameObject CloudObj = Instantiate(Cloud, transform.position, Cloud.transform.rotation );
        Destroy(CloudObj,1);
    }

}
