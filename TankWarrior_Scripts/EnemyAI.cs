using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public Transform player;
    public float playerDistance;
    public float rotSpeed = 5;
    public float moveStart;
    public float moveSpeed = 5;
    public float enemyDamage = 1f;
    public ParticleSystem flameThrower;
    public GameObject flameHit;
    public AudioSource hurtSound;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        playerDistance = Vector3.Distance (player.position, transform.position);
        if (playerDistance < 20f)
        {
            LookAtPlayer();
        }

        if (playerDistance < 18f)
        {
            EnemyAttack();
            if (playerDistance > 2f)
            {
                MoveToPlayer();
            }
            
        }

    }
    void LookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);

    }

    void MoveToPlayer()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

    }
    void EnemyAttack()
    {
        flameThrower.Play();
        RaycastHit hitPlayer;
        if (Physics.Raycast(transform.position, transform.forward, out hitPlayer))
        {

            if(hitPlayer.collider.gameObject.tag == "Player")
            {
                hurtSound.Play();
                hitPlayer.collider.gameObject.GetComponent<HealthBar>().currentHealth -= enemyDamage;
                GameObject hitHere = Instantiate(flameHit, hitPlayer.point, Quaternion.LookRotation(hitPlayer.normal));
                Destroy(hitHere, 0.5f);
                
            }

        }
    }
}
