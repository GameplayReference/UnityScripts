using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwiitcher : MonoBehaviour
{
    //Camera Variables
    public Camera MainCam;
    public Camera Cam2;


    public Camera[] Cameras;
    public int CameraNum;
    private int nextCamNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CameraSwitch()//old code
    {
        if (MainCam.enabled == true)
        {
            Cam2.enabled = true;
            MainCam.enabled = false;

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

    void ChangeToCamera(Camera CamtoChangeTo)
    {
        CamtoChangeTo.enabled = true;// Make sure camera your changing to is enabled.
        int skipnum = System.Array.IndexOf(Cameras, CamtoChangeTo);// get the number of the Camera in the array.


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
            else
            {
                nextCamNum = 0;
                ChangeToCamera(Cameras[nextCamNum]);

            }
        }
    }

}
