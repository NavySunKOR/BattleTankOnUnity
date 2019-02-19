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

    private PlayerTankController playerTankController;
    private AimComponent aimComponent;
    private MovementComponent movementComponent;
    private WeaponComponent weaponComponent;
    private PlayerUIComponent playerUIComponent;
    private float repairHoldTimer;
    private WeaponType weaponType;
    private bool isRepairing;



    // Use this for initialization
    private void Awake () {
        weaponType = WeaponType.Primary;
        repairHoldTimer = 0f;
        aimComponent = GetComponent<AimComponent>();
        movementComponent = GetComponent<MovementComponent>();
        weaponComponent = GetComponent<WeaponComponent>();
        playerUIComponent = GetComponent<PlayerUIComponent>();
        playerTankController = GetComponent<PlayerTankController>();
        UpdateHealth();
    }

    private void Update()
    {
        if(health <= 0)
        {
            health = 0;
            UpdateHealth();
            if (gameObject.layer == 11)
            {
                playerTankController.DisableFunction();
                weaponComponent.DisabledFunction();
                movementComponent.DisableFunction();
            }
            else
                Destroy(gameObject);
        }
    }

    public void AimAt(Vector3 position)
    {
        aimComponent.StartCoroutine("AimAt",position);
    }

    public void Fire()
    {
        if(!isRepairing)
        {
            if (weaponType == WeaponType.Primary)
            {
                weaponComponent.FirePrimary(primaryBulletType);
            }
            else
            {
                weaponComponent.FireSecondary(secondaryBulletType);
            }
        }
    }

    public void SetWeaponType(WeaponType weaponType)
    {
        this.weaponType = weaponType;
    }

    public void Repair()
    {
        repairHoldTimer += Time.deltaTime;
        UpdateFixUI(repairHoldTimer);
        if (repairHoldTimer > repairHoldInterval)
        {
            //add UI
            ResetHoldRepair();
            health += 30;
            if (health > 100)
            {
                health = 100;
            }
            UpdateHealth();
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
        UpdateHealth();
    }

    public void ResetHoldRepair()
    {
        repairHoldTimer = 0f;
        UpdateFixUI(repairHoldTimer);
    }

    public void SetRepairFunction()
    {
        isRepairing = !isRepairing;
    }

    private void UpdateFixUI(float timer)
    {
        if (gameObject.layer == 11)
            playerUIComponent.UpdateFixUI(repairHoldTimer);
    }

    private void UpdateHealth()
    {
        if(gameObject.layer == 11)
            playerUIComponent.UpdateHealth(health);
    }

    public void Reload()
    {
        if(weaponType.Equals(WeaponType.Primary))
        {
            weaponComponent.ReloadPrimaryAmmo();
        }
        else
        {
            weaponComponent.ReloadSecondaryAmmo();
        }
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
    }
    
}
