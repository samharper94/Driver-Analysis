using UnityEngine;
using System.Collections;

public class Cam1 : MonoBehaviour {

    public Transform target;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate () { //runs every frame but guaranteed to run after all other items
        transform.LookAt(target);
	}
}
