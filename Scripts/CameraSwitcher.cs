using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    //Camera Variables
    public Camera MainCam;
    public Camera Cam2;
    public RenderTexture screenCamtexture;

    public Camera[] Cameras;
    public int CameraNum;
    private int nextCamNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        //specifies starting camera, can be change in inspector while Cameras array is public, not neccessary, as camera0 is a "main camera"
        ChangeToCamera(Cameras[0]);
    }

    // Update is called once per frame
    void Update()
    {
        CheckforCameraButtonNumPress();

    }

    private void CheckforCameraButtonNumPress()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))//press 1 on main keyboard, Above the Q key (not numeric keypad)
        {
            ChangeToCamera(Cameras[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeToCamera(Cameras[1]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeToCamera(Cameras[2]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeToCamera(Cameras[3]);
        }
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
        int dontdisable = 1; // Specifiing UI thumbnail Camera Number, TODO a better way.
        Cameras[1].enabled = true;// ensure that camera 1 (UI thumbnail is always enabled)


        for (int i = 0; i < Cameras.Length; i++)//loop through all cameras and disable them,EXCEPT the one your changing to.
        {

            if (i != skipnum)
            {
                Cameras[i].enabled = false;
            }

        }

        if (skipnum == dontdisable)//  if the camera on the UI is switched to disable the UI thumnail Render texture
        {

            Cameras[1].targetTexture = null;
        }
        else
        {
            //enable screen cam texture
            Cameras[1].targetTexture = screenCamtexture;

        }
        Cameras[1].enabled = true;// ensure that camera 1 (UI thumbnail is always enabled)DUPLICATE CODE, CHECK IS NECCESSARY BEFORE FOR LOOP        
    }

    void CheckforCamSwitch()//old code
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
