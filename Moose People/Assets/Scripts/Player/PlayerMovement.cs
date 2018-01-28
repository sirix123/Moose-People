using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; //player speed

    Vector3 movement; //store player movement vector
    Rigidbody playerRigidbody; //reference rigidbody
    int floorMask; //create floor mask for the raycast
    float camRayLength = 100f; //ray from camera

    void Awake()
    {
        //create layer mask for floor layer
        floorMask = LayerMask.GetMask("Floor");

        //references
        playerRigidbody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        //sttore data
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //move around
        Move(h, v);

        //turn
        Turning();
    }

    void Move(float h, float v)
    {
        // set movement vector based on input
        movement.Set(h, 0f, v);

        //normalise movement vector
        movement = movement.normalized * speed * Time.deltaTime;

        //move player to current pos + movement
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }
}
