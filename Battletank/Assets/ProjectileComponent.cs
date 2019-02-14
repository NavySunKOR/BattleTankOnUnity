using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour {

    public float bulletDrop;
    public Vector3 maxBulletSpeed;
    private Rigidbody rb;
    private bool fire;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        if(fire)
        {
            if(maxBulletSpeed.magnitude > rb.velocity.magnitude)
            {
                rb.AddForce(maxBulletSpeed, ForceMode.Acceleration);
            }
            rb.AddForce(Physics.gravity * bulletDrop);
        }
    }


    public void FireObject()
    {
        fire = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
