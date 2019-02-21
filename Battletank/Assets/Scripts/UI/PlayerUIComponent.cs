using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIComponent : MonoBehaviour {

    public GameObject crosshair;
    public Text healthText;
    public Slider repairSlider;
    public Text ammoText;
    public Text weaponText;

    private Tank tank;
    private WeaponComponent weaponComponent;


	// Use this for initialization
	private void Awake () {
        DisableCursor();
        tank = GetComponent<Tank>();
        weaponComponent = GetComponent<WeaponComponent>();
    }

    private void LateUpdate()
    {
        UpdateAmmo();
        UpateWeaponType(); // change to update by event later
    }

    public void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        crosshair.SetActive(true);
    }

    public void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        crosshair.SetActive(false);
    }

    public void UpdateHealth(int health)
    {
        Color color = Color.green;
        if (health < 30)
        {
            color = Color.red;
        }
        else if (health < 60)
        {
            color = Color.yellow;
        }
        healthText.color = color;
        healthText.text = health.ToString();

    }

    public void UpdateFixUI(float timer)
    {
        repairSlider.value = timer;
    }

    private void UpdateAmmo()
    {
        if(tank.GetWeaponType() == WeaponType.Primary)
        {
            ammoText.text = weaponComponent.GetPrimaryAmmoInfo();
        }
        else
        {
            ammoText.text = weaponComponent.GetSecondaryAmmoInfo();
        }
    }

    private void UpateWeaponType()
    {
        if(tank.GetWeaponType() == WeaponType.Primary)
        {
            weaponText.text = tank.primaryBulletType.ToString();
        }
        else
        {
            weaponText.text = tank.secondaryBulletType.ToString();
        }
       
    }
}
