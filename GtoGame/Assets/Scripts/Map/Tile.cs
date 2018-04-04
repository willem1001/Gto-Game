using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class Tile : MonoBehaviour
    {
        public Vector3 position;
        public Color BaseColor;
        public Material BaseMaterial;
        public Player currentOwner;

        public void setBase()
        {
            this.BaseColor = GetComponent<Renderer>().material.color;
            this.BaseMaterial = GetComponent<Renderer>().material;
        }

        public void ChangeOwner(Player player)
        {

            currentOwner = player;
            var newColor = player.tileColor; 
            this.gameObject.GetComponent<Renderer>().material.color = newColor;
            setBase();
        }

        public void resetToBase()
        {
           gameObject.GetComponent<Renderer>().material = BaseMaterial;
           gameObject.GetComponent<Renderer>().material.color = BaseColor;
        }

        public bool HasChild()
        {

            for (var i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject child = gameObject.transform.GetChild(i).gameObject;
                if (child.tag == "Unit") { return true; }
            }
            return false;
        }

        public void AddChild(GameObject childObject)
        {
            childObject.transform.parent = this.gameObject.transform;
            childObject.transform.position = this.gameObject.transform.position;
        }

        public void Setposition(Vector3 position)
        {
            this.position = position;
        }


        public void range()
        {
            List<Vector3> points = new List<Vector3>();
            if (HasChild())
            {
                GameObject unit = this.transform.GetChild(0).gameObject;

                

                Vector3 baseHex = this.gameObject.GetComponent<Tile>().position;

                for (var tile = 0; tile < unit.GetComponent<Unit>().range +1; tile++)
                {


                    //z same
                    points.Add(new Vector3(baseHex.x + tile, baseHex.y - tile, baseHex.z));
                    points.Add(new Vector3(baseHex.x - tile, baseHex.y + tile, baseHex.z));

                    //z same
                    //points.Add(new Vector3(baseHex.x , baseHex.y - tile, baseHex.z + tile));
                    //points.Add(new Vector3(baseHex.x , baseHex.y + tile, baseHex.z - tile));


                }


            }

            List<GameObject> foundhexes = new List<GameObject>();
            List<GameObject> hexes = this.transform.parent.GetComponent<Map>().GetHexes();

            foreach (var point in points)
            {
                foundhexes.AddRange(hexes.Where(hex => hex.GetComponent<Tile>().position.Equals(point)));
            }

            foreach (var hex in foundhexes)
            {
                hex.GetComponent<Renderer>().material.color = Color.black;
            }
        }




    }
}

