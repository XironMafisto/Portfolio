using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TargetTank : MonoBehaviour
{
    public float health = 2000f;
    public float maxHealth = 2000f;
    public Slider healthSlider;

    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0f;
    }


    void Update()
    {
        healthSlider.value = health;
        
    }
    public void HitBase(float damage)

    {
        health -= damage;


        if (health <= 0)
        {
            SceneManager.LoadScene(11);
        }
    }
}
