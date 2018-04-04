using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Map;
using UnityEngine;

public class NewSelect : MonoBehaviour
{

    public GameObject map;
    List<GameObject> movementHexes = new List<GameObject>();
    List<GameObject> attackHexes = new List<GameObject>();
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
                _selectedUnit = tile.transform.GetChild(0).gameObject;
                if (tile.GetComponentInChildren<Unit>().player.isCurrentPlayer)
                {
                    FindTiles(tile, true);
                }
                else
                {
                    FindTiles(tile, false);
                }
                
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
                MoveUnit(tile, _selectedUnit);
            }
            else if(Input.GetMouseButtonDown(1) && _selectedUnit != null && tile.transform.childCount == 1 && _selectedUnit.GetComponent<Unit>().canAttack)
            {
               AttackUnit(tile, _selectedUnit);
            }
        }
    }


    private void FindTiles(GameObject baseHex, bool fromActivePlayer)
    {
        Deselect();
        Unit unitScript = baseHex.GetComponentInChildren<Unit>();


         movementHexes = TileFinder(baseHex, (int)unitScript.rangeLeft);
        if (fromActivePlayer && unitScript.canAttack)
        {
            attackHexes = TileFinder(baseHex, (int) unitScript.attackRange);
        }

        foreach (var hex in movementHexes)
        {
            if (hex.transform.childCount == 0)
            {
                hex.GetComponent<Renderer>().material.color = Color.green;
            }
        }

        foreach (var hex in attackHexes)
        {
            if (hex.transform.childCount <= 0) continue;
            if (!hex.transform.GetComponentInChildren<Unit>().player.isCurrentPlayer)
            {
                hex.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    private List<GameObject> TileFinder(GameObject baseHex, int range)
    {
        Vector3 basePos = baseHex.GetComponent<Tile>().position;
        List<GameObject> hexList = map.GetComponent<NewMap>().GetHexes();
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

    private List<Vector3> InbetweenTiles(Vector3 start, Vector3 end, int tile)
    {

        List<Vector3> points = new List<Vector3>();
        Vector3 step = (start - end) / tile;

        for (var i = 1; i < tile; i++)
        {
            points.Add(end + step * i);
        }
        return points;
    }

    public void Deselect()
    {
        foreach (var hex in movementHexes)
        {
            hex.GetComponent<Tile>().resetToBase();
        }
        foreach (var hex in attackHexes)
        {
            hex.GetComponent<Tile>().resetToBase();
        }

        movementHexes.Clear();
        attackHexes.Clear();
    }

    public void EnterBuildMode()
    {
        _inBuildMode = true;
    }

    private void MoveUnit(GameObject tile, GameObject unit)
    {
        if (movementHexes.Contains(tile) && unit.GetComponent<Unit>().player.isCurrentPlayer)
        {
            Deselect();

            Vector3 startPos = unit.GetComponentInParent<Tile>().position;
            Vector3 endPos = tile.GetComponent<Tile>().position;

            
                unit.GetComponent<Unit>().TurnToTile(tile);
            
            
            Vector3 difference = startPos - endPos;
            var move = (int) (Math.Abs(difference.x) + Math.Abs(difference.y) + Math.Abs(difference.z)) / 2;
            unit.GetComponent<Unit>().Move(move);
            tile.GetComponent<Tile>().AddChild(unit);

            if (tile.GetComponent<Tile>().currentOwner != unit.GetComponent<Unit>().player)
            {
                tile.GetComponent<Tile>().ChangeOwner(unit.GetComponent<Unit>().player);
            }
        }
    }

    private void AttackUnit(GameObject tile, GameObject unit)
    {
        if (attackHexes.Contains(tile) && unit.GetComponent<Unit>().player.isCurrentPlayer &&
            !tile.GetComponentInChildren<Unit>().player.isCurrentPlayer)
        {
            Unit attackedUnit = tile.GetComponentInChildren<Unit>();
            attackedUnit.Damaged(unit.GetComponent<Unit>().Attack());
            FindTiles(unit.transform.parent.gameObject, unit);           
        }
    }
}
