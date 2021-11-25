using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManagerScr : MonoBehaviour
{
    public TextMeshProUGUI TextDisplay;
    public int DialogIndex = 0; 
    [TextArea(3,5)] public string[] Sentences;
    public float TypingSpeed;
    public GameObject ContinueButton;
    public GameObject DialogWindow; 
    public TextMeshProUGUI Left, Right;
    public bool LeftBtn = false, RightBtn = false;
    public GameObject Ally,AllyEntrance;
    GameObject AllyObj, PlayerDartCredits,PlayerSleepObj, AllyObj2;
    public Transform PlayerPos;
    public GameObject Player, PlayerIdle, InGameUI, Instructions,UpgradeSystem;
    public GameObject PlayerAv, NarratorAv, MosquitoAv, PoliceAv, SoliderAv, UsaAv, AllyAv;
    public PlayerScr PlayerScr;
    public GameObject ColaCan, TankBullet, SleepingDart, Siren;
    public GameObject BlackScreen, Arrested, GameOverOptions;
    public GameObject CameraObj, CreditsObj;
    public GameObject PublicScripts;
    bool RightBtnCredit, LeftBtnCredit;
    public GameObject PlayerSleep;
    public Checkpoints Checkpoints;

    private void OnEnable() 
    {

    }
    void Start() 
    {
        Left.text = "";
        Right.text = "";
        StartCoroutine(Type());
        DialogIndex = 0;
    }

    void Update() 
    {   
        if(Checkpoints.ItsGameOver)
        {
            LeftBtn = false;
            RightBtn = false;
            TextDisplay.text = "";
            Left.text = "";
            Right.text = "";
            Left.gameObject.SetActive(false);
            Right.gameObject.SetActive(false);
            DialogWindow.SetActive(false);
        }
        if(TextDisplay.text == Sentences[DialogIndex])
        {
            //ContinueButton.SetActive(true);
        }  
        if(DialogIndex==0)
        {
            NarratorAv.SetActive(true);
        }
        if(Left.text == "" && DialogIndex==7)
        {
            Left.gameObject.SetActive(true);
            Right.gameObject.SetActive(true);
            Left.text = "Well...";
            Right.text = "Kill it"; 
            DialogIndex++;
            Checkpoints.LastCheckpoint = 1;
        }
        if(Left.text == "" && DialogIndex==15)
        {
            Left.gameObject.SetActive(true);
            Right.gameObject.SetActive(true);
            Left.text = "It's ok, they are right";
            Right.text = "Big Nope"; 
            DialogIndex++;
            Checkpoints.LastCheckpoint = 2;
        }
        if(Left.text == "" && DialogIndex==27)
        {
            Left.gameObject.SetActive(true);
            Right.gameObject.SetActive(true);
            Left.text = "There is no hope";
            Right.text = "This. Is. MY BEACH!"; 
            DialogIndex++;
            Checkpoints.LastCheckpoint = 3;
        }
        if(Left.text == "" && DialogIndex==40)
        {
            Left.gameObject.SetActive(true);
            Right.gameObject.SetActive(true);
            Left.text = "Take the hit like a Champ";
            Right.text = "Ain't nobody got time for that"; 
            DialogIndex++;
            Checkpoints.LastCheckpoint = 4;
        }
        if(Left.text == "" && DialogIndex==65)
        {
            Left.gameObject.SetActive(true);
            Right.gameObject.SetActive(true);
            Left.text = "An extra hand is appreciated";
            Right.text = "I work on my own"; 
            DialogIndex++;
            Checkpoints.LastCheckpoint = 5;
        }
        if(Left.text == "" && DialogIndex==80)
        {
            Left.gameObject.SetActive(true);
            Right.gameObject.SetActive(true);
            Left.text = "Bring me the final Boss!";
            Right.text = "Ok I'm tired, let's rest"; 
        }
        if(Left.text == "" && DialogIndex==82)
        {
            Left.gameObject.SetActive(true);
            Right.gameObject.SetActive(true);
            Left.text = "Really, bring me the final Boss";
            Right.text = "Ok I'm tired, let's rest"; 
        }
        if(Left.text == "" && DialogIndex==84)
        {
            Left.gameObject.SetActive(true);
            Right.gameObject.SetActive(true);
            Left.text = "Please please please gif Boss";
            Right.text = "Ok I'm tired, let's rest"; 
        }
        if(Left.text == "" && DialogIndex==86)
        {
            Left.gameObject.SetActive(true);
            Right.gameObject.SetActive(true);
            Left.text = "Pleeeeeeeeeeeease";
            Right.text = "Ok I'm tired, let's rest"; 
        }

        if(LeftBtn)
        {
            if(DialogIndex==8)
            {
                FindObjectOfType<MusicSystem>().SoundEffects("Mosquito");
                LeftBtn = false;
            }
            if(DialogIndex==16)
            {
                int RandomPosX;
                int RandomPosY = Random.Range(6,-6);
                int RandomX = Random.Range(1,3);
                if(RandomX==1)
                    RandomPosX = 11;
                else 
                    RandomPosX = -11;
        
                Vector2 ColaPos = new Vector2(RandomPosX,RandomPosY);
                Instantiate(ColaCan,ColaPos,Quaternion.identity);
                
                LeftBtn = false;
            }
            if(DialogIndex==28)
            {
                LeftBtn = false;
                PlayerAv.SetActive(false);
                Checkpoints.GameOver("Arrested");
            }
            if(DialogIndex==41)
            {
                Vector2 TankBulletPos = new Vector2(10.5f,-4);
                Instantiate(TankBullet,TankBulletPos,Quaternion.identity);
                RightBtn = false;
                TextDisplay.text = "";
                Left.text = "";
                Right.text = "";
                Left.gameObject.SetActive(false);
                Right.gameObject.SetActive(false);

                StartCoroutine(Type());
                LeftBtn = false;

                PlayerAv.SetActive(false); SoliderAv.SetActive(true);

            }
            if(DialogIndex==66)
            {
                Invoke("InstaAlly",3);
                AllyObj = Instantiate(AllyEntrance,PlayerPos.position, Quaternion.identity);
                LeftBtn = false;
                TextDisplay.text = "";
                Left.text = "";
                Right.text = "";
                
                Left.gameObject.SetActive(false);
                Right.gameObject.SetActive(false);
                StartCoroutine(Type());
                NarratorAv.SetActive(false); PlayerAv.SetActive(true);
            }
            if(DialogIndex==80||DialogIndex==82||DialogIndex==84)
            {
                LeftBtn = false;
                TextDisplay.text = "";
                Left.text = "";
                Right.text = "";

                Left.gameObject.SetActive(false);
                Right.gameObject.SetActive(false);
                DialogIndex++;
                StartCoroutine(Type());
            }
            if(DialogIndex==86)
            {
                Vector2 SleepingDartPos = new Vector2(10.5f,-4);
                Instantiate(SleepingDart,SleepingDartPos,Quaternion.identity);

                LeftBtn = false;
                TextDisplay.text = "";
                Left.text = "";
                Right.text = "";

                Left.gameObject.SetActive(false);
                Right.gameObject.SetActive(false);
                DialogIndex++;
                LeftBtnCredit = true;
                StartCoroutine(Type());
            }

        }
        if(RightBtn)
        {   
            if(DialogIndex==8 ||DialogIndex==16 || DialogIndex==28 || DialogIndex==41 || DialogIndex==66)
            { 
                RightBtn = false;
                TextDisplay.text = "";
                Left.text = "";
                Right.text = "";
                Left.gameObject.SetActive(false);
                Right.gameObject.SetActive(false);

                StartCoroutine(Type());
            }
            if(DialogIndex==80||DialogIndex==82||DialogIndex==84||DialogIndex==86)
            {
                RightBtn = false;
                TextDisplay.text = "";
                Left.text = "";
                Right.text = "";
                Left.gameObject.SetActive(false);
                Right.gameObject.SetActive(false);
                DialogIndex = 87;
                RightBtnCredit = true;
                StartCoroutine(Type());
            }
            if(DialogIndex==8 ||DialogIndex==16 || DialogIndex==28 || DialogIndex==66)
            {
                NarratorAv.SetActive(false); PlayerAv.SetActive(true);
            }
            if(DialogIndex==41)
            {
                PlayerAv.SetActive(false); SoliderAv.SetActive(true);
            }

        }
        if(DialogIndex==10)
        {
            NarratorAv.SetActive(true);
            TextDisplay.text = "";
            DialogIndex++;
            DialogWindow.SetActive(true);
            StartCoroutine(Type());
            PlayerScr.CanMove = false;
        }
        if(DialogIndex==18)
        {
            NarratorAv.SetActive(true);
            TextDisplay.text = "";
            DialogIndex++;
            DialogWindow.SetActive(true);
            StartCoroutine(Type());
            PlayerScr.CanMove = false;
        }
        if(DialogIndex==30)
        {
            PlayerAv.SetActive(true);
            TextDisplay.text = "";
            DialogIndex++;
            DialogWindow.SetActive(true);
            StartCoroutine(Type());
            PlayerScr.CanMove = false;
        }
        if(DialogIndex==43)
        {
            PlayerAv.SetActive(true);
            TextDisplay.text = "";
            DialogIndex++;
            DialogWindow.SetActive(true);
            StartCoroutine(Type());
            PlayerScr.CanMove = false;
        }  
        if(DialogIndex==68)
        {
            PlayerAv.SetActive(true);
            TextDisplay.text = "";
            DialogIndex++;
            DialogWindow.SetActive(true);
            StartCoroutine(Type());
            PlayerScr.CanMove = false;
        } 
    }

    IEnumerator Type()
    {
        foreach (char Letter in Sentences[DialogIndex].ToCharArray())
        {
            TextDisplay.text += Letter;
            yield return new WaitForSeconds(TypingSpeed);
        }
    
        if(DialogIndex!=6 && DialogIndex!=14 && DialogIndex!=26 && DialogIndex!=39 && DialogIndex!=64 && DialogIndex!=79 
        && DialogIndex!=81 && DialogIndex!=83 && DialogIndex!=85)

            ContinueButton.SetActive(true);

        DialogIndex++;
    }

    IEnumerator WaitSeconds()
    {
        TextDisplay.text = "";
        yield return new WaitForSeconds(2);
        StartCoroutine(Type());
    }

    public void NextSentence()
    {
        ContinueButton.SetActive(false);
        
        switch (DialogIndex)
        {
            case 1:  ReadNextLine(); PlayerScr.CanMove = false ; break;
            case 2:  ReadNextLine(); break;
            case 3:  ReadNextLine(); FindObjectOfType<MusicSystem>().SoundEffects("Mosquito"); break;
            case 4:  ReadNextLine(); FindObjectOfType<MusicSystem>().SoundEffects("Mosquito"); break;
            case 5:  ReadNextLine(); NarratorAv.SetActive(false); MosquitoAv.SetActive(true); break;
            case 6:  ReadNextLine(); MosquitoAv.SetActive(false); NarratorAv.SetActive(true); break;
            case 12: ReadNextLine(); break;
            case 13: ReadNextLine(); break;
            case 14: ReadNextLine(); break;
            case 18: ReadNextLine(); break;
            case 19: ReadNextLine(); break;
            case 20: ReadNextLine(); FindObjectOfType<MusicSystem>().SoundEffects("PoliceSiren"); 
            GameObject SirenObj = Instantiate(Siren, Siren.transform.position, Siren.transform.rotation); Destroy(SirenObj,26);  break;
            case 21: ReadNextLine(); NarratorAv.SetActive(false); PoliceAv.SetActive(true); break;
            case 22: ReadNextLine(); PoliceAv.SetActive(false); PlayerAv.SetActive(true); break;
            case 23: ReadNextLine(); PlayerAv.SetActive(false); PoliceAv.SetActive(true);  break;
            case 24: ReadNextLine(); break;
            case 25: ReadNextLine(); PoliceAv.SetActive(false); PlayerAv.SetActive(true); break;
            case 26: ReadNextLine(); break;
            case 31: ReadNextLine(); break;
            case 32: ReadNextLine(); break;
            case 33: ReadNextLine(); break;
            case 34: ReadNextLine(); PlayerAv.SetActive(false); SoliderAv.SetActive(true); break;
            case 35: ReadNextLine(); SoliderAv.SetActive(false); PlayerAv.SetActive(true); break;
            case 36: ReadNextLine(); PlayerAv.SetActive(false); SoliderAv.SetActive(true); break;
            case 37: ReadNextLine(); SoliderAv.SetActive(false); PlayerAv.SetActive(true); break;
            case 38: ReadNextLine(); PlayerAv.SetActive(false); SoliderAv.SetActive(true); break;
            case 39: ReadNextLine(); SoliderAv.SetActive(false); PlayerAv.SetActive(true); break;
            case 44: ReadNextLine(); break;
            case 45: ReadNextLine(); break;
            case 46: ReadNextLine(); break;
            case 47: ReadNextLine(); break;
            case 48: ReadNextLine(); break;
            case 49: ReadNextLine(); break;
            case 51: ReadNextLine(); PlayerAv.SetActive(true); break;
            case 52: ReadNextLine(); PlayerAv.SetActive(false); UsaAv.SetActive(true); break;
            case 53: ReadNextLine(); UsaAv.SetActive(false); PlayerAv.SetActive(true); break;
            case 54: ReadNextLine(); PlayerAv.SetActive(false); UsaAv.SetActive(true); break;
            case 55: ReadNextLine(); UsaAv.SetActive(false); PlayerAv.SetActive(true); break;
            case 56: ReadNextLine(); PlayerAv.SetActive(false); UsaAv.SetActive(true); break;
            case 57: ReadNextLine(); UsaAv.SetActive(false); PlayerAv.SetActive(true); break;
            case 58: ReadNextLine(); PlayerAv.SetActive(false); UsaAv.SetActive(true);break;
            case 59: ReadNextLine(); break;
            case 60: ReadNextLine(); UsaAv.SetActive(false); PlayerAv.SetActive(true); break;
            case 61: ReadNextLine(); PlayerAv.SetActive(false); AllyAv.SetActive(true); break;
            case 62: ReadNextLine(); break;
            case 63: ReadNextLine(); break;
            case 64: ReadNextLine(); AllyAv.SetActive(false); NarratorAv.SetActive(true); break;
            case 70: ReadNextLine(); break;
            case 71: ReadNextLine(); break;
            case 72: ReadNextLine(); PlayerAv.SetActive(false); NarratorAv.SetActive(true); break;
            case 73: ReadNextLine(); NarratorAv.SetActive(false); PlayerAv.SetActive(true); break;
            case 74: ReadNextLine(); PlayerAv.SetActive(false); NarratorAv.SetActive(true); break;
            case 75: ReadNextLine(); NarratorAv.SetActive(false); PlayerAv.SetActive(true); break;
            case 76: ReadNextLine(); PlayerAv.SetActive(false); NarratorAv.SetActive(true); break;
            case 77: ReadNextLine(); NarratorAv.SetActive(false); PlayerAv.SetActive(true); break;
            case 78: ReadNextLine(); PlayerAv.SetActive(false); NarratorAv.SetActive(true); break;
            case 79: ReadNextLine(); break;
        }
        
        if(DialogIndex==9)
        {
            PlayerAv.SetActive(false);
            DialogWindow.SetActive(false);
            EnemySpownManager.Level1 = true;

            PlayerIdle.SetActive(false);
            Player.SetActive(true);
            InGameUI.SetActive(true);
            Instructions.SetActive(true);
            PlayerScr.CanMove = true;
            Time.timeScale = 0;
            FindObjectOfType<MusicSystem>().Stop("IntroMusic");
            FindObjectOfType<MusicSystem>().Play("LevelMusic");

            return;
        }
        if(DialogIndex==17)
        {
            PlayerAv.SetActive(false);
            DialogWindow.SetActive(false);
            EnemySpownManager.Level2 = true;
            PlayerScr.CanMove = true;
            FindObjectOfType<MusicSystem>().Stop("IntroMusic");
            FindObjectOfType<MusicSystem>().Play("LevelMusic");
            return;
        }

        if(DialogIndex==29)
        {
            PlayerAv.SetActive(false);
            DialogWindow.SetActive(false);
            EnemySpownManager.Level3 = true;
            PlayerScr.CanMove = true;
            FindObjectOfType<MusicSystem>().Stop("IntroMusic");
            FindObjectOfType<MusicSystem>().Play("LevelMusic");
            return;
        }         

        if(DialogIndex==42)
        {
            SoliderAv.SetActive(false);
            DialogWindow.SetActive(false);
            EnemySpownManager.Level4 = true;
            PlayerScr.CanMove = true;
            FindObjectOfType<MusicSystem>().Stop("IntroMusic");
            FindObjectOfType<MusicSystem>().Play("LevelMusic");
            return;
        } 
        if(DialogIndex==50)
        {
            PlayerAv.SetActive(false);
            GameObject SirenObj = Instantiate(Siren, Siren.transform.position, Siren.transform.rotation); Destroy(SirenObj,10);  
            StartCoroutine(WaitSeconds());
            return;
        }  
        if(DialogIndex==67)
        {
            PlayerAv.SetActive(false);
            DialogWindow.SetActive(false);
            EnemySpownManager.Level5 = true;
            PlayerScr.CanMove = true;
            FindObjectOfType<MusicSystem>().Stop("IntroMusic");
            FindObjectOfType<MusicSystem>().Play("LevelMusic");
            return;
        } 
        if(DialogIndex==88 && RightBtnCredit == true)
        {
            NarratorAv.SetActive(false);
            RightBtnCredit = false;
            DialogWindow.SetActive(false);
            Animator CameraAnim = CameraObj.GetComponent<Animator>();
            CameraAnim.SetBool("Closing", true);
            StartCoroutine(CreditsRight());
        } 
        if(DialogIndex==88 && LeftBtnCredit == true)
        {
            NarratorAv.SetActive(false);
            LeftBtnCredit = false;
            DialogWindow.SetActive(false);
            Animator CameraAnim = CameraObj.GetComponent<Animator>();
            CameraAnim.SetBool("Closing", true);
            StartCoroutine(CreditsLeft());

        }
      
        /*if(DialogIndex<Sentences.Length-1)
        {
            DialogIndex++;
            TextDisplay.text = "";
            StartCoroutine(Type());
        }*/
    }
        IEnumerator CreditsLeft()
        {
            FindObjectOfType<MusicSystem>().Stop("LevelMusic");
            FindObjectOfType<MusicSystem>().SoundEffects("CreditsMusic");
            yield return new WaitForSeconds (4.5f);
            CreditsObj.SetActive(true);
            InGameUI.SetActive(false);
            UpgradeSystem.SetActive(false);
            if(AllyObj2!=null)
                Destroy(AllyObj2);
            yield return new WaitForSeconds (122);
            PlayerDartCredits = GameObject.FindGameObjectWithTag("Player");
            Destroy(PlayerDartCredits);
            CreditsObj.SetActive(false);
            PublicScripts.GetComponent<Checkpoints>().ResetNewGame();                                
        }  
        IEnumerator CreditsRight()
        {
            FindObjectOfType<MusicSystem>().Stop("LevelMusic");
            FindObjectOfType<MusicSystem>().SoundEffects("CreditsMusic");
            yield return new WaitForSeconds (4.5f);
            CreditsObj.SetActive(true);
            InGameUI.SetActive(false);
            UpgradeSystem.SetActive(false);
            Vector2 PlayerSleepPos = new Vector2 (6.339f,0.695f);
            Player.SetActive(false);
            if(AllyObj2!=null)
                Destroy(AllyObj2);
            PlayerSleepObj = Instantiate(PlayerSleep,PlayerSleepPos, PlayerSleep.transform.rotation);            
            yield return new WaitForSeconds (122);
            Destroy(PlayerSleepObj);
            CreditsObj.SetActive(false);
            PublicScripts.GetComponent<Checkpoints>().ResetNewGame();                                
        }  

    public void ReadNextLine()
    {
            TextDisplay.text = "";
            StartCoroutine(Type());
            return;        
    }

    public void LeftButton()
    {
        LeftBtn = true;
    }
    public void RightButton()
    {
        RightBtn = true;
    }

    void InstaAlly()
    {
        AllyObj2 = Instantiate(Ally,AllyObj.transform.position, Quaternion.identity);
        Destroy(AllyObj);
    }
    public void CloseCredits()
    {
        PlayerDartCredits = GameObject.FindGameObjectWithTag("Player");
        Destroy(PlayerDartCredits);
        Destroy(PlayerSleepObj);
        FindObjectOfType<MusicSystem>().Stop("CreditsMusic");
        CreditsObj.SetActive(false);
        StopAllCoroutines();
        PublicScripts.GetComponent<Checkpoints>().ResetNewGame();  
    }
}
