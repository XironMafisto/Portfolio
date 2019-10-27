using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseScore : MonoBehaviour {
    public GameObject hitText;
    public static int currentHits = 1;
    public int sceneLoad = 6;

    void Update()
    {
        if (currentHits == 0)
        {
            SceneManager.LoadScene(sceneLoad);
            currentHits = 1;
        }
    }
}
