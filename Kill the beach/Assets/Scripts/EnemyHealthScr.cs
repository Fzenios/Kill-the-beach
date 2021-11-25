using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthScr : MonoBehaviour
{
    public float MaxHp;
    public float EnemyHp;
    public GameObject EnemyDeathAnim;
    public float DeathTimer;
    public Slider Slider;
    //public TextMeshProUGUI DamageText;
    public GameObject DamageTextObj;
    public bool Immune = false;
    public GameObject Souvlaki; 
    public static int MaxChanceOfSouvlaki = 20;
    bool Dying = false;
    
    void Start() 
    {
        EnemyHp = MaxHp;
        Slider.maxValue = MaxHp;
    }
    void Update() 
    {  
        Slider.value = EnemyHp;
    }
    public void TakeDamage(float Damage, bool IsCrit)
    {
        if(!Immune)
        {
            GameObject DamageTextObj2 = Instantiate(DamageTextObj,Slider.transform.position, Slider.transform.rotation);
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
            
            if(EnemyHp <= 0)
            {
                if(!Dying)
                {
                    Die();
                    Dying = true;
                }
            }
        }
    }
    void Die()
    {
        int ChanceOfSouvlaki = Random.Range(1,101);
        if(ChanceOfSouvlaki <= MaxChanceOfSouvlaki )
        {
            float RandomX = Random.Range(-2,2);
            float RandomY = Random.Range(-2,2);
            Vector2 SouvlakiVector = new Vector2(RandomX,RandomY);
            GameObject SouvlakiObj = Instantiate(Souvlaki, transform.position, transform.rotation);
            Rigidbody2D SouvlakiRb = SouvlakiObj.GetComponent<Rigidbody2D>();
            SouvlakiRb.AddForce(SouvlakiVector, ForceMode2D.Impulse);
        }
        GameObject DeathAnim = Instantiate(EnemyDeathAnim, transform.position, transform.rotation);
        Destroy(DeathAnim,DeathTimer);
        Destroy(gameObject);
    }
}
