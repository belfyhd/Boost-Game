using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
   
    
    [SerializeField]float speed = 0.1f; // Speed of the object's movement
    private Rigidbody rb;
    private Vector3 movementDirection = Vector3.up; // Initial movement direction (upwards)
    public GameObject obstacle;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Move the object in the current direction
        rb.MovePosition(transform.position + movementDirection * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reverse the direction of movement on collision
        movementDirection = -movementDirection;
    }

}