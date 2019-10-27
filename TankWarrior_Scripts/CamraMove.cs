using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamraMove : MonoBehaviour {
    // The player transform.
    public Transform target;
    // Another point.
    public Vector3 offset;
    [Range(0.01f, 1.0f)]
    // Variable for camra feel.
    public float smoothMove = 0.2f;
    // Set mouse to orbit.    
    public bool lookAtPlayer = false;
    public bool rotateAroundTarget = true;
    public float rotateSpeed = 5.0f;

    void Start()
    {   // The differance between the two positions.
        offset = transform.position - target.position;
    }   	
	void LateUpdate ()
    {   
        if(rotateAroundTarget)
        {
            Quaternion camRotate = Quaternion.AngleAxis((Input.GetAxis("Mouse X") * rotateSpeed) + (Input.GetAxis("Horizontal") * rotateSpeed), Vector3.up);
            offset = camRotate * offset;
        }
               // Place the camra at end of offset.
        Vector3 camPosition = target.position + offset;
        // Used for a better feel.
        transform.position = Vector3.Slerp(transform.position, camPosition, smoothMove);

        if (lookAtPlayer || rotateAroundTarget)
        {
            transform.LookAt(target.position);
        }
    }    
}
