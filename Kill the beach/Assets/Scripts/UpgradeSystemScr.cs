using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystemScr : MonoBehaviour
{
    public List<GameObject> Upgrades = new List<GameObject>();
    public GameObject MoreDamage, Bazooka, MoreAmmo, Uzi, Burst, HealCard, SouvlakiHealUp, 
    Health, Shotgun, LifestealCard, CriticalCard, Pistol, FastReload, AttackSpd, DefenceUp, SpeedUp; 
    public Transform Upgrade1, Upgrade2;
    GameObject PlayerScrs;
    public static bool DamageUp = false; 
    public static bool AmmoUp = false;
    public static bool Lifesteal = false;
    public static bool Critical = false;
    public static bool FasterReload = false;
    public static bool UpgradeMenuEnabled = false;
    public static bool AttackSpdBool = false;
    public static bool DefenceUpBool = false;
    public static bool SpeedUpBool = false;
    public static bool SouvlakiHealBool = false;
    public GameObject UpgradeCanvas, Warning; 
    public Animator Animator;
    public GameObject NoUpdatesTxt;
    public Transform UpdateBtnPos;
    GameObject NoMoreUpdatesTxt;
    public Transform MainCanvas;
    public PlayerScr PlayerScr;
    Vector3 TextFloatPos;

    void OnEnable() 
    { 
        TextFloatPos = new Vector3(UpdateBtnPos.position.x, UpdateBtnPos.position.y + 150,UpdateBtnPos.position.z);

        //ShootScript = GameObject.Find("Player");
        //ShootScr ShootScr = ShootScript.GetComponent<ShootScr>();        
        //for (int i=0; i < Upgrades.Count; i++ )
       // Debug.Log(Upgrades[i]);
        Upgrades.Clear();
        Upgrades.Add(MoreDamage);
        Upgrades.Add(Bazooka);
        Upgrades.Add(MoreAmmo);
        Upgrades.Add(Uzi);
        Upgrades.Add(Burst);
        Upgrades.Add(HealCard);
        Upgrades.Add(Health);
        Upgrades.Add(Shotgun);
        Upgrades.Add(LifestealCard);
        Upgrades.Add(CriticalCard);
        Upgrades.Add(Pistol);
        Upgrades.Add(FastReload);
        Upgrades.Add(AttackSpd);
        Upgrades.Add(DefenceUp);
        Upgrades.Add(SpeedUp);
        Upgrades.Add(SouvlakiHealUp); 
       
       for(int i=0; i<Upgrades.Count; i++)
       {
           GameObject Obj = Upgrades[i];
           int RandomList = Random.Range(0, i);
           Upgrades[i] = Upgrades[RandomList];
           Upgrades[RandomList] = Obj;
       }
    }
    void Update()
    {
        if(NoMoreUpdatesTxt != null)
            NoMoreUpdatesTxt.transform.position += new Vector3 (0, Time.deltaTime * 70,0); 
        
        /*if(Input.GetKeyDown(KeyCode.G))
        {
            if(!UpgradeCanvas.activeSelf)
            {
                //Debug.Log(Upgrades.Count);
                if(Upgrades.Count > 0)
                {
                    FindObjectOfType<MusicSystem>().SoundEffects("PowerUp");    
                    UpgradeMenuEnabled = true;
                    PauseMenuScr.AllGamePauseEnabled = true;
                    Time.timeScale = 0;

                    UpgradeCanvas.SetActive(true);
                    GameObject Card1 = Instantiate(Upgrades[0], Upgrade1, Upgrade1);
                    Card1.transform.localScale = Upgrade1.localScale;
                    Card1.transform.position = Upgrade1.position;
                    Upgrades.Remove(Upgrades[0]);

                    GameObject Card2 = Instantiate(Upgrades[0], Upgrade2, Upgrade2);
                    Card2.transform.localScale = Upgrade2.localScale;
                    Card2.transform.position = Upgrade2.position;
                    Upgrades.Remove(Upgrades[0]);
                }
                else
                {
                    if(NoMoreUpdatesTxt != null)
                        Destroy(NoMoreUpdatesTxt);

                    NoMoreUpdatesTxt = Instantiate(NoUpdatesTxt, TextFloatPos, Quaternion.identity);
                    NoMoreUpdatesTxt.transform.SetParent(MainCanvas);
                    NoMoreUpdatesTxt.transform.localScale = new Vector3 (1,1,1);
                    Destroy(NoMoreUpdatesTxt,1);
                }
            }
            // OpenUpgrades();
        }  */           
    }
    public void OpenUpgrades()
    {
        if(!UpgradeCanvas.activeSelf && !Warning.activeSelf)
        {
            if(Upgrades.Count > 0)
            {
            
                if(PlayerScr.SouvlakiCount >= 3)
                {
                    FindObjectOfType<MusicSystem>().SoundEffects("PowerUp"); 
                    UpgradeMenuEnabled = true;
                    PauseMenuScr.AllGamePauseEnabled = true;
                    Time.timeScale = 0;

                    UpgradeCanvas.SetActive(true);
                    GameObject Card1 = Instantiate(Upgrades[0], Upgrade1, Upgrade1);
                    Card1.transform.localScale = Upgrade1.localScale;
                    Card1.transform.position = Upgrade1.position;
                    Upgrades.Remove(Upgrades[0]);

                    GameObject Card2 = Instantiate(Upgrades[0], Upgrade2, Upgrade2);
                    Card2.transform.localScale = Upgrade2.localScale;
                    Card2.transform.position = Upgrade2.position;
                    Upgrades.Remove(Upgrades[0]);

                    PlayerScr.SouvlakiCount -= 3;
                }
                else
                {
                    StartCoroutine(ChangeAnimation());
                }
            }
            else
            {
                if(NoMoreUpdatesTxt != null)
                        Destroy(NoMoreUpdatesTxt);

                NoMoreUpdatesTxt = Instantiate(NoUpdatesTxt, TextFloatPos, Quaternion.identity);
                NoMoreUpdatesTxt.transform.SetParent(MainCanvas);
                NoMoreUpdatesTxt.transform.localScale = new Vector3 (1,1,1);
                Destroy(NoMoreUpdatesTxt,1);
            }
        }
        
        /*else
        {
            Canvas.SetActive(false);
            Time.timeScale = 1;
            UpgradeMenuEnabled = false;
        }*/
    }
    IEnumerator ChangeAnimation()
    {
        Animator.SetBool("WrongSouvlaki", true);
        yield return new WaitForEndOfFrame();
        Animator.SetBool("WrongSouvlaki", false);
    }
    public void Damageup()
    {
        DamageUp = true;    
        CloseCanvas();
    }
    public void Ammoup()
    {
        AmmoUp = true;
        ShootScr.MaxAmmo += (ShootScr.MaxAmmo * 50)/100;
        CloseCanvas();
    }
    public void MaxHpUp()
    {
        PlayerScrs = GameObject.Find("Player");
        PlayerScr PlayerScr = PlayerScrs.GetComponent<PlayerScr>(); 

        PlayerScr.MaxHp += (PlayerScr.MaxHp * 25)/100;

        PlayerScr.Slider.maxValue = PlayerScr.MaxHp;
        CloseCanvas();
    }
    public void Lifestealup()
    {
        Lifesteal = true;
        CloseCanvas();
    }
    public void Heal()
    {
        PlayerScr.CurrentHp += PlayerScr.MaxHp/2;
        CloseCanvas();
    }
    public void Criticalup()
    {
        Critical = true;
        CloseCanvas();
    }
    public void FasterReloadup()
    {
        FasterReload = true;
        CloseCanvas();
    }
    public void AttackSpeed()
    {
        AttackSpdBool = true;
        CloseCanvas();
    }
    public void Defence()
    {
        DefenceUpBool = true;
        CloseCanvas();
    }
    public void Speed()
    {
        SpeedUpBool = true;
        CloseCanvas();
    }
    public void Souvlaki()
    {
        SouvlakiHealBool = true;
        CloseCanvas();
    }
    public void UnlockPistol()
    {
        ShootScr.Weapons.Insert(1,"Pistol");
        CloseCanvas();
    }
    public void UnlockShotgun()
    {
        ShootScr.Weapons.Insert(2,"Shotgun");
        CloseCanvas();
    }
    public void UnlockUzi()
    {
        ShootScr.Weapons.Insert(3,"Uzi");
        CloseCanvas();
    }
    public void UnlockBazooka()
    {
        ShootScr.Weapons.Insert(4,"Bazooka");
        CloseCanvas();
    }
    public void UnlockBurst()
    {
        ShootScr.Weapons.Insert(5,"Burst");
        CloseCanvas();
    }  

    public void CloseCanvas()
    {
        FindObjectOfType<MusicSystem>().SoundEffects("PowerUpSelect"); 
        GameObject UpgradeCanvas = GameObject.Find("UpgradeCanvas");
        GameObject[] UpgradeCards = GameObject.FindGameObjectsWithTag("Upgrade");
        foreach (GameObject i in UpgradeCards)
        {
            Destroy(i);
        }
        if(!PauseMenuScr.PauseMenuEnabled)
            {
                Time.timeScale = 1;
                PauseMenuScr.AllGamePauseEnabled = false;
            }        

        UpgradeMenuEnabled = false;
        UpgradeCanvas.SetActive(false);
    }
}
