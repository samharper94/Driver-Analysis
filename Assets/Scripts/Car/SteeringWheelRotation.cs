using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelRotation : MonoBehaviour {

    public GameObject wheel;
    float rotations = 0;
    public float turnSpeed;
    Quaternion defaultRotation;

    void Start()
    {
 
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            defaultRotation = wheel.transform.localRotation;

            wheel.transform.Rotate(Vector3.forward * Time.deltaTime * turnSpeed);
            rotations -= turnSpeed;
        }

        else if (Input.GetKey(KeyCode.D))
        {

            wheel.transform.Rotate(-Vector3.forward * Time.deltaTime * turnSpeed);
            rotations += turnSpeed;
        }
     else    //rotate to default
     {
            if (rotations < 0)
            {
                wheel.transform.Rotate(-Vector3.forward * Time.deltaTime * turnSpeed);
                rotations += turnSpeed;
            }
            else if (rotations > 0)
            {
                wheel.transform.Rotate(Vector3.forward * Time.deltaTime * turnSpeed);
                rotations -= turnSpeed;
            }
            //if (rotations != 0)
            //{
              //  wheel.transform.localRotation = defaultRotation;
                //rotations = 0;
            //}
        }
    }
}
