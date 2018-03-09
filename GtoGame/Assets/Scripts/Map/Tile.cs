using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {


    public Texture texture;
    public GameObject hex;
    public Vector3 position;
    
    public bool HasChild()
    {
       return this.gameObject.transform.childCount > 0;
    }

    public void AddChild(GameObject parentObject, GameObject childObject)
    {
        childObject.transform.parent = parentObject.transform;
    }
}
