using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizensManager : MonoBehaviour
{
    public GameObject CitizenMelee1,CitizenMelee2 , CitizenThrown1, CitizenThrown2, CitizenBoss;
    public GameObject PlayerPos;
    public PlayerScr PlayerScr;
    public int CurrentSpowns = 0;
    public int SpownsToUpgrade;
    public int TotalSpowns = 40; 
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
            EnemySpownManager.Level2 = false;
            CurrentSpowns = 0;
            PlusTimer = 0;
        }
        if(EnemySpownManager.Level2 && !PauseMenuScr.AllGamePauseEnabled)
        { 
            EnemiesCount = GameObject.FindGameObjectsWithTag("Enemy");   
            
            if(CurrentSpowns < TotalSpowns)
            {
                CurrentTimer += Time.deltaTime + PlusTimer;
                
                if(CurrentTimer > TotalTimer)
                {
                    int RandomEnemy = Random.Range(1,3);
                    float Randomx = Random.Range(2f,-5f);
                    Vector3 EnemyPos = new Vector3(-11f,Randomx,0f);
                    SpownEnemy(RandomEnemy, EnemyPos);

                    RandomEnemy = Random.Range(1,5);
                    float Randomx2 = Random.Range(2f,-5f);
                    Vector3 EnemyPos2 = new Vector3(11f,Randomx2,0f);
                    SpownEnemy(RandomEnemy, EnemyPos2);
                    
                    CurrentTimer = 0;
                    CurrentSpowns += 2;

                    if(CurrentSpowns >= SpownsToUpgrade)
                    PlusTimer = 0.02f; 
                } 
            } 
            
            if(CurrentSpowns >= TotalSpowns && EnemiesCount.Length == 0 && BossFight == false)
            {
                Checkpoints.LastCheckpoint = 2.5f;
                PlayerScr.SouvlakiCheckpoint = PlayerScr.SouvlakiMaxCount;
                BossFight = true;
                StartCoroutine(SummonBoss());
                Warning.SetActive(true);
                PlayerPos.transform.position = new Vector3(-0,-1.3f,0); 
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
                EnemySpownManager.Level2 = false;
                DialogManagerScr.DialogIndex++;
            }      
        }      
    }

    IEnumerator PlayerCanMove()
    {
        yield return new WaitForSeconds(7.5f);
        PlayerScr.CanMove = true; 
    }

    IEnumerator SummonBoss()
    {
        yield return new WaitForSeconds(3f);
        Warning.SetActive(false);
        Vector3 BossPos = new Vector3(-4,-1,0);
        Instantiate(CitizenBoss, BossPos, Quaternion.identity);
    }
    
    void SpownEnemy(float WhichEnemy, Vector3 EnemyPos)
    {
        switch (WhichEnemy)
        {
            case 1: Instantiate(CitizenMelee1,EnemyPos,Quaternion.identity); break;
            case 2: Instantiate(CitizenMelee2,EnemyPos,Quaternion.identity); break;
            case 3: Instantiate(CitizenThrown1,EnemyPos,Quaternion.identity); break;
            case 4: Instantiate(CitizenThrown2,EnemyPos,Quaternion.identity); break;
        }
    }
}
