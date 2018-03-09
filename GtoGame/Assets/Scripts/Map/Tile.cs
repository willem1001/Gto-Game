using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {


    public Texture texture;
    public GameObject gameObject;
    public GameObject gameObjectChild;
    public GameObject hex;
    
    public bool HasChild()
    {
        return this.gameObject.transform.childCount > 0;
    }

   public void Instantiate(Vector3 position)
   {
        Instantiate(hex, position, new Quaternion(90, 0, 0,0));
   } 

}
