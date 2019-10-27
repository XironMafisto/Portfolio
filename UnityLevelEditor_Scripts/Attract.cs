using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour
{
    public static List<Attract> Attractors;
    public Rigidbody rb;
    public float Grav = 667.0f;

    private void OnEnable()
    {
        if(Attractors == null)
        {
            Attractors = new List<Attract>();
            Attractors.Add(this);
        }        
    }

    private void OnDisable()
    {
        Attractors.Remove(this);
    }



    void FixedUpdate()
    {
        foreach(Attract attractor in Attractors)
        {
            if(attractor != this)
            {
                Attraction(attractor);
            }
        }
    }

    void Attraction(Attract objectAttracted)
    {
        Rigidbody rbAttracter = objectAttracted.rb;
        Vector3 direction = rb.position - rbAttracter.position;

        float distance = direction.magnitude;

        if(distance == 0)
        {
            return;
        }

        float forceMag = (Grav * rb.mass * rbAttracter.mass) / Mathf.Pow(distance, 2);

        Vector3 force = direction.normalized * forceMag;
        rbAttracter.AddForce(force);
    }
}
