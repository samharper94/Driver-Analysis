using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoadSpawn : MonoBehaviour {

    public GameObject StraightRoad;
    public GameObject Curve90Right;
    public GameObject Curve90Left;
    public GameObject FinishLine;
    public Button StraightRoadButton;
    public Button Curve90RightButton;
    public Button Curve90LeftButton;
    public Button FinishLineButton;
    public GameObject RoadSpawnPoint;

    float zOffset = 100f; //Initial offset for road instantiation (start block is 100 units long)
    float xOffset = 0f;
    float roadrotation = 0f;

    // Use this for initialization
    void Start ()
    {
        string start = "Time;RoadPlaced;\n\n";  //write column headings for Excel
        System.IO.File.WriteAllText(@"..\\Output_Files\\Driver_Analysis_RoadPlacement.txt", start); //Clear text file
        Button srbtn = StraightRoadButton.GetComponent<Button>(); //If user clicks Straight Road
        srbtn.onClick.AddListener(SpawnSR); //Go to spawn a straight road
        Button c90rbtn = Curve90RightButton.GetComponent<Button>();
        c90rbtn.onClick.AddListener(SpawnC90R);
        Button c90lbtn = Curve90LeftButton.GetComponent<Button>();
        c90lbtn.onClick.AddListener(SpawnC90L);
        Button flbtn = FinishLineButton.GetComponent<Button>();
        flbtn.onClick.AddListener(SpawnFL);
    }
	
	// Update is called once per frame
	void SpawnSR () //Spawn a straight road
    {
        string roadType = "straight road;";
        Vector3 localOffset = new Vector3(xOffset, 0, zOffset); //Create spawn offset from origin for new road piece
        Vector3 spawnPosition = StraightRoad.transform.position + localOffset; //Set the spawn position to the end of the previous piece of road
        Quaternion localRotation = Quaternion.Euler(0, roadrotation, 0); //Set up road rotation
        Instantiate(StraightRoad, spawnPosition, localRotation);  //Create new road block
        string lines = Time.time.ToString("0.000") + ";" + roadType;
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\\Output_Files\\Driver_Analysis_RoadPlacement.txt", true))
        {
            file.WriteLine(lines);
        }
        if (roadrotation > 359.9f)
        {
            roadrotation = 0f;  //reset road rotation at 360 degrees = 0 degrees
        }
        if (roadrotation == 0f)
        {
            zOffset += 100f;
        }
        if (roadrotation == 180f)
        {
            zOffset -= 100f;
        }
        if (roadrotation == 90f)
        {
            xOffset += 100f;
        }
        if (roadrotation == 270f)
        {
            xOffset -= 100f;
        }
    }

    void SpawnC90R()
    {
        string roadType = "curve 90 right;";
        Vector3 localOffset = new Vector3(xOffset, 0, zOffset);
        Vector3 spawnPosition_C90R = Curve90Right.transform.position + localOffset;
        Quaternion localRotation = Quaternion.Euler(0, roadrotation, 0);
        Instantiate(Curve90Right, spawnPosition_C90R, localRotation);
        // Debug.Log("90 Right Curve spawned.");
        string lines = Time.time.ToString("0.000") + ";" + roadType;
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\\Output_Files\\Driver_Analysis_RoadPlacement.txt", true))
        {
            file.WriteLine(lines);
        }

        if (roadrotation > 359.9f)
        {
            roadrotation = 0f;
        }
        if (roadrotation == 0f)
        {
            zOffset += 25f;
            xOffset += 120f;
        }
        if (roadrotation == 180f)
        {
            zOffset -= 25f;
            xOffset -= 120f;
        }
        if (roadrotation == 90f)
        {
            xOffset += 25f;
            zOffset -= 120f;
        }
        if (roadrotation == 270f)
        {
            xOffset -= 25f;
            zOffset += 120f;
        }
        roadrotation += 90f;
    }

    void SpawnC90L()
    {
        string roadType = "curve 90 left;";
        Vector3 localOffset = new Vector3(xOffset, 0, zOffset);
        Vector3 spawnPosition_C90L = Curve90Left.transform.position + localOffset;
        Quaternion localRotation = Quaternion.Euler(0, roadrotation, 0);
        Instantiate(Curve90Left, spawnPosition_C90L, localRotation);
        //Debug.Log("90 Left Curve spawned.");
        string lines = Time.time.ToString("0.000") + ";" + roadType;
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\\Output_Files\\Driver_Analysis_RoadPlacement.txt", true))
        {
            file.WriteLine(lines);
        }
        if (roadrotation < 0.1f)
        {
            roadrotation = 360;
        }
        if (roadrotation == 360f)
        {
            zOffset += 25f;
            xOffset -= 120f;
        }
        if (roadrotation == 180f)
        {
            zOffset -= 25f;
            xOffset += 120f;
        }
        if (roadrotation == 90f)
        {
            xOffset += 25f;
            zOffset += 120f;
        }
        if (roadrotation == 270f)
        {
            xOffset -= 25f;
            zOffset -= 120f;
        }
        roadrotation -= 90f;
    }

    void SpawnFL() //Spawn the finish line
    {
        string roadType = "finish line;";
        Vector3 localOffset = new Vector3(xOffset, 0, zOffset); //Create spawn offset from origin for new road piece
        Vector3 spawnPosition = FinishLine.transform.position + localOffset; //Set the spawn position to the end of the previous piece of road
        Quaternion localRotation = Quaternion.Euler(0, roadrotation, 0); //Set up road rotation
        Instantiate(FinishLine, spawnPosition, localRotation);  //Create new road block
        string lines = Time.time.ToString("0.000") + ";" + roadType;
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\\Output_Files\\Driver_Analysis_RoadPlacement.txt", true))
        {
            file.WriteLine(lines);
        }
        if (roadrotation > 359.9f)
        {
            roadrotation = 0f;  //reset road rotation at 360 degrees = 0 degrees
        }
        if (roadrotation == 0f)
        {
            zOffset += 100f;
        }
        if (roadrotation == 180f)
        {
            zOffset -= 100f;
        }
        if (roadrotation == 90f)
        {
            xOffset += 100f;
        }
        if (roadrotation == 270f)
        {
            xOffset -= 100f;
        }
    }
}
