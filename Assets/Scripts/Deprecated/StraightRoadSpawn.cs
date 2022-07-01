/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StraightRoadSpawn : MonoBehaviour {

    public GameObject StraightRoad;
    public Transform[] spawnpoints;
    public Button StraightRoadButton;
    public GameObject RoadSpawnPoint;

    public float StraightRoadOffset = 1000f;

    // Use this for initialization
    void Start()
    {
        Button srbtn = StraightRoadButton.GetComponent<Button>();
        srbtn.onClick.AddListener(Spawn);
    }

    // Update is called once per frame
    void Spawn()
    {
        Vector3 localOffset = new Vector3(0, 0, StraightRoadOffset);
        Vector3 spawnPosition = StraightRoad.transform.position + localOffset;
        Instantiate(StraightRoad, spawnPosition, transform.rotation);
        Debug.Log("Straight Road spawned.");
        StraightRoadOffset += 1000f;
    }


}*/
