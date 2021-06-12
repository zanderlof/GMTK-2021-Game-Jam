using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public bool canMove;

    private bool moveForward;
    private bool moveBack;
    private bool moveLeft;
    private bool moveRight;
    private bool jump;

    [SerializeField] float moveSpeed = 5;

    public enum LastMove
    {
        Forward = 0,
        Right,
        Back,
        Left
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Movement();
        }
    }

    void Movement()
    {
        //Vector to hold direction, zeroed out every frame.
        Vector3 direction = Vector3.zero;

        /*control movement up and down*/
        if (moveForward)
        {
            direction += Vector3.forward;
            moveForward = false;
            
        }

        if (moveBack)
        {
            direction += Vector3.back;
            moveBack = false;
        }

        /*control movement left and right*/
        if (moveRight)
        {
            direction += Vector3.right;
            moveRight = false;
        }

        if (moveLeft)
        {
            direction += Vector3.left;
            moveLeft = false;
        }

        if (jump)
        {
            direction += Vector3.up;
            jump = false;
        }

        //Translate this transform in direction.
        transform.Translate(direction * Time.deltaTime * moveSpeed);
    }

    public void MoveForward()
    {
        moveForward = true;
    }

    public void MoveBack()
    {
        moveBack = true;
    }

    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    public void Jump()
    {
        jump = true;
    }
}
