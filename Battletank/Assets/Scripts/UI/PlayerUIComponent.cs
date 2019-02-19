using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIComponent : MonoBehaviour {

    public GameObject crosshair;
    public Text healthText;
    public Slider repairSlider;


	// Use this for initialization
	private void Awake () {
        DisableCursor();
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
}
