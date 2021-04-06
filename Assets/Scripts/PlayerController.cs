using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Animator animator;
    public float speed = 6;
    public float rotationSpeed = 180.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetInteger("legs", 5);
        animator.SetInteger("arms", 5);
    }


    private bool walking;
    // Update is called once per frame
    void Update()
    {
        Move();

        if (walking)
        {
            animator.SetInteger("legs", 1);
            animator.SetInteger("arms", 1);
        } else
        {
            animator.SetInteger("legs", 5);
            animator.SetInteger("arms", 5);
        }
    }

    public void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 rotate = new Vector3(0, horizontal * rotationSpeed * Time.deltaTime, 0);

        Vector3 move = new Vector3(0, 0, vertical * Time.deltaTime);
        move = this.transform.TransformDirection(move);

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
