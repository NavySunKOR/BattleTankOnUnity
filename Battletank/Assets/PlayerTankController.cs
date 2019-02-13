using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankController : MonoBehaviour {


    public LayerMask aimLayer;

    private Tank tank;
    private Camera mainCamera;
    private Transform cameraTr;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraTr = mainCamera.transform;
        tank = GetComponent<Tank>();
    }



    private void Update()
    {
        
        Ray ray = new Ray(cameraTr.position, cameraTr.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, tank.tankMaxRange, aimLayer))
        {
            tank.AimAt(hit.point);
        }


    }

    
}
