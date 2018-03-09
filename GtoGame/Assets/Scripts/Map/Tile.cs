using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool hasChild()
    {
        return this.gameObject.transform.childCount > 0;
    }

}
