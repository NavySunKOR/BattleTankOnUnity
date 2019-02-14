using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankController : MonoBehaviour {


    public LayerMask aimLayer;

    private Tank tank;
    private Camera mainCamera;
    private ThirdPersonCamera trdCamera;
    private Vector3 viewRotation;

    private void Awake()
    {
        mainCamera = Camera.main;
        tank = GetComponent<Tank>();
        trdCamera = GetComponent<ThirdPersonCamera>();
    }



    private void Update()
    {
        AimProjection();
        InputControl();


    }

    private void InputControl()
    {
        if(Input.GetMouseButtonDown(0))
        {
            tank.FirePrimary();
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        tank.Move(vertical);
        tank.Rotate(horizontal);

    }


    private void AimProjection()
    {
        //fuck the raycast.
        viewRotation = new Vector3(trdCamera.pitch, trdCamera.yaw, 0);
        tank.AimAt(viewRotation);
    }

    
}
