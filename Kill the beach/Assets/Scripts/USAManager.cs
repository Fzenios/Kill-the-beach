using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USAManager : MonoBehaviour
{
    public GameObject USABazooka, USAMarineMelee, USAMarineRange, USAPistol, USATank, USABoss;
    public int CurrentSpowns = 0;
    public int SpownsToUpgrade;
    public int TotalSpowns = 40; 
    float PlusTimer = 0f;
    public float TotalTimer = 4f;
    public float CurrentSafeSpowns;
    float CurrentTimer = 0f; 
    public GameObject[] EnemiesCount;
    public DialogManagerScr DialogManagerScr;
    bool TankUp = false;

    void Start()
    {
        //DialogManagerScr = GameObject.FindObjectOfType<DialogManagerScr>(); 
    }

    void Update()
    {
        if(Checkpoints.ItsGameOver)
        {
            EnemySpownManager.Level5 = false;
            CurrentSpowns = 0;
            TankUp = false;
            CancelInvoke("AnotherTank");
            PlusTimer = 0;
            CurrentSafeSpowns = 10;
        }
        if(EnemySpownManager.Level5 && !PauseMenuScr.AllGamePauseEnabled)
        { 
            if(!TankUp && CurrentSpowns < TotalSpowns)
            {
                Instantiate(USATank, transform.position, Quaternion.identity);  
                Invoke("AnotherTank",120);
                TankUp = true;
            }

            EnemiesCount = GameObject.FindGameObjectsWithTag("Enemy");   
            
            if(CurrentSpowns < TotalSpowns)
            {
                if(EnemiesCount.Length < CurrentSafeSpowns)
                {
                    CurrentTimer += Time.deltaTime + PlusTimer;
                
                    if(CurrentTimer > TotalTimer)
                    {
                        float Randomy = Random.Range(2f,-5f);
                        Vector3 EnemyPos = new Vector3(-11f,Randomy,0f);
                        SpownEnemy(EnemyPos);

                        
                        float Randomy2 = Random.Range(2f,-5f);
                        Vector3 EnemyPos2 = new Vector3(11f,Randomy2,0f);
                        SpownEnemy(EnemyPos2);                  
                        
                        CurrentTimer = 0;
                        CurrentSpowns += 2;

                        if(CurrentSpowns >= SpownsToUpgrade)
                        {
                            PlusTimer = 0.01f; 
                            CurrentSafeSpowns = 12;
                        }
                    
                        int RandomMarine = Random.Range(1,3);
                    
                        float RandomPos = Random.Range(-8.5f,8.5f);
                        Vector3 MarinePos = new Vector3(RandomPos,4.6f,0f);
                        CurrentSpowns++;

                        if(RandomMarine == 1)
                        {        
                            Instantiate(USAMarineMelee,MarinePos,Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(USAMarineRange,MarinePos,Quaternion.identity);
                        }
                    
                    }
                    
                } 
            } 
            
            if(CurrentSpowns >= TotalSpowns && EnemiesCount.Length == 0 )
            {
                EnemySpownManager.Level5 = false;
                DialogManagerScr.DialogIndex++;
            }      
        }      
    }
    void SpownEnemy(Vector3 EnemyPos)
    {
        int RandomUSA = Random.Range(1,3);
        if(RandomUSA == 1)        
        Instantiate(USAPistol,EnemyPos,Quaternion.identity);
        else
        Instantiate(USABazooka,EnemyPos,Quaternion.identity);
    }
    void AnotherTank()
    {

        Instantiate(USATank, transform.position, Quaternion.identity);  
    }
}
