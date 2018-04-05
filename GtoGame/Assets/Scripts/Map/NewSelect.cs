using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Map;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewSelect : MonoBehaviour
{
    List<GameObject> movementHexes = new List<GameObject>();
    List<GameObject> attackHexes = new List<GameObject>();
    public FactoryFactory Factory;
    private bool _inBuildMode;
    private GameObject _selectedUnit;
    


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        { 
        RaycastHit raycast;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycast))
            {
                GameObject tile = raycast.transform.gameObject;

                if (tile.transform.childCount == 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_inBuildMode && this.GetComponentInParent<Player>().SpawnHexes.Contains(tile))
                        {
                            Factory.SpawnFactory(tile);
                            ExitBuildMode();
                        }
                        else if (_selectedUnit != null)
                        {
                            if (_selectedUnit.GetComponent<newFactory>() != null)
                            {
                                _selectedUnit.GetComponent<newFactory>().OnDeselect();
                            }

                            Deselect();
                            _selectedUnit = null;
                        }
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        if (_selectedUnit != null && _selectedUnit.GetComponent<Unit>() != null)
                        {
                            MoveUnit(tile, _selectedUnit);
                        }
                    }
                }
                else if (tile.transform.childCount > 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        _selectedUnit = tile.transform.GetChild(0).gameObject;

                        if (_selectedUnit.GetComponent<Unit>() != null)
                        {
                            FindTiles(tile, _selectedUnit.GetComponent<Unit>().player.isCurrentPlayer);
                        }
                        else if (_selectedUnit.GetComponent<newFactory>() != null &&
                                 _selectedUnit.GetComponent<newFactory>().Player.isCurrentPlayer)
                        {
                            _selectedUnit.GetComponent<newFactory>().OnSelect();
                        }

                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        if (tile.GetComponentInChildren<Unit>() != null)
                        {
                            AttackUnit(tile, _selectedUnit);
                        }
                    }
                }
            }
        }
    }


    private void FindTiles(GameObject baseHex, bool fromActivePlayer)
    {
        Deselect();
        Unit unitScript = baseHex.GetComponentInChildren<Unit>();


        movementHexes = TileFinder.FindTiles(baseHex, (int)unitScript.rangeLeft);
        if (fromActivePlayer && unitScript.canAttack)
        {
            attackHexes = TileFinder.FindTiles(baseHex, (int)unitScript.attackRange);
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
            if (hex.transform.GetComponentInChildren<Unit>() != null && !hex.transform.GetComponentInChildren<Unit>().player.isCurrentPlayer)
            {
                hex.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (hex.transform.GetComponentInChildren<newFactory>() != null &&
                     !hex.transform.GetComponentInChildren<newFactory>().Player.isCurrentPlayer)
            {
                hex.GetComponent<Renderer>().material.color = Color.red;
            }
        }
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

        foreach (var spawnHex in this.GetComponentInParent<Player>().SpawnHexes)
        {
            spawnHex.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    private void ExitBuildMode()
    {
        _inBuildMode = false;

        foreach (var spawnHex in this.GetComponentInParent<Player>().SpawnHexes)
        {
            spawnHex.GetComponent<Tile>().resetToBase();
        }
    }

    public void TurnEnded()
    {
        Deselect();
        ExitBuildMode();

        if (_selectedUnit.GetComponent<newFactory>() != null)
        {
            _selectedUnit.GetComponent<newFactory>().OnDeselect();
        }

        _selectedUnit = null;
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
            var move = (int)(Math.Abs(difference.x) + Math.Abs(difference.y) + Math.Abs(difference.z)) / 2;
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
