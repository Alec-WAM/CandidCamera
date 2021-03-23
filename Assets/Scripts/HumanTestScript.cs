using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanTestScript : MonoBehaviour
{
    public Animator animator;
    public int stealSeconds = 5;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetInteger("legs", 1);
        animator.SetInteger("arms", 1);
    }

    //private bool sayingHello;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            waving_start = StartCoroutine(waving_execute());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            steal_start = StartCoroutine(steal_execute());
        }
    }

    Coroutine waving_start;
    Coroutine steal_start;

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

}
