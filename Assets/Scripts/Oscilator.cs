using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Oscilator : MonoBehaviour
{   
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)]float movementFactor;
    [SerializeField] float movementSpeed = 1f;
    private Rigidbody rb;
    bool movingForward = true;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
        Oscilate();
    }

    void Oscilate()
    {
        
        if (movingForward == true)
        {
            movementFactor = movementFactor + movementSpeed * Time.deltaTime;
            if (movementFactor >= 1f)
            {
                movementFactor = 1f;
                movingForward = false; // Reverse direction
            }
        }
        else
        {
            movementFactor = movementFactor - movementSpeed * Time.deltaTime;
            if (movementFactor <= 0f)
            {
                movementFactor = 0f;
                movingForward = true; // Reverse direction
            }
        }
    }
}
