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

    private PrimaryWeapon primaryWeapon;
    private SecondaryWeapon secondaryWeapon;
    public GameObject projectile;
    public Transform firePos;

    private bool mortarOn;

    private void Update()
    {
        if(mortarOn)
        {
            GameObject map = GetComponent<PlayerTankController>().map;
            if (Input.GetMouseButtonDown(0) && Time.time - secondaryWeapon.fireTimer > secondaryWeapon.rpm)
            {
                Vector3 clickedPosition = map.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                    //firer
                Debug.Log("Firing Mortar on :" + clickedPosition);
                
                GameObject pro = Instantiate(projectile, new Vector3(clickedPosition.x,map.transform.position.y, clickedPosition.z), Quaternion.identity) as GameObject;
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

    public void FirePrimary(PrimaryWeaponType bulletType)
    {
        SetPrimaryWeapon(bulletType);
        if (Time.time - primaryWeapon.fireTimer > primaryWeapon.rpm)
        {
            primaryWeapon.fireTimer = Time.time;
            GameObject pro = Instantiate(projectile, firePos.position, Quaternion.identity) as GameObject;
            ProjectileComponent pc = pro.GetComponent<ProjectileComponent>();
            pc.SetWeaponType(primaryWeapon);
            pc.FireObject();
        }
    }

    public void FireSecondary(SecondaryWeaponType bulletType)
    {
        SetSecondaryWeapon(bulletType);
        Debug.Log("FireClicked Secondary");
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
            if (Time.time - secondaryWeapon.fireTimer > secondaryWeapon.rpm)
            {
                secondaryWeapon.fireTimer = Time.time;
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

    private void SetPrimaryWeapon(PrimaryWeaponType primaryWeaponType)
    {
        if(primaryWeapon == null)
        {
            primaryWeapon = new PrimaryWeapon();
        }
        if(primaryWeaponType.Equals(PrimaryWeaponType.APShell))
        {
            primaryWeapon.blastRadius = 5f;
            primaryWeapon.bulletDrop = 10f;
            primaryWeapon.bulletSpeed = 1000f;
            primaryWeapon.damage = 30;
            primaryWeapon.type = PrimaryWeaponType.APShell;
            primaryWeapon.fireDir = firePos.forward;
            primaryWeapon.rpm = 60f / 30f;
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
        }
    }

    private void SetSecondaryWeapon(SecondaryWeaponType secondaryWeaponType)
    {
        if (secondaryWeapon == null)
        {
            secondaryWeapon = new SecondaryWeapon();
        }
        if (secondaryWeaponType.Equals(SecondaryWeaponType.SmartShell))
        {
            secondaryWeapon.blastRadius = 20f;
            secondaryWeapon.bulletDrop = 5f;
            secondaryWeapon.bulletSpeed = 2000f;
            secondaryWeapon.damage = 20;
            secondaryWeapon.type = SecondaryWeaponType.SmartShell;
            secondaryWeapon.fireDir = firePos.forward;
            secondaryWeapon.rpm = 60f / 10f;
        }
        else if (secondaryWeaponType.Equals(SecondaryWeaponType.Dart))
        {
            secondaryWeapon.rpm = 60f / 10f;
        }
        else if (secondaryWeaponType.Equals(SecondaryWeaponType.Artilery))
        {
            secondaryWeapon.type = SecondaryWeaponType.Artilery;
            secondaryWeapon.bulletDrop = 1f;
            secondaryWeapon.bulletSpeed = 200f;
            secondaryWeapon.damage = 20;
            secondaryWeapon.rpm = 60f / 10f;
        }
        else if (secondaryWeaponType.Equals(SecondaryWeaponType.Mortar))
        {
            secondaryWeapon.type = SecondaryWeaponType.Mortar;
            secondaryWeapon.bulletDrop = 1f;
            secondaryWeapon.bulletSpeed = 100f;
            secondaryWeapon.damage = 10;
            secondaryWeapon.rpm = 60f / 30f;
        }
    }

}
