using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIComponent : MonoBehaviour {

    public GameObject crosshair;


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
}
