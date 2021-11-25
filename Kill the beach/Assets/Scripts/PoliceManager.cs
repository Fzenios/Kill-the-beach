using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceManager : MonoBehaviour
{
    public GameObject PoliceMelee, PoliceRange, PoliceBoss, PoliceBossEntrance;
    GameObject PoliceBossEntranceObj; 
    public PlayerScr PlayerScr;
    public GameObject PlayerPos;
    public int CurrentSpowns = 0;
    public int SpownsToUpgrade;
    public int TotalSpowns = 40; 
    public float CurrentSafeSpowns;
    float PlusTimer = 0f;
    public float TotalTimer = 4f;
    float CurrentTimer = 0f; 
    public GameObject[] EnemiesCount;
    public DialogManagerScr DialogManagerScr;
    public bool BossFight = false;
    public GameObject Warning; 
    public Checkpoints Checkpoints;

    

    void Start() 
    {
        //DialogManagerScr = GameObject.FindObjectOfType<DialogManagerScr>();        
    }

    void Update()
    {
        if(Checkpoints.ItsGameOver)
        {
            EnemySpownManager.Level3 = false;
            CurrentSpowns = 0;
            PlusTimer = 0;
        }
        if(EnemySpownManager.Level3 && !PauseMenuScr.AllGamePauseEnabled)
        { 
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
                        SpownEnemy(1, EnemyPos);

                        
                        float Randomy2 = Random.Range(2f,-5f);
                        Vector3 EnemyPos2 = new Vector3(11f,Randomy2,0f);
                        SpownEnemy(1, EnemyPos2);

                        
                        float Randomx = Random.Range(-6f,6f);
                        Vector3 EnemyPos3 = new Vector3(Randomx,-6.5f,0f);
                        SpownEnemy(2, EnemyPos3);                    
                        
                        CurrentTimer = 0;
                        CurrentSpowns += 3;

                        if(CurrentSpowns >= SpownsToUpgrade)
                        PlusTimer = 0.01f; 
                    } 
                }
            } 
            
            if(CurrentSpowns >= TotalSpowns && EnemiesCount.Length == 0 && BossFight == false)
            {
                Checkpoints.LastCheckpoint = 3.5f;
                PlayerScr.SouvlakiCheckpoint = PlayerScr.SouvlakiMaxCount;
                BossFight = true;
                Warning.SetActive(true);
                StartCoroutine(SummonBoss());
                PlayerScr.CanMove = false;
                StartCoroutine(PlayerCanMove());
                FindObjectOfType<MusicSystem>().Stop("LevelMusic");
                FindObjectOfType<MusicSystem>().Play("BossMusic");
                return;                       
            }
            if(CurrentSpowns >= TotalSpowns && EnemiesCount.Length == 0 && BossFight == true)
            {
                FindObjectOfType<MusicSystem>().Stop("BossMusic");
                FindObjectOfType<MusicSystem>().Play("IntroMusic");
                EnemySpownManager.Level3 = false;
                DialogManagerScr.DialogIndex++;
            }      
        }      
    }
    IEnumerator PlayerCanMove()
    {
        yield return new WaitForSeconds(10.5f);
        PlayerScr.CanMove = true; 
    }

    void SpownEnemy(float WhichEnemy, Vector3 EnemyPos)
    {
        if(WhichEnemy == 1)
            Instantiate(PoliceMelee,EnemyPos,Quaternion.identity);
        else 
            Instantiate(PoliceRange,EnemyPos,Quaternion.identity);
    }
    IEnumerator SummonBoss()
    {
        yield return new WaitForSeconds(3f);
        Warning.SetActive(false);
        Vector3 BossPos = new Vector3(0,0,0);
        PoliceBossEntranceObj = Instantiate(PoliceBossEntrance, BossPos, Quaternion.identity);  
        PlayerPos.transform.position = new Vector3(-0.88f,-0.05f,0); 
        yield return new WaitForSeconds(6.5f);
        BossPos = new Vector3(-0.9f,-2f,0);
        Instantiate(PoliceBoss, BossPos, Quaternion.identity);   
        Destroy(PoliceBossEntranceObj,2);
    }
}
