using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShootScr : MonoBehaviour
{
    public Transform GunPoint;
    public Transform GunPointRepelant;
    public Transform ShotgunPoint;
    public Transform ShotgunPoint2;
    public Transform ShotgunPoint3;
    public GameObject BulletPrefab;
    public GameObject BazookaPrefab;
    public GameObject SpreyPrefab;
    public float BulletSpeed = 8f;
    public float WeaponFireRate = 0.1f;
    public float WeaponFireTimer = 0.00f;
    public int WeaponDamage;
    public enum GunType {MosquitoRepellent, Pistol, Shotgun, Uzi, Bazooka, Burst};
    public static List<string> Weapons = new List<string>();
    public GunType Guntype;
    int GunTypeLength = System.Enum.GetValues(typeof(GunType)).Length;
    bool MosquitoActive = false; 
    GameObject MosqRepWeapon;
    bool Bursting = false;
    public float ReloadTime = 2f;
    int CurPistolAmmo = 9 , CurShotgunAmmo = 10 , CurUziAmmo = 30, CurBazookaAmmo = 10, CurBurstAmmo = 7; 
    public static int MaxAmmo, CurrentAmmo;
    bool ReloadReady, reloading = false;
    public Text textbox; 
    GameObject UpgradeSys;
    int SwitchWeapon =0; 
    bool WeaponFound; 
    public Animator animator;
    //bool AmmoUp; 
    
    void OnEnable() 
    {
        reloading = false;
        ShootScr.Weapons.Clear();
        Guntype = GunType.MosquitoRepellent;
        ChooseWeapon();
        CurrentAmmo = MaxAmmo;
        Guntype = GunType.MosquitoRepellent; 
        animator.SetInteger("Weapon",1);

        UpgradeSys = GameObject.Find("UpgradeSystem");
        UpgradeSystemScr UpgradeSystemScr = UpgradeSys.GetComponent<UpgradeSystemScr>();
        for (int i=1; i<10; i++)
            Weapons.Add("");  
    
        Weapons.Insert(0,"MosquitoRepellent");
        CurPistolAmmo = 9 ; CurShotgunAmmo = 10 ; CurUziAmmo = 30; CurBazookaAmmo = 10; CurBurstAmmo = 7; 
        
    }

    void Update()
    {   
        if(PlayerScr.CanMove)
        {
            if(WeaponFireTimer < WeaponFireRate)
            WeaponFireTimer += Time.deltaTime;

            if(Input.GetButton("Fire1"))
            {
                if(WeaponFireTimer >= WeaponFireRate)
                {
                    WeaponFireTimer = 0.0f;
                    switch (Guntype)
                    {
                        case GunType.Pistol: ShootPistol(); break;
                        case GunType.Uzi: ShootUzi(); break;
                        case GunType.Shotgun: ShootShotgun(); break;
                        case GunType.MosquitoRepellent: ShootMosquitoRepellent(); break;
                        case GunType.Bazooka: ShootBazooka(); break;
                        case GunType.Burst: ShootBurst(); break;
                    }    
                }
            }
            if(Input.GetButtonUp("Fire1"))
            {
                FindObjectOfType<MusicSystem>().Stop("Spray");
                MosquitoActive = false; 
                Destroy(MosqRepWeapon);
            }
            

            if(Input.GetKeyDown(KeyCode.Space))
            {  
                StopCoroutine(Reload());
                Destroy(MosqRepWeapon);
            /* if(Guntype.GetHashCode() < GunTypeLength-1)
                Guntype += 1;
                else
                Guntype += -GunTypeLength+1; */
                WeaponFound = false;
                do 
                {
                    if(SwitchWeapon < Weapons.Count-1) 
                    SwitchWeapon++;
                    else SwitchWeapon += -Weapons.Count+1;
                    if(Weapons[SwitchWeapon].Any())
                    WeaponFound = true;                     
                } 
                while (WeaponFound == false);
                switch (Weapons[SwitchWeapon])
                {
                    case "MosquitoRepellent": Guntype = GunType.MosquitoRepellent; animator.SetInteger("Weapon",1); break;
                    case "Pistol": Guntype = GunType.Pistol; animator.SetInteger("Weapon",2); break;
                    case "Shotgun": Guntype = GunType.Shotgun; animator.SetInteger("Weapon",3); break;
                    case "Uzi": Guntype = GunType.Uzi; animator.SetInteger("Weapon",4); break;
                    case "Bazooka": Guntype = GunType.Bazooka; animator.SetInteger("Weapon",5); break;
                    case "Burst": Guntype = GunType.Burst; animator.SetInteger("Weapon",6); break;
                }
                    //Debug.Log(Weapons[SwitchWeapon].Contains("P"));
                    //Debug.Log(Weapons[SwitchWeapon]);
                //Guntype += Guntype.GetHashCode() < GunTypeLength-1 ?  1 : -GunTypeLength+1;
                
                ChooseWeapon();
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                if(!reloading && CurrentAmmo != MaxAmmo)
                StartCoroutine(Reload());
            }
        }
        else
        {
            FindObjectOfType<MusicSystem>().Stop("Spray");
            MosquitoActive = false; 
            Destroy(MosqRepWeapon);
        }
        switch (Guntype)
        {
            case GunType.Pistol: CurrentAmmo = CurPistolAmmo; break;
            case GunType.Uzi: CurrentAmmo = CurUziAmmo; break;
            case GunType.Shotgun: CurrentAmmo = CurShotgunAmmo; break;
            case GunType.MosquitoRepellent: break;
            case GunType.Bazooka: CurrentAmmo = CurBazookaAmmo; break;
            case GunType.Burst: CurrentAmmo = CurBurstAmmo; break;
        }  
        if(Guntype == GunType.MosquitoRepellent) 
        textbox.text = Guntype + ": " + "--";
        else
        textbox.text = Guntype + ": " + CurrentAmmo + ("/") + MaxAmmo;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, GunPoint.position, GunPoint.rotation);
        Rigidbody2D BulletRb = bullet.GetComponent<Rigidbody2D>();
        BulletRb.AddForce(GunPoint.up * BulletSpeed, ForceMode2D.Impulse); 
        Destroy(bullet,3);
        FindObjectOfType<MusicSystem>().SoundEffects("GunShot"); 
    }

    void ShootPistol()
    {
        if(CurPistolAmmo > 0 && !reloading)
        {
            Shoot();
            CurPistolAmmo--;
            return;
        } 
        else if (CurPistolAmmo == 0 && !reloading)
            StartCoroutine(Reload());
    }

    void ShootUzi()
    {
        if(CurUziAmmo > 0 && !reloading)
        {
            Shoot();
            CurUziAmmo--;
            return;
        } 
        else if (CurUziAmmo == 0 && !reloading)
            StartCoroutine(Reload());
    }

    void ShootShotgun() 
    {
        if(CurShotgunAmmo > 0 && !reloading)
        {
            GameObject bullet = Instantiate(BulletPrefab, ShotgunPoint.position, ShotgunPoint.rotation);
            GameObject bullet2 = Instantiate(BulletPrefab, ShotgunPoint2.position, ShotgunPoint2.rotation);
            GameObject bullet3 = Instantiate(BulletPrefab, ShotgunPoint3.position, ShotgunPoint3.rotation);
            Rigidbody2D BulletRb = bullet.GetComponent<Rigidbody2D>();
            Rigidbody2D BulletRb2 = bullet2.GetComponent<Rigidbody2D>();
            Rigidbody2D BulletRb3 = bullet3.GetComponent<Rigidbody2D>();
            BulletRb.AddForce(ShotgunPoint.up * BulletSpeed, ForceMode2D.Impulse); 
            BulletRb2.AddForce(ShotgunPoint2.up * BulletSpeed, ForceMode2D.Impulse); 
            BulletRb3.AddForce(ShotgunPoint3.up * BulletSpeed, ForceMode2D.Impulse); 
            Destroy(bullet,3);
            Destroy(bullet2,3);
            Destroy(bullet3,3);
            CurShotgunAmmo--;
            FindObjectOfType<MusicSystem>().SoundEffects("ShotunShot"); 
            return;
        }
        else if (CurShotgunAmmo == 0 && !reloading)
            StartCoroutine(Reload());
    }

    void ShootMosquitoRepellent()
    {
        if(!MosquitoActive)
        {
                FindObjectOfType<MusicSystem>().Play("Spray");

                

            MosqRepWeapon = Instantiate(SpreyPrefab, GunPointRepelant.position, GunPointRepelant.rotation);
            MosquitoActive = true; 
        } 
    }   

    void ShootBazooka()
    {
        if(CurBazookaAmmo > 0 && !reloading)
        {
            GameObject BazookaBullet = Instantiate(BazookaPrefab, GunPoint.position, GunPoint.rotation);
            Rigidbody2D BazookaBulletRb = BazookaBullet.GetComponent<Rigidbody2D>();
            BazookaBulletRb.AddForce(GunPoint.up * BulletSpeed, ForceMode2D.Impulse);
            CurBazookaAmmo--;
            FindObjectOfType<MusicSystem>().SoundEffects("BazookaShot"); 
            Destroy(BazookaBullet,3);
            return;
        }
        else if (CurBazookaAmmo == 0 && !reloading)
            StartCoroutine(Reload());
    }

    void ShootBurst()
    {
        if(CurBurstAmmo > 0 && !reloading)
        {
            if(!Bursting)
            {
                Bursting = true;
                StartCoroutine(BurstFire());
            }
            CurBurstAmmo--;
            return;
        }
        else if (CurBurstAmmo == 0 && !reloading)
            StartCoroutine(Reload());
    }

    IEnumerator BurstFire()
    {
        yield return new WaitForSeconds(0.1f);
        Shoot();
        yield return new WaitForSeconds(0.1f);
        Shoot();
        yield return new WaitForSeconds(0.1f);
        Shoot();
        Bursting = false;   
    }    

    IEnumerator Reload()
    {
        reloading = true;
        if (reloading)
        {
            if(!UpgradeSystemScr.FasterReload)
            {
                FindObjectOfType<MusicSystem>().SoundEffects("GunReload");  
                yield return new WaitForSeconds(ReloadTime);
            }
            else
            {
                FindObjectOfType<MusicSystem>().SoundEffects("GunReloadFaster"); 
                yield return new WaitForSeconds(ReloadTime/2);
            }
            
            switch (Guntype)
            {
                case GunType.Pistol: CurPistolAmmo = MaxAmmo;  break;  
                case GunType.Uzi: CurUziAmmo = MaxAmmo ; break; 
                case GunType.Shotgun: CurShotgunAmmo = MaxAmmo ; break;
                case GunType.Bazooka: CurBazookaAmmo = MaxAmmo ; break;  
                case GunType.Burst: CurBurstAmmo = MaxAmmo ; break;  
            }

            reloading = false;
           // Debug.Log("Reload Ready");
        }
    }
    void ChooseWeapon()
    {
        if(!UpgradeSystemScr.AmmoUp)
        {
            switch (Guntype)
            {
                case GunType.Pistol: BulletSpeed = 10f; WeaponFireRate = 0.5f; WeaponDamage = 15; MaxAmmo = 9;  break;  
                case GunType.Uzi: BulletSpeed = 15f; WeaponFireRate = 0.06f; WeaponDamage = 7; MaxAmmo = 30; break; 
                case GunType.Shotgun: BulletSpeed = 12f; WeaponFireRate = 0.7f; WeaponDamage = 16; MaxAmmo = 10; break;
                case GunType.MosquitoRepellent: BulletSpeed = 12f; WeaponFireRate = 0.1f; WeaponDamage = 1;  break;  
                case GunType.Bazooka: BulletSpeed = 10f; WeaponFireRate = 1.5f; WeaponDamage = 50; MaxAmmo = 10; break;  
                case GunType.Burst: BulletSpeed = 17f; WeaponFireRate = 0.8f; WeaponDamage = 15; MaxAmmo = 7; break;  
            }
        }
        else
        {
            switch (Guntype)
            {
                case GunType.Pistol: BulletSpeed = 10f; WeaponFireRate = 0.5f; WeaponDamage = 15; MaxAmmo = 14;  break;  
                case GunType.Uzi: BulletSpeed = 15f; WeaponFireRate = 0.06f; WeaponDamage = 7; MaxAmmo = 45; break; 
                case GunType.Shotgun: BulletSpeed = 12f; WeaponFireRate = 0.7f; WeaponDamage = 16; MaxAmmo = 15; break;
                case GunType.MosquitoRepellent: BulletSpeed = 12f; WeaponFireRate = 0.1f; WeaponDamage = 1;  break;  
                case GunType.Bazooka: BulletSpeed = 10f; WeaponFireRate = 1.5f; WeaponDamage = 50; MaxAmmo = 15; break;  
                case GunType.Burst: BulletSpeed = 17f; WeaponFireRate = 0.8f; WeaponDamage = 15; MaxAmmo = 11; break;  
            }            
        }
        if(!UpgradeSystemScr.AttackSpdBool)
        {
            switch (Guntype)
            {
                case GunType.Pistol:            WeaponFireRate = 0.5f; break;  
                case GunType.Uzi:               WeaponFireRate = 0.06f; break; 
                case GunType.Shotgun:           WeaponFireRate = 0.7f; break;
                case GunType.MosquitoRepellent: WeaponFireRate = 0.1f; break;  
                case GunType.Bazooka:           WeaponFireRate = 1.5f; break;  
                case GunType.Burst:             WeaponFireRate = 0.8f; break;  
            }
        }
        else
        {
            switch (Guntype)
            {
                case GunType.Pistol:            WeaponFireRate = 0.3f; break;  
                case GunType.Uzi:               WeaponFireRate = 0.04f; break; 
                case GunType.Shotgun:           WeaponFireRate = 0.4f; break;
                case GunType.MosquitoRepellent: WeaponFireRate = 0.1f; break;  
                case GunType.Bazooka:           WeaponFireRate = 1f; break;  
                case GunType.Burst:             WeaponFireRate = 0.5f; break;  
            }
        }
    }
}
