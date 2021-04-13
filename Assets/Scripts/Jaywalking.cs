using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaywalking : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public float moveSpeed = 2.0f;
    public List<GameObject> aim_areas;
    public float aimDistance = 0.45f; //How close the agent needs to be to finish aim

    private Vector3 aim_point;
    private bool AtAimPoint => Vector3.Distance(transform.position, aim_point) < aimDistance;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetInteger("legs", 5);     // idle
        animator.SetInteger("arms", 5);     // idle
    }

    // Update is called once per frame
    void Update()
    {
        if (aim_point == null)
            return;

        if (!AtAimPoint)
        {
            var offset = aim_point - transform.position;

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

            if (aim_areas.Count > 1)
            {
                int index = Random.Range(0, aim_areas.Count);
                Transform Spawn = aim_areas[index].transform;
                MeshFilter b = Spawn.GetComponent<MeshFilter>();
                Vector3 mult = Spawn.localScale;

                // select a random location in the spawn area and place the object there
                Vector3 newAimPoint = Spawn.position + new Vector3(
                    Random.Range(b.mesh.bounds.min.x, b.mesh.bounds.max.x) * mult.x,
                    0,
                    Random.Range(b.mesh.bounds.min.z, b.mesh.bounds.max.z) * mult.z);

                aim_point = newAimPoint;
            }
            else aim_point = Vector3.zero;
        }
    }
}
