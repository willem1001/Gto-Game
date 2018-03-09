using Assets.Scripts.Map;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    public class Unit : MonoBehaviour {
        public GameObject UnitObject;

        public void SpawnUnit()
        {
            foreach (var hex in Map.Map.HexList)
            {
                if (hex.GetComponent<Tile>().HasChild()) continue;
                GameObject unit = Instantiate(UnitObject, hex.transform.position, hex.transform.rotation);
                unit.transform.SetParent(hex.transform);
                break;
            }
        }
    }
}
