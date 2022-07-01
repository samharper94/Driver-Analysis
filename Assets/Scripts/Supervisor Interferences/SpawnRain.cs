using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRain : MonoBehaviour
{

    public GameObject rain;
    public GameObject defaultpoint;
    public Transform[] rainspawnpoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            int i = 0;
            for(i = 0; i <=39; i++) 
            {
                rainspawnpoint[i].position = new Vector3(defaultpoint.transform.position.x + (i*1f), defaultpoint.transform.position.y, defaultpoint.transform.position.z);
                Quaternion rotation = rainspawnpoint[i].transform.rotation;
                Instantiate(rain, rainspawnpoint[i].position, rotation); //instantiate 40 rain models at a fixed distance from the car
            }
            for (i = 0; i <= 39; i++)
            {
                rainspawnpoint[i].position = new Vector3(defaultpoint.transform.position.x, defaultpoint.transform.position.y, defaultpoint.transform.position.z);
                //reset rain position
            }
            i = 0;
            string lines = Time.time.ToString("0.000") + ";" + "Rain spawned;";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\\Output_Files\\Driver_Analysis_Interference.txt", true))
            {
                file.WriteLine(lines); //log rain
            }
        }
    }
}

