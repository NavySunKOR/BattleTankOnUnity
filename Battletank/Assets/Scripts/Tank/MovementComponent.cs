using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour {
    public Transform tankTr;
    public float tankSpeed;
    [Tooltip("In km/h")]
    public float maximumSpeed;

    public float rotationSpeed;

    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    private Vector3 maximumSpeedVector;
    private bool isDead;
    private bool isRepairing;


    // Use this for initialization
    void Start () {
        rb = transform.GetComponent<Rigidbody>();
        isDead = false;
        isRepairing = false;

    }
	
	// Update is called once per frame
	void Update () {
        if(!isDead && !isRepairing)
        {
            maximumSpeedVector = tankTr.forward * maximumSpeed;
            Vector3 speedInput = tankTr.forward * vertical * tankSpeed;
            if (maximumSpeedVector.magnitude > (rb.velocity + speedInput).magnitude)
                rb.AddForce(speedInput, ForceMode.Acceleration);

            tankTr.Rotate(tankTr.up * rotationSpeed * horizontal * Time.deltaTime);
        }
	}

    public void Move(float vertical)
    {
        this.vertical = vertical;
    }

    public void Rotate(float horizontal)
    {
        this.horizontal = horizontal;
    }

    public void DisableFunction()
    {
        isDead = true;
    }

    public void SetRepairFunction()
    {
        isRepairing = !isRepairing;
    }
}
