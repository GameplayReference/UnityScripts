using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

//Should be attached to Camera, will only respond to object on the selectable Layer(pick in editor, create new layer if none suitable.
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
    public GameObject TheGameManager;
    

    public Camera playerCameraforCasting;

    FeedbackManager theFeedbackManager;

    public float CurrentSpeed { get; private set; }
    public float Speed { get; private set; }

    

    // Start is called before the first frame update
    void Start()
    {
        // reference the FeedbackManager to avoid using get component at runtime
        theFeedbackManager = TheGameManager.GetComponent<FeedbackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HighlightObject();
       
          
    }
 

    void HighlightObject()
    {

        if (playerCameraforCasting.enabled == true)
        {
            //ray cast
            ray = Camera.main.ScreenPointToRay(Input.mousePosition); //create Ray from camera to mouse,x,y

            if (Physics.Raycast(ray, out hitData, 1000, selectableLayer))// check ray hit something (returns True or false, 1000 is Range)
            {
                
                //make hightlighted object the hit by raycast 
                highlightedObject = hitData.transform.gameObject;

                //debug draw whole line from pointer to hit pointon object.
                Debug.DrawLine(pointerlocation.transform.position, hitData.point, Color.white,4.0f, depthTest: false);

                //TheGameManager.GetComponent<FeedbackManager>().ChangeText( "Hit  "+ hitData.transform.gameObject.name); Avoiding getComponent.
                theFeedbackManager.ChangeText("Hit  " + hitData.transform.gameObject.name);



                ChangeColor(Color.red, highlightedObject);//change object color to red.


                if (Input.GetMouseButtonDown(0))//if mouse button pressed
                {

                    ClickedOnObject(highlightedObject);
                }
            }
            else
            {
                //Hit nothing iteractabe, blank middletext;

                theFeedbackManager.ChangeText("" );


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
