using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public variables
    public MovementController movement;
    public float lookSpeed;

    //sounds
    public AK.Wwise.Event footsteps;

    //private variables
    bool ForwardIsPressed;
    bool BackIsPressed;
    bool LeftIsPressed;
    bool RightIsPressed;
    bool JumpIsPressed;
    bool isMoving;
    bool movementChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ForwardIsPressed = Input.GetKey(KeyCode.W);
        BackIsPressed = Input.GetKey(KeyCode.S);
        LeftIsPressed = Input.GetKey(KeyCode.A);
        RightIsPressed = Input.GetKey(KeyCode.D);
        //JumpIsPressed = Input.GetKey(KeyCode.Space);

        Movement();
        Aim();
    }

    public void Movement()
    {
        if (ForwardIsPressed)
        {
            movement.MoveForward();
        }
        else if (BackIsPressed)
        {
            movement.MoveBack();
        }

        /*control movement left and right*/
        if (RightIsPressed)
        {
            movement.MoveRight();
        }
        else if (LeftIsPressed)
        {
            movement.MoveLeft();
        }

        if(LeftIsPressed || RightIsPressed || ForwardIsPressed || BackIsPressed)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        /*control movement Jumping*/
        if (JumpIsPressed)
        {
            movement.Jump();
        }

        if (movementChange != isMoving)
        {
            movementChange = isMoving;

            if (isMoving)
            {
                footsteps.Post(gameObject);
            }
            else
            {
                footsteps.Stop(gameObject);
            }
        }
    }

    public void Aim()
    {
        transform.eulerAngles += lookSpeed * new Vector3(0, Input.GetAxis("Mouse X"), 0);
    }
}
