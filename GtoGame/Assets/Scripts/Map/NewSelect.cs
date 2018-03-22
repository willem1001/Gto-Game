using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Map;
using UnityEngine;

public class NewSelect : MonoBehaviour
{


    public GameObject map;

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
        List<GameObject> HexList = map.GetComponent<NewMap>().HexList;
        List<Vector3> points = new List<Vector3>();

        for (var tile = 0; tile < 3; tile++)
        {



            points.Add(new Vector3(basePos.x + tile, basePos.y - tile, basePos.z));
            points.Add(new Vector3(basePos.x - tile, basePos.y + tile, basePos.z));

            points.Add(new Vector3(basePos.x + tile, basePos.y, basePos.z - tile));
            points.Add(new Vector3(basePos.x - tile, basePos.y, basePos.z + tile));

            points.Add(new Vector3(basePos.x, basePos.y + tile, basePos.z - tile));
            points.Add(new Vector3(basePos.x, basePos.y - tile, basePos.z + tile));


        }

        List<GameObject> foundhexes = new List<GameObject>();

        foreach (var point in points)
        {
            foundhexes.AddRange(HexList.Where(hex => hex.GetComponent<Tile>().position.Equals(point)));
        }

        foreach (var hex in foundhexes)
        {
            hex.GetComponent<Renderer>().material.color = Color.black;
        }

    }
}
