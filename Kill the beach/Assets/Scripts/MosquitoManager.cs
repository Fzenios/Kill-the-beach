using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoManager : MonoBehaviour
{
    public GameObject Mosquito, MosquitoBoss;
    public GameObject PlayerPos;
    public PlayerScr PlayerScr;
    public int CurrentSpowns;
    public int SpownsToUpgrade;
    public int TotalSpowns = 40; 
    float PlusTimer = 0f;
    public float TotalTimer = 4f;
    float CurrentTimer = 0f; 
    public GameObject[] EnemiesCount;
    public bool BossFight = false;
    public DialogManagerScr DialogManagerScr;
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
            EnemySpownManager.Level1 = false;
            CurrentSpowns = 0;
            PlusTimer = 0;
        }
        if(EnemySpownManager.Level1 && !PauseMenuScr.AllGamePauseEnabled)
        { 
            EnemiesCount = GameObject.FindGameObjectsWithTag("Enemy");   
            
            if(CurrentSpowns < TotalSpowns)
            {
                CurrentTimer += Time.deltaTime + PlusTimer;
                if(CurrentTimer > TotalTimer)
                {
                    
                    float Randomx = Random.Range(2f,-5f);
                    Vector3 EnemyPos = new Vector3(-11f,Randomx,0f);
                    Instantiate(Mosquito,EnemyPos,Quaternion.identity);
                    float Randomx2 = Random.Range(2f,-5f);
                    Vector3 EnemyPos2 = new Vector3(11f,Randomx2,0f);
                    Instantiate(Mosquito,EnemyPos2,Quaternion.identity);
                    
                    CurrentTimer = 0;
                    CurrentSpowns += 2;

                    if(CurrentSpowns >= SpownsToUpgrade)
                    PlusTimer = 0.02f; 
                } 
            } 
            if(CurrentSpowns >= TotalSpowns && EnemiesCount.Length == 0 && BossFight == false)
            {
                Checkpoints.LastCheckpoint = 1.5f;
                PlayerScr.SouvlakiCheckpoint = PlayerScr.SouvlakiMaxCount;
                BossFight = true;
                StartCoroutine(SummonBoss());
                Warning.SetActive(true);
                PlayerPos.transform.position = new Vector3(4,-1f,0); 
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
                EnemySpownManager.Level1 = false;
                DialogManagerScr.DialogIndex++;
            }      
        }      
    }

    IEnumerator PlayerCanMove()
    {
        yield return new WaitForSeconds(8f);
        PlayerScr.CanMove = true; 
    }
    IEnumerator SummonBoss()
    {
        yield return new WaitForSeconds(3f);
        Warning.SetActive(false);
        Vector3 MosquitoBossPos = new Vector3(0,0,0);
        Instantiate(MosquitoBoss, MosquitoBossPos, Quaternion.identity); 
    }
}
