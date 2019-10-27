using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public float currentHealth = 100;
    public float maxHealth = 100f;
    public Slider healthSlider;
    public int sceneToLoad = 7;

    void Start ()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0f;
	}
	
	
	void Update ()
    {
        healthSlider.value = currentHealth;
        if (currentHealth < 0)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void HealthLoss(float amount)
    {
        currentHealth -= amount;
    }
    

    // public void HealthGain(float amount)
    // {
    //    if (currentHealth >= maxHealth)
    //       return;

    //   currentHealth += amount;
    //}

}
