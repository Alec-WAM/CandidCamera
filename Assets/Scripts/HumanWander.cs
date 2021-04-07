using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWander : MonoBehaviour
{
    public Animator animator;
    public List<GameObject> aim_points;
    public GameObject aim_point;
    public float aimDistance = 0.45f; //How clos the agent needs to be to finish aim

    UnityEngine.AI.NavMeshAgent agent;

    public bool AtAimPoint => Vector3.Distance(transform.position, aim_point.transform.position) < aimDistance;

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
        if (aim_point == null)
            return;

        if (!AtAimPoint)
        {
            agent.speed = 2.0f;
            agent.SetDestination(aim_point.transform.position);
            animator.SetInteger("arms", 1);
            animator.SetInteger("legs", 1);
        }
        else
        {
            agent.speed = 0;
            animator.SetInteger("arms", 5);
            animator.SetInteger("legs", 5);

            if (aim_points.Count > 1)
            {
                int index = Random.Range(0, aim_points.Count);

                GameObject newAimPoint = aim_points[index];
                aim_point = newAimPoint;
            }
            else aim_point = null;


        }
    }
}
