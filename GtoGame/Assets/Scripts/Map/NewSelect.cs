using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Map;
using UnityEngine;

public class NewSelect : MonoBehaviour
{

    public float range;
    public GameObject map;
    private readonly List<GameObject> SelectedHexes = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit raycast;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycast) && Input.GetMouseButtonDown(0))
        {
            FindTiles(raycast.transform.gameObject);
        }


    }


    void FindTiles(GameObject baseHex)
    {
        Vector3 basePos = baseHex.GetComponent<Tile>().position;
        List<GameObject> hexList = map.GetComponent<NewMap>().HexList;
        List<Vector3> points = new List<Vector3>();



        for (var tile = 0; tile < range + 1; tile++)
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
            SelectedHexes.AddRange(hexList.Where(hex => hex.GetComponent<Tile>().position.Equals(point)));
        }

        foreach (var hex in SelectedHexes)
        {
            hex.GetComponent<Renderer>().material.color = Color.red;
        }


    }

    private List<Vector3> InbetweenTiles(Vector3 start, Vector3 end, int tile)
    {

        List<Vector3> points = new List<Vector3>();
        Vector3 step = (start - end) / tile;

        for (int i = 1; i < tile; i++)
        {

           points.Add(end + step * i);
        }
        return points;
    }
}
