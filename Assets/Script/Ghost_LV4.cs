using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_LV4 : MonoBehaviour
{

    private Animator animator;
    public Transform carTransform;
    private float attackRange = 15f;
    private bool IsAttack = false;
    private Coroutine attackCoroutine;
    AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (carTransform == null)
        {
            carTransform = GameObject.FindWithTag("Car")?.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, carTransform.position);

        if (distance <= attackRange)
        {
            if (!IsAttack) // Start Animation Attack
            {
                attackCoroutine = StartCoroutine(Attack());
            }
        }
        else
        {
            if (IsAttack) // Car Far = Stop Animation Attack
            {
                StopAttack();
            }
            Idle();
        }
    }

    void Idle() // Stop Attack
    {
        IsAttack = false;
    }

    IEnumerator Attack()
    {
        IsAttack = true;
        animator.SetBool("IsAttack", true);
        audioManager.PlaySFX(audioManager.Gattack);

        Debug.Log("Attack animation triggered");

        yield return new WaitForSeconds(2f);

        Debug.Log("Attack animation ended");

        StopAttack();
    }

    void StopAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        IsAttack = false;
        animator.SetBool("IsAttack", false);
    }
}
