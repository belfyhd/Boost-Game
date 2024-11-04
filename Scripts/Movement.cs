using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
        [SerializeField] float thrust = 1000f;
        [SerializeField] float rotationthrust = 1f;
        public Rigidbody rb;
        [SerializeField] AudioClip audioBoost;
        AudioSource audioSource;
        [SerializeField] ParticleSystem leftBooster;
        [SerializeField] ParticleSystem mainBooster;
        [SerializeField] ParticleSystem rightBooster;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        DoRotate();
    }
    // Movement and movement audio
    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StopThrusting()
    {
        rb.AddRelativeForce(0, 0, 0);
        ThrustingAudioStop();
    }

    private void ThrustingAudioStop()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(0, thrust * Time.deltaTime, 0);
        ThrustingAudio();
    }

    void ThrustingAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioBoost);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    void DoRotate()
    {
        RotateRight();
        RotateLeft();
        RotateRightAndLeftCancel();
    }

    void RotateRightAndLeftCancel()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            rb.freezeRotation = true;
            transform.Rotate(0, 0, 0);
            rb.freezeRotation = false;
        }
    }

    void RotateLeft()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            if (!rightBooster.isPlaying)
            {
                rightBooster.Play();
                audioSource.Pause();
            }
            rb.freezeRotation = true;
            transform.Rotate(0, 0, 0.6f * rotationthrust);
            rb.freezeRotation = false;
        }
        else
        {
            //audioSource.Pause();
            rightBooster.Stop();
        }
    }

    private void RotateRight()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {

            if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
            rb.freezeRotation = true;
            transform.Rotate(0, 0, -0.6f * rotationthrust);
            rb.freezeRotation = false;
        }
        else
        {
            //audioSource.Pause();
            leftBooster.Stop();
        }
    }
}
