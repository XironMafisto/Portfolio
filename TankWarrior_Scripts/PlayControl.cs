using UnityEngine;


public class PlayControl : MonoBehaviour {
    Camera cam1;
    public LayerMask moveMask;
    PlayerMouseToMove move1;
    public float steppingCount = 5.0f;

	void Start ()
    {
        cam1 = Camera.main;
        move1 = GetComponent<PlayerMouseToMove>();
    }

    void Update()
    {  
            // Set the forward velocity using control or keyboard.
        //gameObject.GetComponent<Rigidbody>().velocity = Input.GetAxis("Vertical") * steppingCount * transform.forward;


            // Turn the Player using conltroler or keyboard. 
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * steppingCount, 0));

            // Turn the player using mouse.
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * steppingCount, 0));

            // Click left mouse button to move.
        if (Input.GetMouseButtonDown(0))
        {
            Ray mRay = cam1.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(mRay, out rayHit, 100, moveMask))
            {
                    //  Log to check what was hit by the ray.
                // Debug.Log("Hit " + rayHit.collider.name + " " + rayHit.point);
                    //  Move player to what the ray hit.
                move1.MoveToPoint(rayHit.point);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray mRay = cam1.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(mRay, out rayHit, 100))
            {
                // Was Object interactable?
                // If so, set focus.
            }
        }
    }
}
