using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour {
    [System.Serializable]
    public class PrimaryWeapon
    {
        public int damage;
        public float bulletSpeed;
        public float bulletDrop;
        public float blastRadius;
    }
    public enum SecondaryWeaponType
    {Mortar,Artilery,Guided,Dart}

    public class SecondaryWeapon
    {
        public SecondaryWeaponType type;
    }

    public PrimaryWeapon primaryWeapon;
    public SecondaryWeapon secondaryWeapon;
    public GameObject projectile;
    public Transform firePos;

    public void FirePrimary()
    {
        GameObject pro = Instantiate(projectile, firePos.position, Quaternion.identity) as GameObject;
        ProjectileComponent pc = pro.GetComponent<ProjectileComponent>();
        pc.bulletDrop = primaryWeapon.bulletDrop;
        pc.maxBulletSpeed = firePos.forward * primaryWeapon.bulletSpeed;
        pc.blastRadius = primaryWeapon.blastRadius;
        pc.damage = primaryWeapon.damage;
        pc.FireObject();
    }

    public void FireSecondary()
    {

    }

    public void DefensiveActive()
    {

    }

    public void Magnify()
    {

    }

    public void Repair()
    {

    }

}
