using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoundScr : MonoBehaviour
{
    Rigidbody2D Rb;
    GameObject Player;
    Transform PlayerPos;
    public Transform EnemyFace;
    Transform HoundSpot;
    GameObject PoliceBoss;
    EnemyPoliceBossScr EnemyPoliceBossScr;
    EnemyHealthScr EnemyHealthScr;
    PlayerScr PlayerScr;
    public float RotateSpeed, HoundSpeed;
    public float EnemyMaxHp;
    float EnemyHp;
    public Slider Slider;
    public GameObject DamageTextObj;
    bool HoundImmune = true;
    bool Chasing = true;
    bool Returning = false;
    EnemyDamage EnemyDamage;
    public Animator animator;
    
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerPos = Player.transform;
        PlayerScr = Player.GetComponent<PlayerScr>();
        EnemyDamage = GameObject.FindObjectOfType<EnemyDamage>();


        PoliceBoss = GameObject.FindGameObjectWithTag("Enemy");
        EnemyPoliceBossScr = PoliceBoss.GetComponent<EnemyPoliceBossScr>();
        EnemyHealthScr = PoliceBoss.GetComponent<EnemyHealthScr>();
        HoundSpot = GameObject.FindGameObjectWithTag("HoundSpot").transform;

        animator.SetBool("Idle", true);
        
        EnemyHp = EnemyMaxHp;
        Slider.maxValue = EnemyMaxHp;
    }
    void Update() 
    {
        if(EnemyPoliceBossScr.SendHounds)
            animator.SetBool("Idle", false);

        if(EnemyPoliceBossScr.SendHounds && Chasing) 
        {
            transform.SetParent(null);
            HoundImmune = false;
        }          
        if(EnemyPoliceBossScr.SendHounds && EnemyHp <= 0)
        {
            Chasing = false;
            Returning = true;
            HoundImmune = true;
        }

        Slider.value = EnemyHp;  

        if(EnemyHealthScr.EnemyHp <= 0) 
        {
            Returning = true;
        }
    }

    void FixedUpdate() 
    {
        if(EnemyPoliceBossScr.SendHounds && Chasing)
        {
            Vector2 Direction = ((Vector2)PlayerPos.position - Rb.position).normalized;
            
            float RotateAmount = Vector3.Cross(Direction, -transform.right).z;
            Rb.angularVelocity = -RotateAmount * RotateSpeed;
            Rb.velocity = -transform.right * HoundSpeed;
        }

        if(EnemyPoliceBossScr.SendHounds && Returning)
        {
            Vector2 Direction = ((Vector2)HoundSpot.position - Rb.position).normalized;
            
            float RotateAmount = Vector3.Cross(Direction, -transform.right).z;
            Rb.angularVelocity = -RotateAmount * RotateSpeed;
            Rb.velocity = -transform.right * HoundSpeed;
        }
    }
    public void TakeDamage(float Damage,bool IsCrit)
    {
        if(!HoundImmune)
        {
            GameObject DamageTextObj2 = Instantiate(DamageTextObj,Slider.transform.position, DamageTextObj.transform.rotation);
            TextMeshProUGUI DamageTextObject = DamageTextObj2.GetComponent<TextMeshProUGUI>();
            //Transform EnemyCanvas = GameObject.Find("EnemyCanvas").transform;
            Transform EnemyCanvas = GameObject.Find("EnemyCanvas").transform;
            DamageTextObj2.transform.SetParent(EnemyCanvas.transform);
            if(!IsCrit)
            {
                //DamageTextObject.transform.localScale = new Vector3 (1f,1f,1f);
                DamageTextObject.fontSize = 0.3f;
            }
            else 
            {
                DamageTextObject.fontSize = 0.4f;
                DamageTextObject.color = Color.red;
            }
            
            Rigidbody2D DamageTextRb =  DamageTextObj2.GetComponent<Rigidbody2D>();
            float randomx = Random.Range(-3f,3f);
            //float randomy = Random.Range(-3,3);
            Vector2 DamagetextVector = new Vector2(randomx,3);
            
            DamageTextRb.AddForce(DamagetextVector,ForceMode2D.Impulse);
            DamageTextObject.text = Damage.ToString();
            Destroy(DamageTextObj2,1);
            
            EnemyHp -= Damage;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "HoundSpot" && Returning) 
        {
            EnemyPoliceBossScr.HountCount--;
            Destroy(gameObject);
        } 

        if(other.tag == "Player" && !Returning)
        {
            PlayerScr.PlayerTakeDamage(EnemyDamage.EnemyHoundDamage);
        }
   
    }
}
