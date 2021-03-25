using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanTestScript : MonoBehaviour
{
    public Animator animator;
    public GameObject aim_point;
    public int stealSeconds = 5;
    
    UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator.SetInteger("legs", 5);
        animator.SetInteger("arms", 5);
    }

    //private bool sayingHello;
    // Update is called once per frame
    void Update()
    {
        if (aim_point != null)
        {

            if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
            {

                agent.speed = 2.0f;
                agent.SetDestination(aim_point.transform.position);
                animator.SetInteger("arms", 1);
                animator.SetInteger("legs", 1);
            }

            if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
            {
                agent.speed = 0;

                animator.SetInteger("arms", 5);
                animator.SetInteger("legs", 5);
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            waving_start = StartCoroutine(waving_execute());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            steal_start = StartCoroutine(steal_execute());
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            hang_start = StartCoroutine(hang_execute());
        }
    }

    Coroutine waving_start;
    Coroutine steal_start;
    Coroutine hang_start;

    IEnumerator waving_execute()
    {
        yield return new WaitForSeconds(0);



        animator.SetInteger("arms", 16); //Hello


        yield return new WaitForSeconds(3);

        animator.SetInteger("arms", 1); //Walk
        StopCoroutine(waving_start);
    }


    IEnumerator steal_execute()
    {
        yield return new WaitForSeconds(0);


        animator.SetInteger("legs", 5);
        animator.SetInteger("arms", 22); //Steal


        yield return new WaitForSeconds(stealSeconds);

        animator.SetInteger("legs", 1); //Walk
        animator.SetInteger("arms", 1); //Walk
        StopCoroutine(steal_start);
    }

    IEnumerator hang_execute()
    {
        yield return new WaitForSeconds(0);

        int what_to_choose = UnityEngine.Random.Range(9, 12);

        animator.SetInteger("legs", 1);
        animator.SetInteger("arms", 8); //looking


        yield return new WaitForSeconds(3);

        animator.SetInteger("legs", 1);
        animator.SetInteger("arms", 1); //Walk
        StopCoroutine(waving_start);
    }

}
