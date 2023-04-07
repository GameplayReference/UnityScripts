using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{

    //Movement Variables
    private CharacterController characterController;
    private Vector3 moveDirection=Vector3.zero;

    private float gravity = 9.8f;
    public float jumpSpeed = 10.0f;
    public float RunSpeed = 20.0f;

    public float CurrentSpeed { get; private set; }
    public float Speed { get; private set; }


    //mouse rotation
    float rotationX = 0;
    private float rotationY = 0;
    private float lookSpeed = 2.0f;
    private int lookLimit = 180;
    const float BaseSpeed = 10.0f;
    public Camera playerCamera;

    

    // Start is called before the first frame update
    void Start()
    {
        Speed = 10;
        characterController = GetComponent<CharacterController>();

        LockCursor(true);
    }

    private void LockCursor(bool isCursorlocked)
    {
        if (isCursorlocked)
        {
            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }




    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        MouseRotation();
    }


    private void MovePlayer()
    {
        // get current forward direction of player
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        Vector3 right = transform.TransformDirection(Vector3.right);
        float movementDirectionY = moveDirection.y;//store y value(to keep constant if not falling)

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = RunSpeed;
        }
        else { Speed = BaseSpeed; }// 10.0f 

        //Multiply speed by Vertical axis (W/s key in WASD)
        CurrentSpeed = Speed * Input.GetAxis("Vertical");

        // set current movement to forward at speed from input.
        moveDirection = (forward * CurrentSpeed);
        //+ (right * CurrentSpeed);


        //Jump
        if (Input.GetButton("Jump") && characterController.isGrounded)// using character controller to set isGrounded.
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Gravity - Applied if player not on ground
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // This Actually moves the Player!
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void MouseRotation()
    {

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;// add mouse input to the x rotation.(muliplied by lookspeed.)
        rotationX = Mathf.Clamp(rotationX, -lookLimit, lookLimit);


        //NOT neccesary as mouse x roation is done on player(assumes camera in on player
       // rotationY += Input.GetAxis("Mouse X") * lookSpeed;
       // rotationY = Mathf.Clamp(rotationY, -lookLimit, lookLimit);

        //This actual Rotates the player Camera,using above inputs from mouse. 
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, -rotationY, 0);



        //Rotates PLAYER AND Camera Around X
         transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

}
