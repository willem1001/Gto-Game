using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Map;
using UnityEngine;

public class NewSelect : MonoBehaviour
{

    public GameObject map;
    public List<GameObject> _selectedHexes = new List<GameObject>();
    public Material baseColor;
    public UnitFactory Factory;
    private bool _inBuildMode;
    private GameObject _selectedUnit;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit raycast;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycast))
        {
            
            GameObject tile = raycast.transform.gameObject;
            if (tile.transform.childCount != 0 && Input.GetMouseButtonDown(0))
            {
                FindTiles(tile, tile.transform.GetChild(0).gameObject.GetComponent<Unit>().rangeLeft);
                _selectedUnit = tile.transform.GetChild(0).gameObject;
            }
            else if (tile.transform.childCount == 0 && Input.GetMouseButtonDown(0) && !_inBuildMode)
            {
                Deselect();
                _selectedUnit = null;
            }
            else if (_inBuildMode && tile.transform.childCount == 0 && Input.GetMouseButtonDown(0))
            {
                Factory.SpawnUnit(tile);
                _inBuildMode = false;
            }
            else if (Input.GetMouseButtonDown(1) && _selectedUnit != null && tile.transform.childCount == 0)
            {
                moveUnit(tile, _selectedUnit);
            }
            else if(Input.GetMouseButtonDown(1) && _selectedUnit != null && tile.transform.childCount == 1)
            {
               attackUnit(tile, _selectedUnit);
            }
        }


    }


    private void FindTiles(GameObject baseHex, float range)
    {

        Deselect();


        Vector3 basePos = baseHex.GetComponent<Tile>().position;
        List<GameObject> hexList = map.GetComponent<NewMap>().getHexes();
        List<Vector3> points = new List<Vector3>();



        for (var tile = 0; tile < range; tile++)
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
                if (hex.GetComponent<Tile>().position == point && !_selectedHexes.Contains(hex))
                {
                    _selectedHexes.Add(hex);
                }
            }
        }

        foreach (var hex in _selectedHexes)
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

    public void Deselect()
    {
        foreach (var hex in _selectedHexes)
        {
            hex.GetComponent<Renderer>().material = baseColor;
        }

        _selectedHexes.Clear();
    }

    public void EnterBuildMode()
    {
        _inBuildMode = true;
    }

    private void moveUnit(GameObject tile, GameObject unit)
    {
        if (_selectedHexes.Contains(tile) && unit.GetComponent<Unit>().player.isCurrentPlayer)
        {
            Deselect();

            Vector3 startPos = unit.GetComponentInParent<Tile>().position;
            Vector3 endPos = tile.GetComponent<Tile>().position;

            
                unit.GetComponent<Unit>().TurnToTile(tile);
            
            
            Vector3 difference = startPos - endPos;
            var move = (int) (Math.Abs(difference.x) + Math.Abs(difference.y) + Math.Abs(difference.z)) / 2;
            unit.GetComponent<Unit>().Move(move);
            unit.transform.parent = tile.transform;
            unit.transform.position = tile.transform.position;
        }
    }

    private void attackUnit(GameObject tile, GameObject unit)
    { 
        
    }
}
