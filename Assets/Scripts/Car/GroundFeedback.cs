using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;
using UnityEngine;

public class GroundFeedback : MonoBehaviour {

    public float testA;

    void OnTriggerEnter(Collider other)
    {
        GamePad.SetVibration(0, testA, testA);
    }
}
