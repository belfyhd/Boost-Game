using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHit : MonoBehaviour
{
    // Reference to the Renderer component
    private Renderer objectRenderer;
    public AudioSource audioBoost;

    // Initialize the Renderer component
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        audioBoost = GetComponent<AudioSource>();
    }

    // Detect collisions
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object has collided with a player or any other object
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!audioBoost.isPlaying)
            {
            audioBoost.Play();
            }

        }
    }
}