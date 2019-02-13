using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour {

    public float turretRotationSpeed;
    public float barrelRotationSpeed;

    public Transform turretTr;
    public Transform barrelTr;
    private Vector3 pos;
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

        pos = position;
        Quaternion lookRotation = Quaternion.LookRotation(position - turretTr.position);
        Vector3 turretVec3 = lookRotation.eulerAngles;
        Vector3 barrelVec3 = lookRotation.eulerAngles;

        lookerTurretVec3.y = turretVec3.y;
        lookerBarrelVec3.x = barrelVec3.x;
        
        if(lookerBarrelVec3.x > 0f && lookerBarrelVec3.x < 320f)
        { 
            lookerBarrelVec3.x = (lookerBarrelVec3.x > 0f && lookerBarrelVec3.x < 180f) ? 0f :lookerBarrelVec3.x = 320f;
        }

        turretTr.localEulerAngles = lookerTurretVec3;
        barrelTr.localEulerAngles = lookerBarrelVec3;


        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pos, 1f);
    }
}
