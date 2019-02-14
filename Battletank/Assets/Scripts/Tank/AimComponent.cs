using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour {

    public float turretRotationSpeed;
    public float barrelRotationSpeed;

    public Transform turretTr;
    public Transform barrelTr;
    private Vector3 lookerTurretVec3;
    private Vector3 lookerBarrelVec3;

    // Use this for initialization
    void Start () {
        lookerTurretVec3.x = 0f;
        lookerTurretVec3.y = 0f;
        lookerTurretVec3.z = 0f;

        lookerBarrelVec3.x = 0f;
        lookerBarrelVec3.y = 0f;
        lookerBarrelVec3.z = 0f;
    }
	
    IEnumerator AimAt(Vector3 position)
    {

        lookerTurretVec3.y = position.y;
        lookerBarrelVec3.x = position.x;

        if (lookerBarrelVec3.x > 0f)
        { 
            lookerBarrelVec3.x = 0f;
        }
        else if(lookerBarrelVec3.x < -40f)
        {
            lookerBarrelVec3.x = -40f;
        }

        turretTr.localEulerAngles = lookerTurretVec3;
        barrelTr.localEulerAngles = lookerBarrelVec3;


        yield return null;
    }
    
}
