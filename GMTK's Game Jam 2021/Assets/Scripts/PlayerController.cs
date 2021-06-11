using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovementController movement;

    bool ForwardIsPressed;
    bool BackIsPressed;
    bool LeftIsPressed;
    bool RightIsPressed;
    bool JumpIsPressed;

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
        JumpIsPressed = Input.GetKey(KeyCode.Space);

        Movement();
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

        /*control movement Jumping*/
        if (JumpIsPressed)
        {
            movement.Jump();
        }
    }
}
