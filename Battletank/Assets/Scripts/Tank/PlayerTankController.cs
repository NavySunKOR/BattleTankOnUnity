using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum WeaponType { Primary, Secondary };

public class PlayerTankController : MonoBehaviour {


    public LayerMask aimLayer;
    public GameObject map;

    private Tank tank;
    private Camera mainCamera;
    private ThirdPersonCamera trdCamera;
    private Vector3 viewRotation;
    private PlayerUIComponent uiComponent;

    private void Awake()
    {
        mainCamera = Camera.main;
        tank = GetComponent<Tank>();
        trdCamera = GetComponent<ThirdPersonCamera>();
        uiComponent = GetComponent<PlayerUIComponent>();
    }



    private void Update()
    {
        AimProjection();
        InputControl();
    }

    private void InputControl()
    {
        //to prevent backfire of map picking 
        if(Input.GetMouseButtonDown(0) && !map.activeSelf) 
        {
            tank.Fire();
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        tank.Move(vertical);
        tank.Rotate(horizontal);

        if(Input.GetKey(KeyCode.X))
        {
            tank.Repair();
        }
        else if(Input.GetKeyUp(KeyCode.X))
        {
            tank.ResetHoldRepair();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Primary!");
            tank.SetWeaponType(WeaponType.Primary);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Secondary!");
            tank.SetWeaponType(WeaponType.Secondary);
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            MapActive();
        }

    }


    private void AimProjection()
    {
        //fuck the raycast.
        viewRotation = new Vector3(trdCamera.pitch, trdCamera.armTr.localEulerAngles.y, 0);
        tank.AimAt(viewRotation);
    }

    private void MapActive()
    {
        map.SetActive(!map.activeSelf);
        if(map.activeSelf)
        {
            uiComponent.EnableCursor();
        }
        else
        {
            uiComponent.DisableCursor();
        }
    }

    
}
