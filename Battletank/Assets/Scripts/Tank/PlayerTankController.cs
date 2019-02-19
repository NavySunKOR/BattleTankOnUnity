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
    private WeaponComponent weaponComponent;
    private MovementComponent movementComponent;
    private bool isMagnified;
    private bool isFirstPerson;

    private void Awake()
    {
        mainCamera = Camera.main;
        tank = GetComponent<Tank>();
        trdCamera = GetComponent<ThirdPersonCamera>();
        uiComponent = GetComponent<PlayerUIComponent>();
        //
        weaponComponent = GetComponent<WeaponComponent>();
        movementComponent = GetComponent<MovementComponent>();

        isMagnified = false;
        isFirstPerson = false;
    }



    private void Update()
    {
        if(!tank.IsDead())
        {
            AimProjection();
            InputControl();
            CheckMagnified();
        }
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

        if(Input.GetKeyDown(KeyCode.X))
        {
            SetFixState();
        }

        if(Input.GetKey(KeyCode.X))
        {
            tank.Repair();
        }
        else if(Input.GetKeyUp(KeyCode.X))
        {
            tank.ResetHoldRepair();
            SetFixState();
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

        if(Input.GetMouseButtonDown(1))
        {
            isMagnified = !isMagnified;
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

    }


    private void AimProjection()
    {
        Transform cameraTr = trdCamera.cameraTr;
        Ray ray = new Ray(cameraTr.position, cameraTr.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 lookEuler = Quaternion.LookRotation((hit.point - cameraTr.position).normalized).eulerAngles;
            lookEuler.x = trdCamera.pitch;
            tank.AimAt(lookEuler);
        }
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

    private void CheckMagnified()
    {
        if(isFirstPerson)
        {
            trdCamera.cameraOffset.z = (isMagnified) ? 5f : 0f;
        }
        else
        {
            trdCamera.cameraOffset.z = (isMagnified) ? -5f : -10f;
        }
       // 
    }

    private void SetFixState()
    {
        //can't fire,move while reparing.
        weaponComponent.SetRepairFunction();
        movementComponent.SetRepairFunction();
        tank.SetRepairFunction();
    }

    private void Reload()
    {
        tank.Reload();
    }


}
