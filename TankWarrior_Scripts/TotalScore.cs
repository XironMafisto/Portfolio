using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TotalScore : MonoBehaviour {
    public GameObject hitText;
    public static int currentHits = 10;
    public int sceneToLoad = 3;

    void Update()
    {
        hitText.GetComponent<Text>().text = "Remaining Targets: " + currentHits;
        if (currentHits == 0)
        {
            currentHits = 10;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
