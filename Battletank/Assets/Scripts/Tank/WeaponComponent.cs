using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PrimaryWeaponType { None, APShell, HEShell, SABOTShell }
public enum SecondaryWeaponType { None, SmartShell, Mortar, Artilery, Dart }

public class Weapon
{
    public int damage;
    public float bulletSpeed;
    public float bulletDrop;
    public float blastRadius;
    public Vector3 fireDir;
    public float fireTimer;
    public float rpm;
    public int currentMagazine;
    public int maxMagazine;
    public int maxAmmo;
    public int loadedAmmo;
    public float refillInterval;
}

[System.Serializable]
public class PrimaryWeapon : Weapon
{
    public PrimaryWeaponType type;
   
}

public class SecondaryWeapon : Weapon
{
    public SecondaryWeaponType type;
}

public class WeaponComponent : MonoBehaviour {

    [SerializeField]
    private PrimaryWeapon primaryWeapon;
    [SerializeField]
    private SecondaryWeapon secondaryWeapon;
    public GameObject projectile;
    public Transform firePos;

    private Tank tank;
    private bool mortarOn;
    private bool isRepairing;
    private float primaryRefillTimer;
    private float secondaryRefillTimer;

    private void Start()
    {
        isRepairing = false;
        primaryRefillTimer = 0f;
        secondaryRefillTimer = 0f;
        tank = GetComponent<Tank>();
        SetWeaponInfo();
    }

    private void Update()
    {
        if(!tank.IsDead() && !isRepairing)
        {
            if (mortarOn)
            {
                GameObject map = GetComponent<PlayerTankController>().map;
                if (Input.GetMouseButtonDown(0) && Time.time - secondaryWeapon.fireTimer > secondaryWeapon.rpm && secondaryWeapon.currentMagazine > 0)
                {
                    Vector3 clickedPosition = map.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                    secondaryWeapon.currentMagazine--;
                    //firer
                    Debug.Log("Firing Mortar on :" + clickedPosition);

                    GameObject pro = Instantiate(projectile, new Vector3(clickedPosition.x, map.transform.position.y, clickedPosition.z), Quaternion.identity) as GameObject;
                    secondaryWeapon.fireTimer = Time.time;
                    secondaryWeapon.fireDir = -pro.transform.up;

                    ProjectileComponent pc = pro.GetComponent<ProjectileComponent>();
                    pc.SetWeaponType(secondaryWeapon);
                    pc.SetFireInstanceId(transform.GetInstanceID());
                    pc.FireObject();
                }
                if (!map.activeSelf)
                {
                    mortarOn = false;
                }
            }
        }
        RefilAmmo();
        CheckPrimaryAmmo();
        CheckSecondaryAmmo();
    }

    public void FirePrimary(PrimaryWeaponType bulletType)
    {
        if (Time.time - primaryWeapon.fireTimer > primaryWeapon.rpm && primaryWeapon.currentMagazine > 0)
        {
            primaryWeapon.fireTimer = Time.time;
            primaryWeapon.currentMagazine--;
            primaryWeapon.fireDir = firePos.forward;
            GameObject pro = Instantiate(projectile, firePos.position, Quaternion.identity) as GameObject;
            ProjectileComponent pc = pro.GetComponent<ProjectileComponent>();
            pc.SetWeaponType(primaryWeapon);
            pc.FireObject();
        }
    }

    public void FireSecondary(SecondaryWeaponType bulletType)
    {
        if (secondaryWeapon.type == SecondaryWeaponType.Mortar || secondaryWeapon.type == SecondaryWeaponType.Artilery)
        {
            GameObject map = GetComponent<PlayerTankController>().map;
            if (!map.activeSelf)
            {
                map.SetActive(true);
                GetComponent<PlayerUIComponent>().EnableCursor();
            }
            mortarOn = true;
        }
        else
        {
            if (Time.time - secondaryWeapon.fireTimer > secondaryWeapon.rpm && secondaryWeapon.currentMagazine > 0)
            {
                secondaryWeapon.fireTimer = Time.time;
                secondaryWeapon.fireDir = firePos.forward;
                secondaryWeapon.currentMagazine--;
                GameObject pro = Instantiate(projectile, firePos.position, Quaternion.identity) as GameObject;
                ProjectileComponent pc = pro.GetComponent<ProjectileComponent>();
                pc.SetWeaponType(secondaryWeapon);
                pc.SetFireInstanceId(transform.GetInstanceID());
                pc.FireObject();
            }
        }
    }

    public void DefensiveActive()
    {

    }

    private void SetWeaponInfo()
    {
        SetPrimaryWeapon(tank.primaryBulletType);
        SetSecondaryWeapon(tank.secondaryBulletType);

    }

    private void SetPrimaryWeapon(PrimaryWeaponType primaryWeaponType)
    {
        primaryWeapon = new PrimaryWeapon();
        if(primaryWeaponType.Equals(PrimaryWeaponType.APShell))
        {
            primaryWeapon.blastRadius = 5f;
            primaryWeapon.bulletDrop = 10f;
            primaryWeapon.bulletSpeed = 1000f;
            primaryWeapon.damage = 30;
            primaryWeapon.type = PrimaryWeaponType.APShell;
            primaryWeapon.fireDir = firePos.forward;
            primaryWeapon.rpm = 60f / 30f;
            primaryWeapon.currentMagazine = 1;
            primaryWeapon.maxMagazine = 1;
            primaryWeapon.loadedAmmo = 5;
            primaryWeapon.maxAmmo = 5;
            primaryWeapon.refillInterval = 20f;
        }
        else if (primaryWeaponType.Equals(PrimaryWeaponType.HEShell))
        {
            primaryWeapon.blastRadius = 20f;
            primaryWeapon.bulletDrop = 10f;
            primaryWeapon.bulletSpeed = 1000f;
            primaryWeapon.damage = 20;
            primaryWeapon.type = PrimaryWeaponType.HEShell;
            primaryWeapon.fireDir = firePos.forward;
            primaryWeapon.rpm = 60f / 30f;
            primaryWeapon.currentMagazine = 1;
            primaryWeapon.maxMagazine = 1;
            primaryWeapon.loadedAmmo = 5;
            primaryWeapon.maxAmmo = 5;
            primaryWeapon.refillInterval = 20f;
        }
        else if(primaryWeaponType.Equals(PrimaryWeaponType.SABOTShell))
        {
            primaryWeapon.blastRadius = 2f;
            primaryWeapon.bulletDrop = 5f;
            primaryWeapon.bulletSpeed = 2000f;
            primaryWeapon.damage = 30;
            primaryWeapon.type = PrimaryWeaponType.SABOTShell;
            primaryWeapon.fireDir = firePos.forward;
            primaryWeapon.rpm = 60f / 10f;
            primaryWeapon.currentMagazine = 1;
            primaryWeapon.maxMagazine = 1;
            primaryWeapon.loadedAmmo = 5;
            primaryWeapon.maxAmmo = 5;
            primaryWeapon.refillInterval = 20f;
        }
    }

    private void SetSecondaryWeapon(SecondaryWeaponType secondaryWeaponType)
    {
        secondaryWeapon = new SecondaryWeapon();
        if (secondaryWeaponType.Equals(SecondaryWeaponType.SmartShell))
        {
            secondaryWeapon.blastRadius = 20f;
            secondaryWeapon.bulletDrop = 5f;
            secondaryWeapon.bulletSpeed = 2000f;
            secondaryWeapon.damage = 20;
            secondaryWeapon.type = SecondaryWeaponType.SmartShell;
            secondaryWeapon.fireDir = firePos.forward;
            secondaryWeapon.rpm = 60f / 10f;
            secondaryWeapon.currentMagazine = 1;
            secondaryWeapon.maxMagazine = 1;
            secondaryWeapon.loadedAmmo = 5;
            secondaryWeapon.maxAmmo = 5;
            secondaryWeapon.refillInterval = 20f;
        }
        else if (secondaryWeaponType.Equals(SecondaryWeaponType.Dart))
        {
            secondaryWeapon.rpm = 60f / 10f;
            secondaryWeapon.currentMagazine = 1;
            secondaryWeapon.maxMagazine = 1;
            secondaryWeapon.loadedAmmo = 5;
            secondaryWeapon.maxAmmo = 5;
            secondaryWeapon.refillInterval = 20f;
        }
        else if (secondaryWeaponType.Equals(SecondaryWeaponType.Artilery))
        {
            secondaryWeapon.type = SecondaryWeaponType.Artilery;
            secondaryWeapon.bulletDrop = 1f;
            secondaryWeapon.bulletSpeed = 200f;
            secondaryWeapon.damage = 20;
            secondaryWeapon.rpm = 60f / 10f;
            secondaryWeapon.currentMagazine = 1;
            secondaryWeapon.maxMagazine = 1;
            secondaryWeapon.loadedAmmo = 5;
            secondaryWeapon.maxAmmo = 5;
            secondaryWeapon.refillInterval = 20f;
        }
        else if (secondaryWeaponType.Equals(SecondaryWeaponType.Mortar))
        {
            secondaryWeapon.type = SecondaryWeaponType.Mortar;
            secondaryWeapon.bulletDrop = 1f;
            secondaryWeapon.bulletSpeed = 100f;
            secondaryWeapon.damage = 10;
            secondaryWeapon.rpm = 60f / 30f;
            secondaryWeapon.currentMagazine = 4;
            secondaryWeapon.maxMagazine = 4;
            secondaryWeapon.loadedAmmo = 20;
            secondaryWeapon.maxAmmo = 20;
            secondaryWeapon.refillInterval = 40f;
        }
    }

    private void CheckPrimaryAmmo()
    {
        //only for ai.
        if(primaryWeapon.currentMagazine == 0 && gameObject.layer == 10)
        {
            primaryWeapon.currentMagazine = primaryWeapon.maxMagazine;
            //TODO: check this later for balance
            primaryWeapon.loadedAmmo = primaryWeapon.maxMagazine;
        }
    }
    private void CheckSecondaryAmmo()
    {
        //only for ai.
        if (secondaryWeapon.currentMagazine == 0 && gameObject.layer == 10)
        {
            secondaryWeapon.currentMagazine = secondaryWeapon.maxMagazine;
            //TODO: check this later for balance
            secondaryWeapon.loadedAmmo = secondaryWeapon.maxMagazine;
        }
    }

    private void RefilAmmo()
    {
        primaryRefillTimer += Time.deltaTime;
        secondaryRefillTimer += Time.deltaTime;
        if (primaryRefillTimer > primaryWeapon.refillInterval)
        {
            primaryRefillTimer = 0f;
            primaryWeapon.loadedAmmo += primaryWeapon.maxMagazine;
            if (primaryWeapon.loadedAmmo > primaryWeapon.maxAmmo)
            {
                primaryWeapon.loadedAmmo = primaryWeapon.maxAmmo;
            }
        }

        if (secondaryRefillTimer > secondaryWeapon.refillInterval)
        {
            secondaryRefillTimer = 0f;
            secondaryWeapon.loadedAmmo += primaryWeapon.maxMagazine;
            if (secondaryWeapon.loadedAmmo > secondaryWeapon.maxAmmo)
            {
                secondaryWeapon.loadedAmmo = secondaryWeapon.maxAmmo;
            }
        }
    }



    public void ReloadPrimaryAmmo()
    {
        if(primaryWeapon.maxMagazine > primaryWeapon.currentMagazine)
        {
            int requireAmmo = (primaryWeapon.maxMagazine - primaryWeapon.currentMagazine);
            if (primaryWeapon.loadedAmmo >= requireAmmo)
            {
                primaryWeapon.loadedAmmo -= requireAmmo;
                primaryWeapon.currentMagazine += requireAmmo;
                primaryWeapon.loadedAmmo = (primaryWeapon.loadedAmmo < 0) ? 0 : primaryWeapon.loadedAmmo;
            }
            else
            {
                primaryWeapon.currentMagazine += primaryWeapon.loadedAmmo;
                primaryWeapon.loadedAmmo = 0;
            }
        }
        
    }

    public void ReloadSecondaryAmmo()
    {
        if(secondaryWeapon.maxMagazine > secondaryWeapon.currentMagazine)
        {
            int requireAmmo = (secondaryWeapon.maxMagazine - secondaryWeapon.currentMagazine);
            if (secondaryWeapon.loadedAmmo >= requireAmmo)
            {
                secondaryWeapon.loadedAmmo -= requireAmmo;
                secondaryWeapon.currentMagazine += requireAmmo;
                secondaryWeapon.loadedAmmo = (secondaryWeapon.loadedAmmo < 0) ? 0 : secondaryWeapon.loadedAmmo;
            }
            else
            {
                secondaryWeapon.currentMagazine += secondaryWeapon.loadedAmmo;
                secondaryWeapon.loadedAmmo = 0;
            }
        }
        
        
    }

    public void SetRepairFunction()
    {
        isRepairing = !isRepairing;
    }

    public string GetPrimaryAmmoInfo()
    {
        return primaryWeapon.currentMagazine + " / " + primaryWeapon.loadedAmmo;
    }

    public string GetSecondaryAmmoInfo()
    {
        return secondaryWeapon.currentMagazine + " / " + secondaryWeapon.loadedAmmo;
    }

}
