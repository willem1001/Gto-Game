using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Assets.Scripts.Map;
using UnityEngine;

public class NewMap : MonoBehaviour
{

    public GameObject hex;
    public float width;
    public float height;
    public TurnManager TurnManager;
    private Player player1;
    private Player player2;
    private readonly float _xOffset = Mathf.Sqrt(3);
    private readonly float _zOffset = 1.5f;
    private readonly List<GameObject> _hexList = new List<GameObject>();




    private int _xCoordinateOffset;

    // Use this for initialization
    void Start()
    {
        player1 = TurnManager._players[0];
        player2 = TurnManager._players[1];
        for (var z = 0; z < height; z++)
        {

            if (z % 2 == 0 && z != 0)
            {
                _xCoordinateOffset--;
            }
            for (var x = 0; x < width; x++)
            {
                var xPos = x * _xOffset;
                if (z % 2 == 1)
                {
                    xPos += _xOffset / 2;
                }

                GameObject hexInstance = Instantiate(hex, new Vector3(xPos, RandomHeight(), z * _zOffset),
                    Quaternion.Euler(new Vector3(90, 0, 0)));
                hexInstance.name = "Hex_" + (x + _xCoordinateOffset) + "_" + (0 - (x + _xCoordinateOffset) - z) + "_" + z;
                hexInstance.isStatic = true;
                hexInstance.transform.SetParent(this.transform);
                hexInstance.GetComponent<Tile>().position =
                    new Vector3(x + _xCoordinateOffset, 0 - (x + _xCoordinateOffset) - z, z);
                _hexList.Add(hexInstance);

            }
        }


        var total = width * height;
        var halfwayPoint = (total - 4 * width) / 2;
        float chance = 0;
        var change = 100 / (total - 4f * width);
        var current = 1;
        var tilesToWin = total - 4 * width;


        foreach (var instance in _hexList)
        {
            if (current <= 2 * width)
            {
                instance.GetComponent<Tile>().isSpawn = true;
                instance.GetComponent<Tile>().setOwner(player1);
                player1.SpawnHexes.Add(instance);
            }
            else if (current > total - 2 * width)
            {
                instance.GetComponent<Tile>().isSpawn = true;
                instance.GetComponent<Tile>().setOwner(player2);
                player2.SpawnHexes.Add(instance);
            }
            else
            {
                if (player2.OwnedTiles.Count >= halfwayPoint)
                {
                    instance.GetComponent<Tile>().setOwner(player1);
                }
                else if (player1.OwnedTiles.Count >= halfwayPoint)
                {
                    instance.GetComponent<Tile>().setOwner(player2);
                }
                else
                {
                    var currentChance = chance;
                    if (player1.OwnedTiles.Count > player2.OwnedTiles.Count)
                    {
                        currentChance -= 3;
                    }
                    else
                    {
                        currentChance += 3;
                    }

                    if (random(0, 100) >= currentChance)
                    {
                        instance.GetComponent<Tile>().setOwner(player1);
                    }
                    else
                    {
                        instance.GetComponent<Tile>().setOwner(player2);
                    }
                }
                chance += change;
            }
            current++;
        }
        foreach (var instance in _hexList)
        {
            instance.GetComponent<Tile>().setBase();
        }
        player1.tilesToWin = (int)tilesToWin;
        player2.tilesToWin = (int)tilesToWin;
    }

    public List<GameObject> GetHexes()
    {
        return _hexList;
    }

    private float random(int start, int end)
    {
        return Random.Range(start, end);
    }
    private float RandomHeight()
    {
        return Random.Range(0f, .5f);
    }



}
