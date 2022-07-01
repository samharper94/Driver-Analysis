using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeClick : MonoBehaviour

{
    public GroundClick ground;

    void OnMouseDown()
    {
        ground.SetNodeStart(gameObject);
    }



}
