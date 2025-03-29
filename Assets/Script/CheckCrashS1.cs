using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CarCrashS1 : MonoBehaviour
{
    public static CarCrashS1 instance;
    public static int objectCount = 0;  // for Count Crash Ghost
    public int targetCount = 20;  // Goal Crash Ghost
    public bool isDead = false;
    AudioManager audioManager;
    public PrometeoCarController carController;
    public CoinManager cm;


    void OnCollisionEnter(Collision collision)
    {
    
        if (collision.gameObject.CompareTag("Ghost"))  // Ghost Normal Tag "Ghost"
        {
            Collider ghostCollider = collision.gameObject.GetComponent<Collider>();
            if (ghostCollider != null)
            {
                ghostCollider.enabled = false;
            }
            // Animation Dead
            Animator ghostAnimator = collision.gameObject.GetComponent<Animator>();
            if (ghostAnimator != null)
            {
                ghostAnimator.SetTrigger("death");  // Start Animation Dead
                audioManager.PlaySFX(audioManager.Crash);
            }

            // count 
            objectCount++;
            cm.coinCount= objectCount;

            // Destroy Ghost 
            Destroy(collision.gameObject, 1.5f); // Time Ghost Disapeer

            Debug.Log("ชนแล้ว: " + objectCount + " / " + targetCount + " ครั้ง"); //Debug

            // Goal
            if (objectCount >= targetCount)
            {
                Debug.Log("ครบ 20 ครั้ง กำลังเปลี่ยนฉาก...");
                objectCount = 0;  // Reset 0 
                LoadNextScene();  // go to scence 2
            }

        }
    }
    void Awake()
    {
        instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public static void RespawnCar()
    {
        objectCount = 0;  // Reset 0
        SceneManager.LoadScene("Level1"); // re game
        if (instance.carController != null)
        {
            instance.carController.enabled = true;  // Open Car Script
        }
    }

    void LoadNextScene()
    {
        // Go to Next Level 2
        Debug.Log("Level2");
        objectCount = 0; // reset0
        SceneManager.LoadScene("Level2");  // Goto Level2

        if (carController != null)
        {
            carController.enabled = true;  //Open Car Script
        }
    }


    // Check Dead with BoxAttack
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("BoxAttack"))
        {
            isDead = true;
            audioManager.PlaySFX(audioManager.Dead);
            StopGame();
            
        }
        //if (other.gameObject.CompareTag("Ghost"))
        //{
          //  if (cm != null)
            //{
              //  cm.coinCount++;  // เพิ่มคะแนน
               // Debug.Log("เหรียญที่เก็บได้: " + cm.coinCount);
            //}
            //else
            //{
              //  Debug.LogError("ไม่พบ CoinManager ใน Scene");
            //}
        //}
    }


    // Stop Game When Dead Need to Edit***
    void StopGame()
    {
        Debug.Log("เกมหยุด เพราะตาย!");
        Time.timeScale = 0f;  // Stop Time

        if (carController != null)
        {
            carController.enabled = false;  //Close Car Script
        }

    }

}
