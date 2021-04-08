using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWanderTraining : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public float moveSpeed = 2.0f;
    public List<GameObject> aim_points;
    public GameObject aim_point;
    public float aimDistance = 0.45f; //How clos the agent needs to be to finish aim

    public bool AtAimPoint => Vector3.Distance(transform.position, aim_point.transform.position) < aimDistance;

    // Start is called before the first frame update
    void Start()
    {
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

            var offset = aim_point.transform.position - transform.position;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, offset, 1.0f, 0.0f);
            Quaternion lookAtRotation = Quaternion.LookRotation(newDirection);
            Quaternion lookAtRotation_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, lookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = lookAtRotation_Y;

            offset = offset.normalized * moveSpeed;
            characterController.Move(offset * Time.deltaTime);



            animator.SetInteger("arms", 1);
            animator.SetInteger("legs", 1);
        }
        else
        {
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
