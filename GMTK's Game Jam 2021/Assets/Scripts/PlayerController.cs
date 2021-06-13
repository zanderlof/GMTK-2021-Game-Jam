using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovementController movement;
    public float lookSpeed;

    //sounds
    public AK.Wwise.Event footsteps;

    //movement detection
    bool ForwardIsPressed;
    bool BackIsPressed;
    bool LeftIsPressed;
    bool RightIsPressed;
    bool JumpIsPressed;
    bool CrouchIsPressed;
    bool SlideIsPressed;

    //private variables
    private PowerGenerator.Elemental type;

    //states
    bool isMoving;
    bool movementChange;
    bool sizeChange;
    bool isPowered;
    int activeGun;

    // Start is called before the first frame update
    void Start()
    {
        isPowered = false;
    }

    // Update is called once per frame
    void Update()
    {
        ForwardIsPressed = Input.GetKey(KeyCode.W);
        BackIsPressed = Input.GetKey(KeyCode.S);
        LeftIsPressed = Input.GetKey(KeyCode.A);
        RightIsPressed = Input.GetKey(KeyCode.D);
        //JumpIsPressed = Input.GetKey(KeyCode.Space);
        CrouchIsPressed = Input.GetKey(KeyCode.LeftControl);
        //SlideIsPressed = Input.GetKey(KeyCode.LeftShift);

        Movement();
        // Aim();
        //Replace in favour of controlling cam directly

        SwitchWeapon();
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

        if (LeftIsPressed || RightIsPressed || ForwardIsPressed || BackIsPressed)
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

        if (sizeChange != CrouchIsPressed)
        {
            sizeChange = CrouchIsPressed;

            if (CrouchIsPressed)
            {
                movement.Crouch();
            }
            else
            {
                movement.Stand();
            }
        }
    }

    // public void Aim()
    // {
    //     transform.eulerAngles += lookSpeed * new Vector3(0, Input.GetAxis("Mouse X"), 0);
    // }

    public void SwitchWeapon()
    {
        KeyCode gun = 0;
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            gun = KeyCode.Alpha1;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            gun = KeyCode.Alpha2;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            gun = KeyCode.Alpha3;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            gun = KeyCode.Alpha4;
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            gun = KeyCode.Alpha5;
        }

        if (gun != 0)
        {
            transform.GetChild(0).GetChild(activeGun).gameObject.SetActive(false);
            activeGun = (int)gun - 49;
            transform.GetChild(0).GetChild(activeGun).gameObject.SetActive(true);
        }
        
    }

    public void SetType(PowerGenerator.Elemental element)
    {
        type = element;
    }

    public void PowerOn()
    {
        isPowered = true;

        for (int i = 0; i < 1; i++)
        {
            transform.GetChild(0).GetChild(i).gameObject.GetComponent<GunController>().powerOn();
        }
    }

    public void PowerOff()
    {
        isPowered = false;
        for (int i = 0; i < 1; i++)
        {
            transform.GetChild(0).GetChild(i).gameObject.GetComponent<GunController>().powerOff();
        }
    }
}
