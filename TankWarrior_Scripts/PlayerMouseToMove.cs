using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMouseToMove : MonoBehaviour {

    NavMeshAgent navAgent;
	void Start ()
    {
        navAgent = GetComponent<NavMeshAgent>();

	}

    public void MoveToPoint (Vector3 point)
    {
        navAgent.SetDestination(point);
    }

	void Update ()
    {
		
	}
}
