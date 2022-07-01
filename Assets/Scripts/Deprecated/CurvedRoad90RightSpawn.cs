/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurvedRoad90RightSpawn : MonoBehaviour {

    public GameObject Curve90Right;
    public Transform[] spawnpoints;
    public Button Curve90RightButton;
    public GameObject RoadSpawnPoint;

    public float StraightRoadOffset = 1000f;
    public float Curve90RightOffset = 950f;

    // Use this for initialization
    void Start()
    {
        Button c90rbtn = Curve90RightButton.GetComponent<Button>();
        c90rbtn.onClick.AddListener(Spawn);
    }

    // Update is called once per frame
    void Spawn()
    {
        Vector3 localOffset = new Vector3(Curve90RightOffset, 0, StraightRoadOffset);
        //Vector3 spawnPosition_S = Curve90Right.transform.position + straightlocalOffset;
        Vector3 spawnPosition_C90R = Curve90Right.transform.position + localOffset;
        Instantiate(Curve90Right, spawnPosition_C90R, transform.rotation);
        Debug.Log("90 Right Curve spawned.");
        StraightRoadOffset += 1000f;
        Curve90RightOffset += 950f;
    }
}*/
