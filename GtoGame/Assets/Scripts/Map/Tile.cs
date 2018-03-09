using UnityEngine;

public class Tile : MonoBehaviour
{

    public bool HasChild()
    {
        return this.gameObject.transform.childCount > 0;
    }

    public void AddChild(GameObject parentObject, GameObject childObject)
    {
        childObject.transform.parent = parentObject.transform;
        childObject.transform.position = parentObject.transform.position;
    }
}

