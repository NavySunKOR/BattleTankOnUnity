using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

    public float tankMaxRange = 100000f;
    public int health = 100;
    public float repairInterval;
    public float repairHoldInterval;
    public PrimaryWeaponType primaryBulletType;
    public SecondaryWeaponType secondaryBulletType;

    private AimComponent aimComponent;
    private MovementComponent movementComponent;
    private WeaponComponent weaponComponent;
    private float repairTimer;
    private float repairHoldTimer;
    private WeaponType weaponType;


    // Use this for initialization
    private void Awake () {
        weaponType = WeaponType.Primary;
        repairTimer = 0f;
        repairHoldTimer = 0f;
        aimComponent = GetComponent<AimComponent>();
        movementComponent = GetComponent<MovementComponent>();
        weaponComponent = GetComponent<WeaponComponent>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AimAt(Vector3 position)
    {
        aimComponent.StartCoroutine("AimAt",position);
    }

    public void Fire()
    {
        if(weaponType == WeaponType.Primary)
        {
            weaponComponent.FirePrimary(primaryBulletType);
        }
        else
        {
            weaponComponent.FireSecondary(secondaryBulletType);
        }
    }

    public void SetWeaponType(WeaponType weaponType)
    {
        this.weaponType = weaponType;
    }

    public void Repair()
    {
        if(Time.time - repairTimer>repairInterval)
        {
            repairHoldTimer += Time.deltaTime;
            if(repairHoldTimer > repairHoldInterval)
            {
                //add UI
                ResetHoldRepair();
                health += 30;
                if (health > 100)
                {
                    health = 100;
                }
            }
        }
    }

    public void Move(float vertical)
    {
        movementComponent.Move(vertical);
    }

    public void Rotate(float horizontal)
    {
        movementComponent.Rotate(horizontal);
    }

    public void TookDamage(int damage)
    {
        health -= damage;
    }

    public void ResetHoldRepair()
    {
        repairHoldTimer = 0f;
    }
}
