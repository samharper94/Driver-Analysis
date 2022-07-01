using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupervisorInteference : MonoBehaviour {

    public GameObject player_camera;
    float standard_camera_orientation;
    float camera_orientation;
    public GameObject car;
    Vector3 CarPos;

    // Use this for initialization
    void Start () {
        standard_camera_orientation = player_camera.transform.position.x;
        string start = "Time;Interference;\n\n";  //write column headings for Excel
        System.IO.File.WriteAllText(@"..\\Output_Files\\Driver_Analysis_Interference.txt", start); //Clear text file
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.J))
        {
            CarPos = car.transform.position; //get current car position
            CarPos.x = car.transform.position.y + 1; //move car along y axis
            car.transform.position = CarPos; //Move car to new position
        }
	}
}
