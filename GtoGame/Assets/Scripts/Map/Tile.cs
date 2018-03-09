using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool HasChild()
    {
        return this.gameObject.transform.childCount > 0;
    }

}
