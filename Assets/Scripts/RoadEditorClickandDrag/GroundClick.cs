 //This section includes code as credited below, licensed under MIT

//Copyright(C) 2012-2014  Martin "quill18" Glaude


 //   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

 //   The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

 //   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundClick : MonoBehaviour
{ 
    public Collider coll; //setup the ground collider from the GUI
    public GameObject prefabRoad; //call pre-created road prefab
    public GameObject prefabNode; //call pre-created node prefab

    GameObject nodeStart; //node placed upon initial click
    Quaternion nodeRot; //rotation of nide

    void Update()
    {
        Vector3 roadStart;
        if (Input.GetMouseButtonDown(0))
        {
            if (ClickLocation(out roadStart))
            {
                nodeStart = Instantiate(prefabNode, roadStart, nodeRot);
                nodeStart.GetComponent<NodeClick>().ground = this; //create a node on the ground once clicked
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 roadEnd;
            if(nodeStart != null && ClickLocation(out roadEnd)) //Make sure not dragging from skybox
            {

                GameObject nodeEnd = Instantiate(prefabNode, roadEnd, nodeRot); //upon releasing the mouse button, instantiate another node
                nodeEnd.GetComponent<NodeClick>().ground = this;

                CreateRoad(nodeStart.transform.position, nodeEnd.transform.position); //draw a road between the nodes
            }

            else if(nodeStart != null && ClickLocationNode(out roadEnd))
            {
                CreateRoad(nodeStart.transform.position, roadEnd); //if dragging from skybox, create the road as one node
            }

            nodeStart = null; //reset nodes
        }
    }


    bool ClickLocation(out Vector3 point)
    {
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition); //raycast from the mouse

        RaycastHit hitInfo = new RaycastHit(); //store click point
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider == coll) //check if hitting ground collider
            {
                point = hitInfo.point;
                return true;
            }
        }

        point = Vector3.zero;
        return false;

    }

    bool ClickLocationNode(out Vector3 point) //check if object clicked was a node
    {
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition); //raycast from the mouse

        RaycastHit hitInfo = new RaycastHit(); //store click point
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            if(hitInfo.collider.transform.parent!=null && hitInfo.collider.transform.parent.tag == "Node") //check if hitting node collider
            {
                point = hitInfo.collider.transform.position;
                return true;
            }
        }

        point = Vector3.zero;
        return false;

    }

    public void SetNodeStart(GameObject n)
    {
        nodeStart = n;
    }

    public void SetNodeEnd(GameObject n)
    {
        CreateRoad(nodeStart.transform.position, n.transform.position);
        nodeStart = null;
    }

    void CreateRoad(Vector3 roadStart, Vector3 roadEnd)
    {
        
        float width = 1; //mouse offset
        float length = Vector3.Distance(roadStart, roadEnd); //length of road

        if (length < 1)
        {
            return;  //if the road is less than 1 unit long don't create road
        }

        GameObject road = Instantiate(prefabRoad); //spawn a road
        road.transform.position = roadStart + new Vector3(0f, 0.01f, 0f); //move the road above the ground to prevent z-fighting
        nodeRot = road.transform.rotation; //rotate node with road, doesn't work as is lagging one update behind and is 90 degrees off

        road.transform.rotation = Quaternion.FromToRotation(Vector3.right, roadEnd-roadStart); //gets rotation to get from start to end

        Debug.Log(road.transform.rotation.eulerAngles);



        

        Vector3[] vertices =
        {
            new Vector3(0f,     0f,  -width/2), //square
            new Vector3(length, 0f,  -width/2),
            new Vector3(length, 0f,  width/2),
            new Vector3(0f,     0f,  width/2)
        };

        int[] triangles =
        {
            1,0,2,  //triangle 1
            2,0,3   //triangle 2
        };

        Vector2[] uv =
        {
            new Vector2(0,0),
            new Vector2(length,0),  //assigns uv maps based on length variable, saves drawcalls
            new Vector2(length,1),
            new Vector2(0,1)
        };

        Vector3[] normals =
        {
            Vector3.up, //on a flat plane all normals are 90 degrees to the plane therefore straight up
            Vector3.up,
            Vector3.up,
            Vector3.up
        };

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.normals = normals;

        MeshFilter mesh_filter = road.GetComponent<MeshFilter>();
        mesh_filter.mesh = mesh;
    }
}
