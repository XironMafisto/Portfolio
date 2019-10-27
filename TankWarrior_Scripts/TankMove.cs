using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float moveSpeed = 10;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);

    }
}
