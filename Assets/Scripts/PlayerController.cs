using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script allows user input on an agent via rotation and forward and backward movement
public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Animator animator;
    public float speed = 6;
    public float rotationSpeed = 180.0f;
    private bool walking;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetInteger("legs", 5);     // idle
        animator.SetInteger("arms", 5);     // idle
    }

    void Update()
    {
        Move();

        if (walking)
        {
            animator.SetInteger("legs", 1);     // walk
            animator.SetInteger("arms", 1);     // walk
        } else
        {
            animator.SetInteger("legs", 5);     // idle
            animator.SetInteger("arms", 5);     // idle
        }
    }

    public void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");     // get keyboard inputs
        float vertical = Input.GetAxis("Vertical");

        // rotate on left and right arrows
        Vector3 rotate = new Vector3(0, horizontal * rotationSpeed * Time.deltaTime, 0);

        // move on up and down arrows
        Vector3 move = new Vector3(0, 0, vertical * Time.deltaTime);
        move = this.transform.TransformDirection(move);

        // set the animations based on whether walking or idle
        if(move != Vector3.zero)
        {
            walking = true; 
        } else
        {
            walking = false;
        }

        characterController.Move(speed * move);

        this.transform.Rotate(rotate);
    }
}
