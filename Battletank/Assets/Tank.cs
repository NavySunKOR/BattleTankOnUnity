using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

    public float tankMaxRange = 100000f;

    private AimComponent aimComponent;

	// Use this for initialization
	void Awake () {
        aimComponent = GetComponent<AimComponent>();
    }

    public void AimAt(Vector3 position)
    {
        aimComponent.StartCoroutine("AimAt",position);
    }
}
