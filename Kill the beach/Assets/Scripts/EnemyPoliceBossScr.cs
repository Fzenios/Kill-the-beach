using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoliceBossScr : MonoBehaviour
{
    bool PrepareHounds;
    public bool SendHounds;
    public EnemyHealthScr EnemyHealthScr;
    public GameObject HountObj1, HountObj2, HountObj3, HountObj4 ;
    public Transform HountPos1, HountPos2, EnemyFaceTrans;
    public static int HountCount = 0;
    bool HountsDead = true;
    public bool Phase1, Phase2; 
    EnemyShootScr EnemyShootScr;
    CitizenMovement CitizenMovement;    
    EnemyDamage EnemyDamage; 
    public Animator animator;
    void Start() 
    {
        EnemyShootScr = GetComponent<EnemyShootScr>();  
        CitizenMovement = GetComponent<CitizenMovement>();
        EnemyDamage = GetComponent<EnemyDamage>();

        EnemyHealthScr.Immune = true;
        PrepareHounds = true;
        EnemyShootScr.enabled = false; 
        CitizenMovement.enabled = true;
        Phase1 = true;
    }
    
    void Update() 
    {
        if(HountCount == 0 && HountsDead)
        {
            animator.SetBool("CallHounds", true);
            PrepareHounds = true;
            HountsDead = false;
            SendHounds = false;
            EnemyShootScr.enabled = false;
            EnemyHealthScr.Immune = false;  
            CitizenMovement.enabled = false;   
        }

        if(PrepareHounds)
        {
            PrepareHounds = false;
            StartCoroutine(Preparehounts());
            StartCoroutine(PrepareTime());
        }
        if(EnemyHealthScr.EnemyHp <= EnemyHealthScr.MaxHp/2 && Phase1)
        {
            animator.SetBool("CallHounds", false);
            animator.SetBool("Phase2", true);
            StopAllCoroutines();
            Phase1 = false;
            Phase2 = true;
            EnemyHealthScr.Immune = true;
            EnemyDamage.EnemyProjectileDamage = 7;
            EnemyShootScr.ShootCounterLimit = 10;
            StartCoroutine(PreparehountsOnce());
        }
        if(EnemyHealthScr.EnemyHp <= 0)
        {
            StopAllCoroutines();
            SendHounds = true;
        }
    }

    IEnumerator Preparehounts()
    {
        yield return new WaitForSeconds(2.5f);
        GameObject Hount1 = Instantiate(HountObj1, HountPos1, HountPos1);
        Hount1.transform.position = HountPos1.position;
        Hount1.transform.rotation = HountPos1.rotation;
        HountCount++;
        //Hount1.transform.SetParent(EnemyFaceTrans.transform);
        GameObject Hount2 = Instantiate(HountObj2, HountPos2, HountPos2);
        Hount2.transform.position = HountPos2.position;
        Hount2.transform.rotation = HountPos2.rotation;
        HountCount++;
        //Hount2.transform.SetParent(EnemyFaceTrans.transform);

        if(Phase2)
        {
            yield return new WaitForSeconds(4);
            GameObject Hount3 = Instantiate(HountObj3, HountPos1, HountPos1);
            Hount3.transform.position = HountPos1.position;
            Hount3.transform.rotation = HountPos1.rotation;
            HountCount++;
            //Hount1.transform.SetParent(EnemyFaceTrans.transform);
            GameObject Hount4 = Instantiate(HountObj4, HountPos2, HountPos2);
            Hount4.transform.position = HountPos2.position;
            Hount4.transform.rotation = HountPos2.rotation;
            HountCount++;
        }

        HountsDead = true;
    }

    IEnumerator PrepareTime()
    {
        yield return new WaitForSeconds(5f);
        EnemyShootScr.enabled = true;        
        SendHounds = true;
        EnemyHealthScr.Immune = true;
        CitizenMovement.enabled = true;   
    }

    IEnumerator PreparehountsOnce()
    {  
        
        if(HountCount == 0)
        {   
            yield return new WaitForSeconds(4);

            GameObject Hount1 = Instantiate(HountObj1, HountPos1, HountPos1);
            Hount1.transform.position = HountPos1.position;
            Hount1.transform.rotation = HountPos1.rotation;
            HountCount++;
            //Hount1.transform.SetParent(EnemyFaceTrans.transform);
            GameObject Hount2 = Instantiate(HountObj2, HountPos2, HountPos2);
            Hount2.transform.position = HountPos2.position;
            Hount2.transform.rotation = HountPos2.rotation;
            HountCount++;
            //Hount2.transform.SetParent(EnemyFaceTrans.transform);
            yield return new WaitForSeconds(2);
            animator.SetBool("Phase2", false);
            yield return new WaitForSeconds(0.5f);
           
            GameObject Hount3 = Instantiate(HountObj3, HountPos1, HountPos1);
            Hount3.transform.position = HountPos1.position;
            Hount3.transform.rotation = HountPos1.rotation;
            HountCount++;
            //Hount1.transform.SetParent(EnemyFaceTrans.transform);
            GameObject Hount4 = Instantiate(HountObj4, HountPos2, HountPos2);
            Hount4.transform.position = HountPos2.position;
            Hount4.transform.rotation = HountPos2.rotation;
            HountCount++;
        }
        else if(HountCount == 2)
        {
            yield return new WaitForSeconds(6);
            animator.SetBool("Phase2", false);
            yield return new WaitForSeconds(0.5f);
            GameObject Hount3 = Instantiate(HountObj3, HountPos1, HountPos1);
            Hount3.transform.position = HountPos1.position;
            Hount3.transform.rotation = HountPos1.rotation;
            HountCount++;
            //Hount1.transform.SetParent(EnemyFaceTrans.transform);
            GameObject Hount4 = Instantiate(HountObj4, HountPos2, HountPos2);
            Hount4.transform.position = HountPos2.position;
            Hount4.transform.rotation = HountPos2.rotation;
            HountCount++;
        }

        HountsDead = true;
        EnemyShootScr.enabled = true;        
        SendHounds = true;
        EnemyHealthScr.Immune = true;
        CitizenMovement.enabled = true;
        animator.SetBool("Phase2", false);   
    }
}
