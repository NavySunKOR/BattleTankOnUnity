using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour {

    public float bulletDrop;
    public Vector3 maxBulletSpeed;
    public float blastRadius;
    public int damage;
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
        if(collision.transform.CompareTag("Tank"))
        {
                collision.transform.GetComponent<Tank>().TookDamage(damage);
                Debug.Log("Took damage by directive hit.");
        }
        else
        {
            int enemy = 1 << 10;
            int player = 1 << 11;
            int layer = enemy | player;
            layer = ~layer;
            
            Collider[] collider =  Physics.OverlapSphere(transform.position, blastRadius, layer);
            foreach(Collider coll in collider)
            {
                if(coll.transform.CompareTag("Tank"))
                {
                    coll.GetComponent<Tank>().TookDamage((int)damage / 2);
                    Debug.Log("Took damage by explosive.");
                }
            }
        }
        Destroy(gameObject);
    }
}
