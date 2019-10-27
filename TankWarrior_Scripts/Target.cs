using UnityEngine;
using UnityEngine.UI;

// The Target, to be put on targets.
public class Target : MonoBehaviour
{   // What is damaged.
    public float health = 10f;

    public void HitTarget(float damage)
    {   
        health -= damage;
        
        if (health <= 0)
        {   
            Destroy(gameObject);
            TotalScore.currentHits = TotalScore.currentHits - 1;
            BaseScore.currentHits = BaseScore.currentHits - 1;
        }
    }
}
