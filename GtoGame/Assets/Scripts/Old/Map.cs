using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


namespace Assets.Scripts.Map
{
    public class Map : MonoBehaviour
    {
        public GameObject Hex;
        public int Startwidth;
        private List<GameObject> HexList = new List<GameObject>();
        private float XHexDifference = (Mathf.Sqrt(3)/2);
        private const float ZHexDifference = 1.5f;
        private GameObject startHex;

        // Use this for initialization
        public void Start()
        {
            //    for (var currentRow = 0; currentRow < Startwidth; currentRow++)
            //    {
            //        for (var currentHex = 0; currentHex < Startwidth + currentRow; currentHex++)
            //        {
            //            var x = (currentHex * XHexDifference * 2) - (currentRow * XHexDifference);
            //            var z = (currentRow * ZHexDifference);
            //            var zOpposite = ZHexDifference * (2 * Startwidth - currentRow - 2);
            //            var heightScale = 1; //CreateHills(new Vector3(x, 1, z));
            //            var y = (heightScale - 1) * (Hex.transform.localScale.y / 2);

            //            GameObject hex = Instantiate(Hex, new Vector3(x, y, z), Quaternion.Euler(new Vector3(90, 0, 0)),
            //                gameObject.transform);
            //            hex.transform.localScale = new Vector3(1, 1, heightScale);
            //            HexList.Add(hex);
            //            if (currentRow == Startwidth - 1) continue;
            //            {
            //                GameObject hexOpposite = Instantiate(Hex, new Vector3(x, y, zOpposite),
            //                    Quaternion.Euler(new Vector3(90, 0, 0)), gameObject.transform);
            //                hexOpposite.transform.localScale = new Vector3(1, 1, heightScale);
            //                HexList.Add(hexOpposite);
            //            }
            //        }
            //    }

            //    foreach (var hex in HexList)
            //    {
            //        hex.GetComponent<Tile>().Setposition();
            //    }


            for (var z = 0; z < Startwidth; z++)
            {
                for (var x = 0; x < Startwidth; x++)
                {
                    if (z % 2 == 0)
                    {

                        GameObject hex = Instantiate(Hex, new Vector3(x * XHexDifference * 2 + XHexDifference , 1, z * ZHexDifference), Quaternion.Euler(new Vector3(90, 0, 0)), gameObject.transform);
                        hex.GetComponent<Tile>().Setposition(new Vector3((0 - z - x), z , x));
                        HexList.Add(hex);
                    }
                    else
                    {
                        GameObject hex = Instantiate(Hex, new Vector3(x * XHexDifference * 2, 1, z * ZHexDifference), Quaternion.Euler(new Vector3(90, 0, 0)), gameObject.transform);
                        hex.GetComponent<Tile>().Setposition(new Vector3((0 - z - x), z, x));
                        HexList.Add(hex);
                    }       
                }
            }





        }

        public List<GameObject> GetHexes()
        {
            return HexList;
        }

        //public float CreateHills(Vector3 currentPosition)
        //{
        //    var rand = _random.Next(1, 100);
        //    Collider[] hitColliders = Physics.OverlapSphere(currentPosition, XHexDifference * 2);
        //    foreach (Collider c in hitColliders)
        //    {
        //        if (c.gameObject.transform.position == currentPosition) continue;
        //        if (c.gameObject.transform.localScale.z > 1)
        //        {
        //            if (c.gameObject.transform.localScale.z > 2)
        //            {
        //                if (rand < 50) return 2;
        //                {
        //                    return 3;
        //                }
        //            }
        //            if (rand < 33) return 1;
        //            if (rand > 33 && rand < 66) return 2;
        //            if (rand > 66) return 3;
        //        }
        //        else
        //        {
        //            if (rand < 66) return 1;
        //            {
        //                return 2;
        //            }
        //        }
        //    }
        //    return 1;
        //}
    }
}