using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    public Transform carTransform;  
    private float detectionRange = 20f;  // Ghost Can see
    public float moveSpeed = 5f;  // speed Run Ghost
    private Animator animator;  // Animation Ghost
    public int ghostCollisionCount = 0;

    private bool isChasing = false;

    void Start()
    {
        
        animator = GetComponent<Animator>();

        ghostCollisionCount = 0;
        Debug.Log("Ghost collision count reset at scene start!");
    }

    void Update()
    {
        // Ghost between Car
        float distanceToCar = Vector3.Distance(transform.position, carTransform.position);

        // Ghost go to Car
        Vector3 directionToCar = (carTransform.position - transform.position).normalized;

        // Car Passaway Ghost
        float relativeZ = transform.InverseTransformPoint(carTransform.position).z;

        // Debug
        Debug.Log("Distance to Car: " + distanceToCar + " | Relative Z: " + relativeZ + " | Is Chasing: " + isChasing);

        // Car Passaway Ghost = Ghost Stop
        if (distanceToCar > detectionRange || relativeZ < 0)
        {
            isChasing = false;
            animator.SetBool("IsRunning", false);
            return;
        }

        // If Car near Ghost
        isChasing = true;
        transform.position = Vector3.MoveTowards(transform.position, carTransform.position, moveSpeed * Time.deltaTime);
        animator.SetBool("IsRunning", true);  // Bool "IsRunning"  Star Animtion Run
    }
}
