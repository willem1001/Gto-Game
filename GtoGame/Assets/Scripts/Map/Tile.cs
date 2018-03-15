using UnityEngine;

namespace Assets.Scripts.Map
{
    public class Tile : MonoBehaviour
    {

        public bool HasChild()
        {

            for (var i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject child = gameObject.transform.GetChild(i).gameObject;
                if (child.tag == "Unit") {return true;}
            }
            return false;
        }

        public void AddChild(GameObject parentObject, GameObject childObject)
        {
            childObject.transform.parent = parentObject.transform;
            childObject.transform.position = parentObject.transform.position;
        }
    }
}

