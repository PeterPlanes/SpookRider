using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ghost_attackLV2 : MonoBehaviour
{
    public float speed2 = 3f;
    public Animator animator2;
    public Transform carTransform2;
    public float attackRange2 = 3f;
    public bool isAttack = false;

    void Start()
    {
        animator2 = GetComponent<Animator>();

        if (carTransform2 == null)
        {
            carTransform2 = GameObject.FindWithTag("Car")?.transform;
        }
    }

    void Update()
    {
        if (isAttack) return;

        float distance = Vector3.Distance(transform.position, carTransform2.position);
        Debug.Log("Distance to Car: " + distance);

        if (distance <= attackRange2)
        {
            Debug.Log("Car is in attack range!");
            isAttack = true;
            StartCoroutine(Attack1());
            return;
        }

        //transform.position += Vector3.left * speed2 * Time.deltaTime;
    }

    IEnumerator Attack1()
    {
        Debug.Log("Setting attack trigger");
        animator2.SetTrigger("IsAttack");

        yield return new WaitUntil(() => animator2.GetCurrentAnimatorStateInfo(0).IsName("Attack"));

        Debug.Log("Playing Attack animation...");

        yield return new WaitWhile(() => animator2.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

        Debug.Log("Attack animation ended, loading Level1");
        SceneManager.LoadScene("Level1");
    }
}
