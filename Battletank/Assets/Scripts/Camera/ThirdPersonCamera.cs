using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    public Transform cameraTr;
    public Transform armTr;

    public bool isInverted;
    public Vector3 cameraOffset;
    public float pitch;
    public float yaw;
    public LayerMask collisionMask;
    [SerializeField]
    private float rotationSpeed;
    private float invertedVal;
    private Vector3 targetPosition;




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

        targetPosition = armTr.position + armTr.forward * cameraOffset.z + armTr.up * cameraOffset.y + armTr.right * cameraOffset.x;
        CameraCollision();

    }

    void SetInverted()
    {
        invertedVal = (isInverted) ? -1.0f : 1.0f;
    }

    void CameraCollision()
    {
        RaycastHit hit;

        if(Physics.Linecast(cameraTr.position, armTr.position, out hit, collisionMask))
        {
            cameraTr.position = hit.point;
        }
        else
        {
            cameraTr.position = targetPosition;
        }
        

    }
}
