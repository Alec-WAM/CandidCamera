using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script uses the navmesh of a scene on a bot to 
// make the bot navigate the navmesh and go to random places (aim points)
public class HumanWander : MonoBehaviour
{
    public Animator animator;
    public List<GameObject> aim_points;
    public GameObject aim_point;
    public float aimDistance = 0.45f; // How close the agent needs to be to finish aim

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
            agent.SetDestination(aim_point.transform.position);     // set the bot to navigate the navmesh
            animator.SetInteger("arms", 1);     // walk
            animator.SetInteger("legs", 1);     // walk
        }
        else
        {
            agent.speed = 0;
            animator.SetInteger("arms", 5);     // idle
            animator.SetInteger("legs", 5);     // idle

            // set new aim point
            if (aim_points.Count > 1)
            {
                int index = Random.Range(0, aim_points.Count);      // randomize between aim points

                GameObject newAimPoint = aim_points[index];
                aim_point = newAimPoint;
            }
            else aim_point = null;


        }
    }
}
