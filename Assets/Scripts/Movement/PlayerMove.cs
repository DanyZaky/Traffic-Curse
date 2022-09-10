using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isRunning;

    public Rigidbody2D rb;
    public Animator anim;

    Vector2 movement;

    private void Start()
    {
        isRunning = false;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || 
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || 
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        //if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) ||
        //    Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) ||
        //    Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
        //    Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    isRunning = false;
        //}

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
