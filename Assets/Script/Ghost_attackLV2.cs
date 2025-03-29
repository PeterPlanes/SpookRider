using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ghost_AttackLV2 : MonoBehaviour
{
    public float speed = 3f;
    private Animator animator;
    public Transform carTransform;
    private float attackRange = 5.8f;
    private bool IsAttack = false;
    private Coroutine attackCoroutine; 

    void Start()
    {
        animator = GetComponent<Animator>();
        if (carTransform == null)
        {
            carTransform = GameObject.FindWithTag("Car")?.transform;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, carTransform.position);

        if (distance <= attackRange)
        {
            if (!IsAttack) 
            {
                attackCoroutine = StartCoroutine(Attack());
            }
        }
        else
        {
            if (IsAttack) 
            {
                StopAttack();
            }
            Idle();
        }
    }

    void Idle()
    {
        IsAttack = false;
    }

    IEnumerator Attack()
    {
        IsAttack = true;
        animator.SetBool("IsAttack", true);

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
