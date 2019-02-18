using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ProjectileComponent : MonoBehaviour {

    private float bulletDrop;
    private Vector3 bulletDir;
    private float bulletSpeed;
    private float blastRadius;
    private int damage;
    private Rigidbody rb;
    private bool fire;
    private PrimaryWeaponType primaryType;
    private SecondaryWeaponType secondaryType;
    private bool lockOn;
    private Collider lockOnColl;
    private int fireInstanceId;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody>();
        primaryType = PrimaryWeaponType.None;
        secondaryType = SecondaryWeaponType.None;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if(fire)
        {
            Debug.Log(primaryType);
            Debug.Log(secondaryType);
            if (primaryType != PrimaryWeaponType.None)
            {
                FlyProjectile();
            }
            else if(secondaryType == SecondaryWeaponType.SmartShell)
            {
                Debug.Log("Target locking");
                FlyProjectile();
                if(!lockOn)
                {
                    Collider[] colls = Physics.OverlapSphere(transform.position, 20f);
                    foreach (Collider coll in colls)
                    {
                        if (coll.CompareTag("Tank") && !coll.transform.GetInstanceID().Equals(fireInstanceId))
                        {
                            lockOnColl = coll;
                            lockOn = true;
                            Debug.Log("Target acquired");
                        }
                    }
                }
                else
                {
                    Vector3 direction = (lockOnColl.transform.position - transform.position).normalized;
                    transform.eulerAngles = Quaternion.LookRotation(direction).eulerAngles;
                    bulletDir = transform.forward;
                }
            }
            else if(secondaryType == SecondaryWeaponType.Mortar || secondaryType == SecondaryWeaponType.Artilery)
            {
                FlyProjectile();
            }
        }

        if(transform.position.y < 0f)
        {
            Destroy(gameObject);
        }
    }
    
    public void FireObject()
    {
        fire = true;
    }

    public void SetWeaponType(Weapon weaponType)
    {
        if(weaponType.GetType() == typeof(PrimaryWeapon))
        {
            PrimaryWeapon primary = (PrimaryWeapon)weaponType;
            primaryType = primary.type;
            bulletSpeed = primary.bulletSpeed;
            bulletDrop = primary.bulletDrop;
            bulletDir = primary.fireDir;
            blastRadius = primary.blastRadius;
            damage = primary.damage;
        }
        else
        {
            Debug.Log("Setting Secondary");
            SecondaryWeapon secondary = (SecondaryWeapon)weaponType;
            secondaryType = secondary.type;
            Debug.Log(secondaryType);
            bulletSpeed = secondary.bulletSpeed;
            bulletDrop = secondary.bulletDrop;
            bulletDir = secondary.fireDir;
            blastRadius = secondary.blastRadius;
            damage = secondary.damage;
        }
    }

    public void SetFireInstanceId(int inst)
    {
        fireInstanceId = inst;
    }

    private void FlyProjectile()
    {
        if ((bulletDir * bulletSpeed).magnitude > rb.velocity.magnitude)
        {
            rb.AddForce(bulletDir * bulletSpeed, ForceMode.Acceleration);
        }
        rb.AddForce(Physics.gravity * bulletDrop);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Tank"))
        {
            collision.transform.GetComponent<Tank>().TookDamage(damage);
        }
        else
        {
            int enemy = 1 << 10;
            int player = 1 << 11;
            int layer = enemy | player;
            layer = ~layer;

            Collider[] collider = Physics.OverlapSphere(transform.position, blastRadius, layer);
            foreach (Collider coll in collider)
            {
                if (coll.transform.CompareTag("Tank"))
                {
                    coll.GetComponent<Tank>().TookDamage((int)damage / 2);
                }
            }
        }
        Destroy(gameObject);
    }
}
