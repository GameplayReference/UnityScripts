using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
 
 public Vector3[] SplinePoints; //array of points, can be gameobject positions in editor(need to be child object of the gameobject this script is attached to.)
    public int SplineCount; // no of points in the spline

    public bool showSpline;//bool for toggling visablity--NOT USED YET

    private void Start()
    {
        SplineCount = transform.childCount; //no of spline node equal number of sub objects;
        SplinePoints = new Vector3[SplineCount]; //make array of splines of spline count number;

        for (int i = 0; i < SplineCount; i++)//fill array with postion of all child objects
        {
            SplinePoints[i] = transform.GetChild(i).position;
        }
       // UpdateSplines();

    }

    private void UpdateSplines()
    {
        

        for (int i = 0; i < SplineCount; i++)
        {
            SplinePoints[i] = transform.GetChild(i).position;
        }
    }

    private void Update()
    {

        if (SplineCount > 1)//draws line from each point(press gizmo button if not visable) 
        {
            for (int i = 0; i < SplineCount-1; i++)
            {
                Debug.DrawLine(SplinePoints[i], SplinePoints[i + 1], Color.red);
            }
        }
      
    }

    public Vector3 NearestSplinePoint(Vector3 pos)
    {
        int closestSplinePointIndex = GetClosestPointonSpline(pos);

        if(closestSplinePointIndex==0)
        {
            return SplineSection(SplinePoints[0], SplinePoints[1], pos);
        }
        else if( closestSplinePointIndex==(SplineCount-1))
            {
                return SplineSection(SplinePoints[SplineCount - 1], SplinePoints[SplineCount - 2], pos);
            }
           
        
         else
        {

            Vector3 leftSection = SplineSection(SplinePoints[closestSplinePointIndex - 1], SplinePoints[closestSplinePointIndex], pos);
            Vector3 RightSection = SplineSection(SplinePoints[closestSplinePointIndex + 1], SplinePoints[closestSplinePointIndex], pos);

            if ((pos - leftSection).sqrMagnitude <= (pos - RightSection).sqrMagnitude)
            {
                return leftSection;
            }
            else { return RightSection; }
        }
    }

    private int GetClosestPointonSpline(Vector3 pos)//calulate distance from given point(pos) to each point on spine.return index num of closest point.
    {
        int ClosestPoint = -1;

        float Shortestdistance = 0.0f;

        for (int i = 0; i < SplineCount; i++)
        {
            float sqrdistance = (SplinePoints[i] - pos).sqrMagnitude;
            if (Shortestdistance == 0 || sqrdistance < Shortestdistance)
            {
                Shortestdistance = sqrdistance;
                ClosestPoint = i;
               
            }
        }
        return ClosestPoint;


    }

    public Vector3 SplineSection(Vector3 v1, Vector3 v2, Vector3 pointposition)
    {
        Vector3 v1toPos = pointposition - v1;
        Vector3 SectionDirection = (v2 - v1).normalized;//direction of section

        float DistanceFromV1 = Vector3.Dot(SectionDirection, v1toPos);

        if (DistanceFromV1 < 0.0f)
        {
            return v1;
        }
        else
        {
            if (DistanceFromV1 * DistanceFromV1 > (v2 - v1).sqrMagnitude)
            {
                return v2;
            }
            else
            {
                Vector3 FromV1 = SectionDirection * DistanceFromV1;
                return v1 + FromV1;
            }
        }
    }

     public Vector3 returnDir(int v1, int v2)//???
    {
        Vector3 direction = (SplinePoints[v1] - SplinePoints[v2]).normalized;
        return direction;
    }


}
