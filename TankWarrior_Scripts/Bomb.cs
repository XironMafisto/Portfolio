using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay = 3;
    float countDown;
    bool hasExploded = false;
    public float radius = 5;
    public float force = 200;
    public float damage = 20;
    public GameObject explodeEffect;
	void Start ()
    {
        countDown = delay;
	}
	
	
	void Update ()
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
	}
    void Explode()
    {
        GameObject explode = Instantiate(explodeEffect, transform.position, transform.rotation);
        Destroy(explode, 2f);
        Collider[] colliderToDestroy = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearBy in colliderToDestroy)
        {
            Target target = nearBy.GetComponent<Target>();
            TargetEnemy enemyTarget = nearBy.GetComponent<TargetEnemy>();

            if (enemyTarget != null)
            {
                enemyTarget.HitEnemy(damage);
            }

            if (target != null)
            {
                target.HitTarget(damage);
            }
        }

        Collider[] colliderToForce = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearBy in colliderToForce)
        {
            Rigidbody rb = nearBy.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        
        Destroy(gameObject);
    }
}
