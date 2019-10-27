using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crosshair : MonoBehaviour {
    public bool canSeeMouse = false;
	// Use this for initialization
	void Start ()
    {   // Disable the mouse cursor's visibility.
        Cursor.visible = canSeeMouse;
	}
	
	// Update is called once per frame
	void Update ()
    {   // Set crosshair's position to the mouse's position.
        transform.position = Input.mousePosition;
	}
}
