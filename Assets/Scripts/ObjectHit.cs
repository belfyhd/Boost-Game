using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectHit : MonoBehaviour

{
    // Reference to the Renderer component 
    private Renderer objectRenderer;
    AudioSource audioSource;

    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip finishAudio;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem finishParticle;
    [SerializeField] float levelLoadDelay = 2f;
    //[SerializeField] float AudioDelay = 1f;
    //[SerializeField] public Movement Mv;
    Rigidbody rb;


    bool isTransitioning = false;
    bool collisionDisabled = false;

    // Initialize the Renderer component
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        
    }
    void Update()
    {
        LoadNextLevelCheatCode();
        DisableCollisionCheatCode();
    }

    // Detect collisions
    private void OnCollisionEnter(Collision collision)

    {
            if (isTransitioning  || collisionDisabled){return;}
                {
                    switch(collision.gameObject.tag)
                    {
                        case "Finish":
                            //LoadNextLevel();
                            WinSequence();
                        break;
                        case "Obstacle":
                            //ReloadLevel();
                            LooseSequence();
                        break;
                    }
                    
                }
               
            }

    


            //SCENE MANAGER
            private void LoadNextLevel()
            {
                audioSource.Stop();
                int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
                int nextSceneIndex = currentLevelIndex + 1;
                if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
                    {
                        nextSceneIndex = 0;
                    }
                SceneManager.LoadScene(nextSceneIndex);
            }

             private void ReloadLevel()
            {
                audioSource.Stop();
                int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentLevelIndex);

            }


            
            
            


            //WIN OR LOOSE MANAGER N 
            private void WinSequence()
            {
                isTransitioning= true;
                audioSource.Stop();
                finishParticle.Play();
                audioSource.PlayOneShot(finishAudio);
                GetComponent<Movement>().enabled = false;
                Invoke("LoadNextLevel", levelLoadDelay);
                
            }
            private void LooseSequence()
            {
                isTransitioning = true;
                audioSource.Stop();
                crashParticle.Play();
                audioSource.PlayOneShot(crashAudio);
                GetComponent<Movement>().enabled = false; 
                Invoke("ReloadLevel", levelLoadDelay);
            }

            //Cheat Keys
            void LoadNextLevelCheatCode()
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    LoadNextLevel();
                }
            }
            void DisableCollisionCheatCode()
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    collisionDisabled = !collisionDisabled; //toggles collision on and off
                }
            }
            
    
}