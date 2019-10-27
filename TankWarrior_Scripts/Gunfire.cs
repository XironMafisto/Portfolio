using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is my shooting device as well as my firing mechanism.
public class Gunfire : MonoBehaviour
{
    // Give the device a set range.
    public float range;
    // Now set the damage.
    public float damage = 10;
    // To show when fire button is pressed.
    public ParticleSystem gunSmoke;
    // To show where the prjectile hit.
    public GameObject hitEffect;
    // Add a sound.
    public AudioSource shootSound;
	void Update () {
		// Use the default firing button, (left mouse click for me).
        if (Input.GetButtonDown("Fire1"))
        {   // Function(s) activated when button is pressed.
            Shoot();
            shootSound.Play();
        }
	}
    void Shoot ()
    {
        // A simple fire animation using particals.
        gunSmoke.Play();

        // Raycast for targeting purposes.
        RaycastHit rayHit;
        //                      Player            Player forward   Going out to range
        if (Physics.Raycast(transform.position, transform.forward, out rayHit))
            range = rayHit.distance;
        {
            // Set up target.            What the ray hit.
            Target target = rayHit.transform.GetComponent<Target>();
            TargetEnemy enemyTarget = rayHit.transform.GetComponent<TargetEnemy>();
            TargetBase baseTarget = rayHit.transform.GetComponent<TargetBase>();
            TargetTank tankTarget = rayHit.transform.GetComponent<TargetTank>();

            //GameObject hitHere = Instantiate(hitEffect, transform.position + transform.forward * range, Quaternion.LookRotation(rayHit.point));
            GameObject hitHere = Instantiate(hitEffect, rayHit.point , Quaternion.LookRotation(rayHit.normal));
            Destroy(hitHere, 0.5f);

            Debug.DrawLine(transform.position, transform.position + transform.forward * range, Color.red);
            // Show me what the ray hit.
            // Debug.Log(rayHit.transform.name);
            if (enemyTarget != null)
            {   // Target takes damage.
                enemyTarget.HitEnemy(damage);
            }
            // Only if targe is not zero, a valid targe is in range.
            if (target != null)
            {   // Target takes damage.
                target.HitTarget(damage);
            }
            if(baseTarget != null)
            {   
                baseTarget.HitBase(damage);
            }
            if (tankTarget != null)
            {
                tankTarget.HitBase(damage);
            }
        }
    }
    
}
