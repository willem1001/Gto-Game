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
    public int black;
    public int green;
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
        var standardGreen = 0;
        var standardBlack = 0;


        foreach (var instance in _hexList)
        {
            if (current <= 2 * width)
            {
                instance.GetComponent<Tile>().ChangeOwner(player1);
                standardBlack++;
            }
            else if (current >= total - 2 * width)
            {
                instance.GetComponent<Tile>().ChangeOwner(player2);
                standardGreen++;
            }
            else
            {
                if (green >= halfwayPoint)
                {      
                    instance.GetComponent<Tile>().ChangeOwner(player1);
                    black++;
                }
                else if (black >= halfwayPoint)
                {
                    instance.GetComponent<Tile>().ChangeOwner(player2);
                    green++;
                }
                else if (random(0, 100) > chance)
                {
                    instance.GetComponent<Tile>().ChangeOwner(player1);
                    black++;
                }
                else
                {
                    instance.GetComponent<Tile>().ChangeOwner(player2);
                    green++;
                }
                chance += change;
            }
            current++;
        }
        foreach (var instance in _hexList)
        {
            instance.GetComponent<Tile>().setBase();
        }

        green += standardGreen;
        black += standardBlack;
    }

    public List<GameObject> GetHexes()
    {
        return _hexList;
    }

    // Update is called once per frame
    void Update()
    {

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
