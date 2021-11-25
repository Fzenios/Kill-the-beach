using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    public float LastCheckpoint;
    //public GameObject Player, PlayerEntrance, DialogueManager, CanvasStartMenu, CanvasUI, InGameUI, CameraStart, CameraMain; 
    public GameObject Player;
    public PlayerScr PlayerScr;
    public int SouvlakiMaxCount, SouvlakiCheckpoint; 
    public GameObject[] EnemiesCount,SouvlakiOnFloor, BulletsOnFloor, TracksCount;
    public GameObject Arrested, GameOverOptions, BlackScreen, DialogWindow, GameOverHp, GameOverArrested;
    public MosquitoManager MosquitoManager;
    public CitizensManager CitizensManager;
    public PoliceManager PoliceManager;
    public ArmyManager ArmyManager;
    public USAManager USAManager;
    public GameObject UpgradeSystem;
    public DialogManagerScr DialogManagerScr;
    public static bool ItsGameOver = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(PlayerScr!=null)
        {
            SouvlakiMaxCount = PlayerScr.SouvlakiMaxCount;
            SouvlakiCheckpoint = PlayerScr.SouvlakiCheckpoint;
        }
    }

    public void ResetNewGame()
    {
        PauseMenuScr.AllGamePauseEnabled = false;
        UpgradeSystemScr.UpgradeMenuEnabled = false;
        PauseMenuScr.PauseMenuEnabled = false;
        EnemySpownManager.Level1 = false;
        EnemySpownManager.Level2 = false;
        EnemySpownManager.Level3 = false;
        EnemySpownManager.Level4 = false;
        EnemySpownManager.Level5 = false;
        ResetLevel();
        SceneManager.LoadScene(0);
        FindObjectOfType<MusicSystem>().Play("IntroMusic");
        /*CameraMain.SetActive(false);
        CameraStart.SetActive(true);
        DialogueManager.SetActive(false);
        PlayerEntrance.SetActive(true);*/
    }
    public void GameOver(string Reason)
    {
        FindObjectOfType<MusicSystem>().Death("PlayerDeath");

        if(Reason == "Arrested")
        {
            ItsGameOver = true;
            Player.SetActive(false);
            GameOverArrested.SetActive(true);
            DialogWindow.SetActive(false); 
            StartCoroutine(GameOverOptionsFunc());
                       
        }
        if(Reason == "DropHp")
        {
            ItsGameOver = true;
            Player.SetActive(false);
            GameOverHp.SetActive(true);
            DialogWindow.SetActive(false); 
            StartCoroutine(GameOverOptionsFunc());
            Time.timeScale = 0;
        }
    }
    IEnumerator GameOverOptionsFunc()
    {
        yield return new WaitForSecondsRealtime(5);
        GameOverOptions.SetActive(true);
    }
    public void GoLastCheckPoint()
    {
        if(LastCheckpoint == 1)
        {
            ResetLevel();
            DialogManagerScr.DialogIndex = 0;
            DialogWindow.SetActive(true);  
            DialogManagerScr.ReadNextLine();
            FindObjectOfType<MusicSystem>().Play("IntroMusic");

        }
        if(LastCheckpoint == 1.5f)
        {
            ResetLevel();   
            EnemySpownManager.Level1 = true;     
            MosquitoManager.BossFight = false; 
            MosquitoManager.CurrentSpowns = MosquitoManager.TotalSpowns;
            FindObjectOfType<MusicSystem>().Play("BossMusic");
        }
        if(LastCheckpoint == 2)
        {
            ResetLevel();
            DialogManagerScr.DialogIndex = 10;
            DialogWindow.SetActive(true);  
            FindObjectOfType<MusicSystem>().Play("IntroMusic"); 
        }
        if(LastCheckpoint == 2.5)
        {
            ResetLevel();  
            EnemySpownManager.Level2 = true;
            CitizensManager.BossFight = false; 
            CitizensManager.CurrentSpowns = CitizensManager.TotalSpowns;
            FindObjectOfType<MusicSystem>().Play("BossMusic");
        }
        if(LastCheckpoint == 3)
        {
            ResetLevel();
            DialogManagerScr.DialogIndex = 18;
            DialogWindow.SetActive(true);  
            FindObjectOfType<MusicSystem>().Play("IntroMusic");
        }
        if(LastCheckpoint == 3.5)
        {
            ResetLevel();  
            EnemySpownManager.Level3 = true;
            PoliceManager.BossFight = false;
            EnemiesCount = GameObject.FindGameObjectsWithTag("Hound");
            for(int i=0; i<EnemiesCount.Length; i++)
            {
                Destroy(EnemiesCount[i]);
            }
            PoliceManager.CurrentSpowns = PoliceManager.TotalSpowns;
            EnemyPoliceBossScr.HountCount = 0;
            FindObjectOfType<MusicSystem>().Play("BossMusic");
        }
        if(LastCheckpoint == 4)
        {
            ResetLevel();
            DialogManagerScr.DialogIndex = 30;
            DialogWindow.SetActive(true);  
            FindObjectOfType<MusicSystem>().Play("IntroMusic");
        }
        if(LastCheckpoint == 4.5)
        {
            ResetLevel();  
            EnemySpownManager.Level4 = true;
            ArmyManager.BossFight = false;
            TracksCount = GameObject.FindGameObjectsWithTag("Tracks");
            for(int i=0; i<TracksCount.Length; i++)
            {
                Destroy(TracksCount[i]);
            }
            ArmyManager.CurrentSpowns = ArmyManager.TotalSpowns;
            FindObjectOfType<MusicSystem>().Play("BossMusic");            
        }
        if(LastCheckpoint == 5)
        {
            ResetLevel();
            DialogManagerScr.DialogIndex = 43;
            DialogWindow.SetActive(true);
            GameObject Ally = GameObject.FindGameObjectWithTag("Ally");
            if(Ally != null)
                Destroy(Ally);
            FindObjectOfType<MusicSystem>().Play("IntroMusic");
        }
    }
    public void ResetLevel()
    {
        SouvlakiMaxCount = SouvlakiCheckpoint;
        GameOverHp.SetActive(false);
        GameOverArrested.SetActive(false);
        GameOverOptions.SetActive(false);
        
        EnemiesCount = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i=0; i<EnemiesCount.Length; i++)
        {
            Destroy(EnemiesCount[i]);
        }

        SouvlakiOnFloor = GameObject.FindGameObjectsWithTag("Souvlaki"); 
        for(int j=0; j<SouvlakiOnFloor.Length; j++)
        {
            Destroy(SouvlakiOnFloor[j]);
        }  
        
        BulletsOnFloor = GameObject.FindGameObjectsWithTag("Bullet");       
        for(int i=0; i<BulletsOnFloor.Length; i++)
        {
            Destroy(BulletsOnFloor[i]);
        }  

        UpgradeSystem.SetActive(false);
        UpgradeSystem.SetActive(true);
        
        Player.SetActive(true);
        PlayerScr.SouvlakiCount = SouvlakiMaxCount;
        PlayerScr.MaxHp = 100;
        PlayerScr.CurrentHp = 100;
        PlayerScr.Slider.value = 100;
        PlayerScr.Slider.maxValue = 100;
        UpgradeSystemScr.DamageUp = false;
        UpgradeSystemScr.AmmoUp = false;
        UpgradeSystemScr.Lifesteal = false;
        UpgradeSystemScr.Critical = false;
        UpgradeSystemScr.FasterReload = false;
        UpgradeSystemScr.AttackSpdBool = false;
        UpgradeSystemScr.DefenceUpBool = false;
        UpgradeSystemScr.SpeedUpBool = false;
        UpgradeSystemScr.SouvlakiHealBool = false;

        ItsGameOver = false; 
        Time.timeScale = 1; 
    }
}
