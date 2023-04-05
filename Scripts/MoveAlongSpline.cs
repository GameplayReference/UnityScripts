using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gameobject moves on the spline by maintaing nearest position to player.contains addition code for lerp 
public class MoveAlongSpline : MonoBehaviour
{
    public Spline spline;
    public Transform FollowObj;
    private Transform thisTransform;
    public float Speed = 1.0f;
    private Vector3 movDir;

    //for lerp
    public int FrameCount = 1200;
    int elapsedFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        thisTransform.position = spline.NearestSplinePoint(FollowObj.position);


        //mov direction is direction from one point ot next

        // firstsecondDir = spline.SplineSection(this.thisTransform.position, spline.S).
       // doLerp();//Disabled

    }

    void doLerp()
    {
        if (spline.SplinePoints.Length > 1)
        {
            Vector3 startVec = spline.SplinePoints[0];//HARDCODE
            Vector3 desVec = spline.SplinePoints[1];
           // transform.position = Vector3.Lerp(startVec, desVec, Speed * Time.deltaTime);

            //recalulate ratio
            float Ratio = (float)elapsedFrames / FrameCount;

            Vector3 interpolatedPosition = Vector3.Lerp(startVec, desVec, Ratio);
            //move 2 new position
            transform.position = interpolatedPosition;

            //increase frames(modulus reset to 0;
            elapsedFrames = (elapsedFrames + 1) % (FrameCount + 1);

            //Can be done in While loop for onceoff;
            while(elapsedFrames<FrameCount)
            {
                elapsedFrames++;

            }
        }
    }
}
