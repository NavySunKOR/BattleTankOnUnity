using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITankController : MonoBehaviour {

    public Transform playerTr;
    public float trackingDistance;
    public float fireDistance;
    private Tank tank;
    private NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        tank = GetComponent<Tank>();
    }

    // Update is called once per frame
    void Update () {
        MoveTowards();
        FireTowards();
    }

    void MoveTowards()
    {
        if(Vector3.Distance(playerTr.position,transform.position) > trackingDistance)
        {
            //Debug.Log("Im moving");
            agent.isStopped = false;
            agent.destination = playerTr.position;
        }
        else
        {
           agent.isStopped = true;
        }
    }
    
    void FireTowards()
    {
        Ray ray = new Ray(transform.position, (playerTr.position - transform.position).normalized);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, fireDistance,1<<11))
        {
            Vector3 lookEuler = Quaternion.LookRotation((hit.point - transform.position).normalized).eulerAngles;
            tank.AimAt(lookEuler);
            tank.Fire();
        }
  
    }
}
