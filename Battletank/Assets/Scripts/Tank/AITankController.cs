using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITankController : MonoBehaviour {

    public Transform playerTr;
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
        if(Vector3.Distance(playerTr.position,transform.position) > 300)
        {
            Debug.Log("Im moving");
            agent.SetDestination(playerTr.position);
        }
    }
    
    void FireTowards()
    {
        Ray ray = new Ray(transform.position, (playerTr.position - transform.position).normalized);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,350,~(1<<8)))
        {
            Debug.Log("Found you!");
            Vector3 lookEuler = Quaternion.LookRotation(hit.point - transform.position).eulerAngles;
            tank.AimAt(lookEuler);
            tank.FirePrimary();
        }


    }
}
