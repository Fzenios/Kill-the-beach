using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PlayerScr : MonoBehaviour
{
    public float PlayerSpeed;
    public Rigidbody2D PlayerRb;
    public Camera Cam;
    Vector2 Movement;
    Vector2 MousePos;
    Vector2 MoveVelocity;
    public Slider Slider;
    public Text HpText;
    public TextMeshProUGUI SouvlakiCountTxt;
    public Animator animator;
    public static float MaxHp = 100, CurrentHp;
    public static bool CanMove = true;
    public int SouvlakiCount,SouvlakiCheckpoint,SouvlakiMaxCount;
    public GameObject Crosshair;
    bool FirstTimeSouvlaki;
    public GameObject Instructions, UpgradeBtnSystem;
    public InstructionsScr InstructionsScr;
    public Checkpoints Checkpoints;

    void Start()
    {
        FirstTimeSouvlaki = true;
        SouvlakiCount = 0;
    }
    void Awake() 
    {
        CurrentHp = MaxHp; 
    }

    void Update()
    {            
        if(CanMove)
        {
            Movement.x = Input.GetAxisRaw("Horizontal");
            Movement.y = Input.GetAxisRaw("Vertical");          

            if(transform.position.x <= -9 )
            transform.position = new Vector3 (-9f,transform.position.y,0);
            if(transform.position.x >= 9 )
            transform.position = new Vector3 (9f,transform.position.y,0);
            if(transform.position.y <= -5 )
            transform.position = new Vector3 (transform.position.x,-5,0);
            if(transform.position.y >= 2.3 )
            transform.position = new Vector3 (transform.position.x,2.3f,0);
        }
        else
        {
            Movement.x = 0;
            Movement.y = 0;    
        }
        
        if(!UpgradeSystemScr.SpeedUpBool)
            PlayerSpeed = 4;
        else
            PlayerSpeed = 6;

        MoveVelocity = Movement.normalized * PlayerSpeed;
        animator.SetFloat("Speed",Movement.sqrMagnitude);

        if(CurrentHp > MaxHp)
            CurrentHp = MaxHp;
            
        Slider.value = CurrentHp;
        //HpText.text = (float)Math.Round(CurrentHp,1) +"/"+MaxHp;
        
        SouvlakiCountTxt.text = SouvlakiCount + " x ";
    }       

    void FixedUpdate() 
    {
        MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        PlayerRb.MovePosition(PlayerRb.position + MoveVelocity * Time.fixedDeltaTime);
        Vector2 lookDir = MousePos - PlayerRb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;  
        PlayerRb.rotation = angle;      
    }    

    public void PlayerTakeDamage(int PlayerDamage)
    {
        if(!UpgradeSystemScr.DefenceUpBool)
            CurrentHp -= PlayerDamage;
        else
            CurrentHp -= PlayerDamage/2;

        if(CurrentHp <= 0)
        {
            Checkpoints.GameOver("DropHp");
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Souvlaki")
        {   
            SouvlakiCount++; 
            SouvlakiMaxCount++;
            if(FirstTimeSouvlaki)
            {
                FirstTimeSouvlaki = false;
                Time.timeScale = 0;
                InstructionsScr.i++;
                Instructions.SetActive(true);
                UpgradeBtnSystem.SetActive(true);
            }
        }
    }
}
