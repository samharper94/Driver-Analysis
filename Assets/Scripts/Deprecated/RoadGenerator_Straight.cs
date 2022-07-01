using UnityEngine;
using System.Collections;

public class RoadGenerator_Straight : MonoBehaviour
{

    public GameObject[] RoadPieces = new GameObject[2];
    const float RoadLength = 100f; //length of roads

    const float RoadSpeed = 5f; //speed to scroll roads at
    void Update()
    {
        foreach (GameObject road in RoadPieces)
        {
            Vector3 newRoadPos = road.transform.position;
            newRoadPos.z -= RoadSpeed * Time.deltaTime;
            if (newRoadPos.z < -RoadLength / 2)
            {
                newRoadPos.z += RoadLength;
            }
            road.transform.position = newRoadPos;
        }
    }
}