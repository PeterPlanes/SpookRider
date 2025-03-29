using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CarCrashS2 : MonoBehaviour
{
    public static CarCrashS2 instance;
    public static int objectCountS2 = 0;
    public int targetCountS2 = 30;
    public bool isDead = false;
    AudioManager audioManager;
    public PrometeoCarController carController;
    public CoinManager cm;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            Collider ghostCollider = collision.gameObject.GetComponent<Collider>();
            if (ghostCollider != null)
            {
                ghostCollider.enabled = false;
            }

            Animator ghostAnimator = collision.gameObject.GetComponent<Animator>();
            if (ghostAnimator != null)
            {
                ghostAnimator.SetTrigger("death");
                audioManager.PlaySFX(audioManager.Crash);
            }

            objectCountS2++;
            cm.coinCount = objectCountS2;

            Destroy(collision.gameObject, 1.5f);

            Debug.Log("ชนแล้ว: " + objectCountS2 + " / " + targetCountS2);

            // Goal
            if (objectCountS2 >= targetCountS2)
            {
                Debug.Log("ครบ 30 ครั้ง กำลังเปลี่ยนฉาก...");

                if (carController != null)
                {
                    carController.enabled = false;  // Cloes Script Car When Win
                }

            }
        }
    }

    void Awake()
    {
        instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    public static void RespawnCar() //Reset
    {
        objectCountS2 = 0;  // Reset ObjectsCount 
        SceneManager.LoadScene("Level2"); // Reload Scene

        if (instance.carController != null)
        {
            instance.carController.enabled = true;  // Open Script Car
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
         //   if (cm != null)
           // {
             //   cm.coinCount++;  // เพิ่มคะแนน
               // Debug.Log("เหรียญที่เก็บได้: " + cm.coinCount);
            //}
            //else
            //{
             //   Debug.LogError("ไม่พบ CoinManager ใน Scene");
            //}
        //}
    }


    // Stop Game When Dead GameOver
    void StopGame()
    {
        Debug.Log("เกมหยุด เพราะตาย!");
        Time.timeScale = 0f;  // Stop Time
        if (carController != null)
        {
            carController.enabled = false;  // Cloes Script Car
        }

    }
}