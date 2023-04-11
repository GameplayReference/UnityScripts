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
   
    private int current=0;//the point in array 
    private float acceptableCloseness=0.1f;
    public GameObject SplineObject;

    //for transform along nodes method(  moveObjonVector3Array)
    public Vector3[] waypoints;
    public int nodecount;

    public float TurnSpeed = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
         nodecount = SplineObject.transform.childCount;//array size equal to number of child object attached

        for (int i = 0; i < nodecount; i++)//fill array with postion of all child objects
        {
    
            waypoints[i] = SplineObject.transform.GetChild(i).position;
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        // for follow player code
        // thisTransform.position = spline.NearestSplinePoint(FollowObj.position);
        //mov direction is direction from one point ot next
        // firstsecondDir = spline.SplineSection(this.thisTransform.position, spline.S).
        // doLerp();//Disabled
        moveObjonVector3Array();
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

    private void moveObjonVector3Array()//transforms and rotates object along the spline(loops)
    {
        if( Vector3.Distance(waypoints[current],transform.position ) < acceptableCloseness)
          {
            current++;// change to  next waypoint

            //set current back to 0 to loop.
            if (current >= waypoints.Length)
            {
                current = 0;
            }
           
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current], Time.deltaTime * 10.0f);

       //change angle

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 TargetDirection = waypoints[current] - transform.position;
        float singleStep = TurnSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(forward, TargetDirection, singleStep, 0.0f);
        Debug.DrawRay(transform.position, newDirection, Color.yellow);

        //apply rotation
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
