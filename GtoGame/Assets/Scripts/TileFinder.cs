using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Map;
using UnityEngine;

public class TileFinder : MonoBehaviour
{

    private static NewMap map;

    void Start()
    {
        map = this.GetComponentInParent<NewMap>();
    }

    public static List<GameObject> FindTiles(GameObject baseHex, int range)
    {
        Vector3 basePos = baseHex.GetComponent<Tile>().position;
        List<GameObject> hexList = map.GetHexes();
        List<Vector3> points = new List<Vector3>();
        List<GameObject> foundHexes = new List<GameObject>();

        for (var tile = 1; tile < range; tile++)
        {
            Vector3 middleRight = new Vector3(basePos.x + tile, basePos.y - tile, basePos.z);
            Vector3 middleLeft = new Vector3(basePos.x - tile, basePos.y + tile, basePos.z);
            Vector3 rightUp = new Vector3(basePos.x, basePos.y - tile, basePos.z + tile);
            Vector3 rightDown = new Vector3(basePos.x + tile, basePos.y, basePos.z - tile);
            Vector3 leftUp = new Vector3(basePos.x - tile, basePos.y, basePos.z + tile);
            Vector3 leftDown = new Vector3(basePos.x, basePos.y + tile, basePos.z - tile);

            points.AddRange(InbetweenTiles(middleRight, rightUp, tile));
            points.AddRange(InbetweenTiles(middleRight, rightDown, tile));
            points.AddRange(InbetweenTiles(middleLeft, leftUp, tile));
            points.AddRange(InbetweenTiles(middleLeft, leftDown, tile));
            points.AddRange(InbetweenTiles(rightUp, leftUp, tile));
            points.AddRange(InbetweenTiles(rightDown, leftDown, tile));

            points.Add(middleRight);
            points.Add(middleLeft);
            points.Add(rightUp);
            points.Add(rightDown);
            points.Add(leftUp);
            points.Add(leftDown);

        }



        foreach (var point in points)
        {
            foreach (var hex in hexList)
            {
                if (hex.GetComponent<Tile>().position == point && !foundHexes.Contains(hex))
                {
                    foundHexes.Add(hex);
                }
            }
        }

        return foundHexes;
    }


    private static List<Vector3> InbetweenTiles(Vector3 start, Vector3 end, int tile)
    {

        List<Vector3> points = new List<Vector3>();
        Vector3 step = (start - end) / tile;

        for (var i = 1; i < tile; i++)
        {
            points.Add(end + step * i);
        }
        return points;
    }
}
