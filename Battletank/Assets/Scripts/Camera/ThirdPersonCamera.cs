using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    public Transform cameraTr;
    public Transform armTr;

    public bool isInverted;
    [SerializeField]
    private Vector3 distance;
    [SerializeField]
    private float rotationSpeed;
    private float invertedVal;

    private float pitch;
    private float yaw;


	// Use this for initialization
	void Start () {
        SetInverted();
        Vector3 currentToEuler = transform.rotation.eulerAngles;
        yaw = currentToEuler.y;
        pitch = currentToEuler.x;
    }
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Mouse X") * invertedVal * Time.deltaTime;
        float vertical = -Input.GetAxis("Mouse Y") * invertedVal * Time.deltaTime;

        pitch += vertical * rotationSpeed;
        yaw += horizontal * rotationSpeed;
        
        armTr.eulerAngles = Vector3.Lerp(armTr.eulerAngles, new Vector3(pitch, yaw, 0),1f);
    }

    void SetInverted()
    {
        invertedVal = (isInverted) ? -1.0f : 1.0f;
    }
}
