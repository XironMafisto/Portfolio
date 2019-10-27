using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour {

    public float throwForce = 5;
    public GameObject bombPrefab;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            BombThrow();
        }

    }
    void BombThrow()
    {
        GameObject bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.up * throwForce, ForceMode.VelocityChange);
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
