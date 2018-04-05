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
        public bool isSpawn = false;

        public void setBase()
        {
            this.BaseColor = GetComponent<Renderer>().material.color;
            this.BaseMaterial = GetComponent<Renderer>().material;
        }

        public void setOwner(Player player)
        {
            currentOwner = player;
            var newColor = player.tileColor;
            this.gameObject.GetComponent<Renderer>().material.color = newColor;
            setBase();

            if (!isSpawn)
            {
                currentOwner.OwnedTiles.Add(this.gameObject);
            }
        }

        public void ChangeOwner(Player player)
        {
            currentOwner.OwnedTiles.Remove(this.gameObject);

            currentOwner = player;
            var newColor = player.tileColor;
            this.gameObject.GetComponent<Renderer>().material.color = newColor;
            setBase();

            currentOwner.OwnedTiles.Add(this.gameObject);
        }

        public void ResetToBase()
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
    }
}

