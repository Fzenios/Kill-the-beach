using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    public GameObject ArmyRange, ArmyTank, ArmyBoss, ArmyBossEntrance;
    GameObject BossEntranceObj;
    public PlayerScr PlayerScr;
    public GameObject PlayerPos;
    public int CurrentSpowns = 0;
    public int SpownsToUpgrade;
    public int TotalSpowns = 40; 
    float PlusTimer = 0f;
    public float TotalTimer = 4f;
    public float CurrentSafeSpowns;
    float CurrentTimer = 0f; 
    public GameObject[] EnemiesCount;
    public DialogManagerScr DialogManagerScr;
    public bool BossFight = false;
    bool TankUp = false;
    public GameObject Warning; 
    public Checkpoints Checkpoints;


    void Start() 
    {
       // DialogManagerScr = GameObject.FindObjectOfType<DialogManagerScr>();      
    }

    void Update()
    {
        if(Checkpoints.ItsGameOver)
        {
            EnemySpownManager.Level4 = false;
            CurrentSpowns = 0;
            TankUp = false;
            PlusTimer = 0;
        }
        if(EnemySpownManager.Level4 && !PauseMenuScr.AllGamePauseEnabled)
        { 
            if(!TankUp && CurrentSpowns < TotalSpowns)
            {
                Instantiate(ArmyTank, transform.position, Quaternion.identity);  
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

                        
                        float Randomx = Random.Range(-6f,6f);
                        Vector3 EnemyPos3 = new Vector3(Randomx,-6.5f,0f);
                        SpownEnemy(EnemyPos3);                    
                        
                        CurrentTimer = 0;
                        CurrentSpowns += 3;

                        if(CurrentSpowns >= SpownsToUpgrade)
                        PlusTimer = 0.01f; 
                    }
                } 
            } 
            
            if(CurrentSpowns >= TotalSpowns && EnemiesCount.Length == 0 && BossFight == false)
            {
                Checkpoints.LastCheckpoint = 4.5f;
                PlayerScr.SouvlakiCheckpoint = PlayerScr.SouvlakiMaxCount;
                BossFight = true;
                StartCoroutine(SummonBoss());
                Warning.SetActive(true);   
                PlayerPos.transform.position = new Vector3(-0.5f,-2.5f,0); 
                PlayerScr.CanMove = false;
                FindObjectOfType<MusicSystem>().Stop("LevelMusic");
                FindObjectOfType<MusicSystem>().Play("BossMusic");
                return;                       
            }
            if(CurrentSpowns >= TotalSpowns && EnemiesCount.Length == 0 && BossFight == true)
            {
                FindObjectOfType<MusicSystem>().Stop("BossMusic");
                FindObjectOfType<MusicSystem>().Play("IntroMusic");
                EnemySpownManager.Level4 = false;
                DialogManagerScr.DialogIndex++;
            }      
        }      
    }

    void SpownEnemy(Vector3 EnemyPos)
    {
            Instantiate(ArmyRange,EnemyPos,Quaternion.identity);
    }

    IEnumerator SummonBoss()
    {
        yield return new WaitForSeconds(3);
        Warning.SetActive(false);
        Vector3 BossPos = new Vector3(0,0,0);
        BossEntranceObj = Instantiate(ArmyBossEntrance, BossPos, Quaternion.identity);
        yield return new WaitForSeconds(9);
        BossPos = new Vector3(0,0,0);
        Destroy(BossEntranceObj);
        Instantiate(ArmyBoss, BossPos, Quaternion.identity);
        PlayerScr.CanMove = true; 
    }
}
