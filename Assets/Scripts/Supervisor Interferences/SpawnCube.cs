using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour {

    public GameObject cube;
    public GameObject cubespawnpoint;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3 spawnpoint = cubespawnpoint.transform.position;
            Quaternion rotation = cubespawnpoint.transform.rotation;
            Instantiate(cube, spawnpoint, rotation);  //instantiate cube at fixed point from car
            string lines = Time.time.ToString("0.000") + ";" + "Cube spawned;";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\\Output_Files\\Driver_Analysis_Interference.txt", true))
            {
                file.WriteLine(lines); //log action
            }
        }
	}
}
