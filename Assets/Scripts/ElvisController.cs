using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ElvisController: Agent
{
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        if (Input.GetKey("d"))
            transform.Rotate(0.0f, +2f, 0.0f);

        if (Input.GetKey("a"))
            transform.Rotate(0.0f, -2f, 0.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
       if (collision.collider.tag == "Wall"){
       
       }
    }
 
}
