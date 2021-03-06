using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //public
    public bool canMove;
    public float playerSpeed;
    public float runSpeed = 8;
    public float knockBackStunTime = 1f;

    //private
    private bool moveForward;
    private bool moveBack;
    private bool moveLeft;
    private bool moveRight;
    private bool jump;
    public float jumpForce;
    private Rigidbody rb;
    private float currentSpeed;
    [HideInInspector] public bool knockedBack;

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
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Movement();
        }
        // if (knockedBack)
        // {
        //     StartCoroutine(KnockBackRevert());
        // }
    }

    // IEnumerator KnockBackRevert()
    // {
    //     yield return new WaitForSeconds(knockBackStunTime);
    //     knockedBack = false;
    // }

    void Movement()
    {
        //Vector to hold direction, zeroed out every frame.
        Vector3 direction = Vector3.zero;

        /*control movement up and down*/
        if (moveForward)
        {
            direction += transform.forward;
            moveForward = false;

        }

        if (moveBack)
        {
            direction += -transform.forward;
            moveBack = false;
        }

        /*control movement left and right*/
        if (moveRight)
        {
            direction += transform.right;
            moveRight = false;
        }

        if (moveLeft)
        {
            direction += -transform.right;
            moveLeft = false;
        }

        if (jump)
        {
            direction += transform.up;
            jump = false;
        }

        //Translate this transform in direction.
        // transform.Translate(direction * Time.deltaTime * playerSpeed);
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : playerSpeed;
        rb.MovePosition(rb.position + direction * Time.deltaTime * currentSpeed);
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
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

    public void Crouch()
    {
        transform.localScale = new Vector3(1, 0.5f, 1);
        transform.position += Vector3.down * 0.4f;
        transform.GetChild(0).GetChild(0).localScale += Vector3.up * 0.1f;
    }

    public void Stand()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.GetChild(0).GetChild(0).localScale -= Vector3.up * 0.1f;
    }
}
