using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class MoveMeThisWay : MonoBehaviour
{
    // Set up a vector for the movement and set to 0.
    private Vector3 moveThisWay = Vector3.zero;
    // Set a speed .
    public float setSpeed = 5f;
    // Apply a force.
    public float jumpingForce = 20f;
    // Added another force.
    public float downforce = 10f;
    // Make the rotation speed separate.
    public float rotSpeed = 10;
    // Add a jump sound.
    public AudioSource jumpSound;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
    void FixedUpdate ()
      
    {   
    CharacterController moveMe = gameObject.GetComponent<CharacterController>();
        
        if (moveMe.isGrounded)
        {
            //                   rotation, left and right                forward and backward
            moveThisWay = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            //                                 Move in this direction.
            moveThisWay = (Input.GetAxis("Vertical") * setSpeed * transform.forward) + (Input.GetAxis("Mouse Y") * setSpeed * transform.forward);
            //moveThisWay = Input.GetAxis("Mouse Y") * setSpeed * transform.forward;

            // Rotate around this.
            // With the mouse.
            // transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotSpeed, 0));
            // Using left and right.
            // transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * rotSpeed, 0));
            // Now set a vilocity.

            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane rayPlane = new Plane(Vector3.up, Vector3.zero);
            float raySize;
            if (rayPlane.Raycast(camRay, out raySize))
            {
                Vector3 targetPoint = camRay.GetPoint(raySize);
                transform.LookAt(new Vector3(targetPoint.x, transform.position.y, targetPoint.z));

            }
            moveThisWay *= setSpeed;     
            
            // Did player try to jump?
            if (Input.GetButtonDown("Jump"))
            {   // If yes then apply force.
                moveThisWay.y = jumpingForce;
                // Play jump sound.
                jumpSound.Play();
            }
            if (Input.GetButtonDown("Fire2"))
            {   
                moveThisWay.y = jumpingForce;
                jumpSound.Play();
            }
        }
        // How quickly the player falls.
        moveThisWay.y -= downforce * setSpeed * Time.deltaTime;
        // Set this to a constant.
        moveMe.Move (moveThisWay * Time.deltaTime);        
    }
}
