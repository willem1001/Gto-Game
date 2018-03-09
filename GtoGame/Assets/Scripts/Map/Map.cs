using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


namespace Assets.Scripts.Map
{
    public class Map : MonoBehaviour
    {
        public GameObject Hex;
        public int Startwidth;
        public static List<GameObject> HexList = new List<GameObject>();
        private const float XHexDifference = 0.866025404f;
        private const float ZHexDifference = 1.5f;
        private readonly Random _random = new Random();
        


        // Use this for initialization
        public void Start()
        {
            for (var currentRow = 0; currentRow < Startwidth; currentRow++)
            {
                for (var currentHex = 0; currentHex < Startwidth + currentRow; currentHex++)
                {
                    var x = (currentHex * XHexDifference * 2) - (currentRow * XHexDifference);
                    var z = (currentRow * ZHexDifference);
                    var zOpposite = ZHexDifference * (2 * Startwidth - currentRow - 2);
                    var heightScale = CreateHills(new Vector3(x, 1, z));
                    var y = (heightScale - 1) * (Hex.transform.localScale.y / 2);

                    GameObject hex = Instantiate(Hex, new Vector3(x, y, z), Quaternion.Euler(new Vector3(90, 0, 0)),
                        gameObject.transform);
                    hex.transform.localScale = new Vector3(1, 1, heightScale);
                    HexList.Add(hex);

                    if (currentRow == Startwidth - 1) continue;
                    {
                        GameObject hexOpposite = Instantiate(Hex, new Vector3(x, y, zOpposite),
                            Quaternion.Euler(new Vector3(90, 0, 0)), gameObject.transform);
                        hexOpposite.transform.localScale = new Vector3(1, 1, heightScale);
                        HexList.Add(hexOpposite);
                    }
                }
            }
        }

        public float CreateHills(Vector3 currentPosition)
        {
            var rand = _random.Next(1, 100);
            Collider[] hitColliders = Physics.OverlapSphere(currentPosition, 0.866025404f * 2);
            foreach (Collider c in hitColliders)
            {
                if (c.gameObject.transform.position == currentPosition) continue;
                if (c.gameObject.transform.localScale.z > 1)
                {
                    if (c.gameObject.transform.localScale.z > 2)
                    {
                        if (rand < 50) return 2;
                        {
                            return 3;
                        }
                    }
                    if (rand < 33) return 1;
                    if (rand > 33 && rand < 66) return 2;
                    if (rand > 66) return 3;
                }
                else
                {
                    if (rand < 66) return 1;
                    {
                        return 2;
                    }
                }
            }
            return 1;
        }
    }
}