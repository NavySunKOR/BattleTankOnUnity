using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

    public float tankMaxRange = 100000f;

    private AimComponent aimComponent;
    private MovementComponent movementComponent;

	// Use this for initialization
	void Awake () {
        aimComponent = GetComponent<AimComponent>();
        movementComponent = GetComponent<MovementComponent>();
    }

    public void AimAt(Vector3 position)
    {
        aimComponent.StartCoroutine("AimAt",position);
    }

    public void FirePrimary()
    {

    }

    public void Move(float vertical)
    {
        movementComponent.Move(vertical);
    }

    public void Rotate(float horizontal)
    {
        movementComponent.Rotate(horizontal);
    }
}
