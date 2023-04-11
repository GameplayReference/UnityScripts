using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Very Basic script for point camera at object.uses UPDATE
public class LookAtObj : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
    }
}
