using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  Very PRIMITIVE rigidbody controller, rotated and add force forward/back.
public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public float forceAmount = 600.0f;//CHECK PHYSICS MATERIAL FRICTION.
    public  float rotspeed = 30.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    private void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);//get forward direction of player

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation *= Quaternion.Euler(0, -rotspeed * Time.fixedDeltaTime, 0);
           
        }

       
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation *= Quaternion.Euler(0, +rotspeed * Time.fixedDeltaTime, 0);
        }


        if (Input.GetKey(KeyCode.W))
            rb.AddForce(forward*forceAmount * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.S))
            rb.AddForce(-forward * forceAmount * Time.fixedDeltaTime);
    }
}
