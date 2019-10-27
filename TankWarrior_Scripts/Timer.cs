using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject timerText;
    public float timer;
	// Use this for initialization
	void Start () {
        timer = 60;
	}
	
	// Update is called once per frame
	void Update () {
        timer = timer - 1 * Time.deltaTime;
        timerText.GetComponent<Text>().text = "Time Remaining: " + timer;
        if(timer <= 0f)
        {
            SceneManager.LoadScene(10);
        }
    }
}
