using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

//Should be attached to Camera
public class RaycastExample : MonoBehaviour
{
    //Raycast variables
    private Ray ray;
    public GameObject highlightedObject;
    public GameObject SelectedObject;
    private RaycastHit hitData;
    public LayerMask selectableLayer;
    //Raycast helper object references
    public GameObject target;
    public GameObject pointerlocation;

    //Camera Variables
    public Camera MainCam;
    public Camera Cam2;

    float rotationX=0;
    private float lookSpeed = 2.0f;
    private int lookLimit=180;

    private Camera playerCamera;

    private float rotationY=0;

    //Movement Variables
    private CharacterController characterController;
    private Vector3 moveDirection;
   
    private float gravity=9.8f;
    public float jumpSpeed=10.0f;

    public Camera[] Cameras;
    public int CameraNum;
    private int nextCamNum=0;

    public float CurrentSpeed { get; private set; }
    public float Speed { get; private set; }

    

    // Start is called before the first frame update
    void Start()
    {
        Speed = 10;
        playerCamera = MainCam;
        characterController = GetComponent<CharacterController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        HighlightObject();
        CheckforCamSwitch();
        if(MainCam.enabled == true)
        {
            MouseRotation();
        }
        MovePlayer();
    }

    private void MovePlayer()
    {
        // get current forward direction of player
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        Vector3 right = transform.TransformDirection(Vector3.right);
        float movementDirectionY = moveDirection.y;//store y value(to keep constant if not falling)


        //Multiply speed by Vertical axis (W key in WASD)
        CurrentSpeed = Speed * Input.GetAxis("Vertical");

        // set current movement to forward at speed from input.
        moveDirection = (forward * CurrentSpeed);
            //+ (right * CurrentSpeed);
       

        //Jump
        if (Input.GetButton("Jump")  && characterController.isGrounded)
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
        
        rotationX += Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookLimit, lookLimit);

        rotationY += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -lookLimit, lookLimit);

        //This actual Rotates the player Camera,using above inputs from mouse. 
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, -rotationY, 0);

        

        //ROTATE PLAYER DISABLED.
        // transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    public void CameraSwitch()//old code
    {
        if (MainCam.enabled == true)
        {
            Cam2.enabled=true;
            MainCam.enabled=false;
            
        }
        else
        {
            if (MainCam.enabled == false)
            {
                Cam2.enabled = false;
                MainCam.enabled = true;
            }
        }
    }

    void ChangeToCamera (Camera CamtoChangeTo)
    {
        CamtoChangeTo.enabled = true;// Make sure camera your changing to is enabled.
        int skipnum = Array.IndexOf(Cameras, CamtoChangeTo);// get the number of the Camera in the array.
        Array.

        for (int i = 0; i < Cameras.Length; i++)//loop through all cameras and disable them,EXCEPT the one your changing to.
        {
            if (i != skipnum)
            {
                Cameras[i].enabled = false;
            }
        }
    }

    void CheckforCamSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UnityEngine.Debug.Log("Q pressed");
            // CameraSwitch();

            //ensure array index does not go to more than the number of cameras available and pick next camera.
            if ((nextCamNum + 1) < Cameras.Length)
            {
                nextCamNum = nextCamNum + 1;//set array index to next camera, add one to current index
                ChangeToCamera(Cameras[nextCamNum]);
            }
            else {
                nextCamNum = 0;
                ChangeToCamera(Cameras[nextCamNum]);
                
            }
        }
    }




    void HighlightObject()
    {

        if (MainCam.enabled == true)
        {
            //ray cast
            ray = Camera.main.ScreenPointToRay(Input.mousePosition); //create Ray from camera to mouse,x,y

            if (Physics.Raycast(ray, out hitData, 1000, selectableLayer))// check ray hit something (returns True or false, 1000 is Range)
            {
                
                //make hightlighted object the hit by raycast 
                highlightedObject = hitData.transform.gameObject;

                //debug draw whote line from pointer to hit pointon object.
                Debug.DrawLine(pointerlocation.transform.position, hitData.point, Color.white,4.0f, depthTest: false);
               
               
               
                ChangeColor(Color.red, highlightedObject);//change object color to red.

                if (Input.GetMouseButtonDown(0))//if mouse button pressed
                {

                    ClickedOnObject(highlightedObject);
                }
            }
            else
            {
                if (highlightedObject!=null)//if ray misses set last highlighted obje back to white color.
                {
                    ChangeColor(Color.white, highlightedObject);
                }

                highlightedObject = null;//if no hit object, clear the highlightobject variable

                if (Input.GetMouseButtonDown(0))//fireButton presssed?
                {
                    SelectedObject = null;
                }
            }
        }
    }

    private void ClickedOnObject(GameObject highlightedObject)
    {
        Destroy(highlightedObject);
    }

    private void ChangeColor(Color newcolor, GameObject ObjectToColor)
    {
        SelectedObject = ObjectToColor;
           

        var SelectedRend = SelectedObject.GetComponent<Renderer>(); //reference the renderer component on on the selected object.

        if (SelectedRend != null)//make sure it has a rendered
        {
            SelectedRend.material.SetColor("_Color", newcolor);
        }
    }
}