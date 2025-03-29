using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ghost_Attack : MonoBehaviour
{
    public float speed = 3f;
    public Transform leftPoint, rightPoint;
    private bool movingRight = true;
    private Animator animator;
    public Transform carTransform;
    private float attackRange = 15f;
    private bool isAttacking = false;
    private Coroutine attackCoroutine; 
    AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
            if (!isAttacking) // Start Animation Attack
            {
                attackCoroutine = StartCoroutine(Attack());
            }
        }
       // else if (CarCrashS2.instance != null && CarCrashS2.objectCountS2 >= CarCrashS2.instance.targetCountS2)
        //{
          //  if (isAttacking) // Car Far = Stop Animation Attack
           // {
            //    StopAttack();
            //}
           // MovePatrol();
       // }
 
        else
        {
            if (isAttacking) // Car Far = Stop Animation Attack
            {
                StopAttack();
            }
            MovePatrol();
        }
    }

    void MovePatrol()
    {
        if (isAttacking) return; // Stop Walk when Attack

        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            animator.SetBool("WalkR", true);
            animator.SetBool("WalkL", false);

            if (transform.position.x >= rightPoint.position.x)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            animator.SetBool("WalkR", false);
            animator.SetBool("WalkL", true);

            if (transform.position.x <= leftPoint.position.x)
                movingRight = true;
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttack", true);
        animator.SetBool("WalkR", false);
        animator.SetBool("WalkL", false);
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

        isAttacking = false;
        animator.SetBool("isAttack", false);
    }
}
