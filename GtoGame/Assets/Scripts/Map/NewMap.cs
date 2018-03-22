using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Map;
using UnityEngine;

public class NewMap : MonoBehaviour
{

    public GameObject hex;
    public float width;
    public float height;

    private readonly float _xOffset = Mathf.Sqrt(3);
    private readonly float _zOffset  = 1.5f;
    public List<GameObject> HexList = new List<GameObject>();

    private int _xCoordinateOffset;

	// Use this for initialization
	void Start () {

	    for (int z = 0; z < height; z++)
	    {

	        if (z % 2 == 0 && z != 0)
	        {
	            _xCoordinateOffset--;
	        }
	        for (int x = 0; x < width; x++)
	        {
	            float xPos = x * _xOffset;
	            if (z % 2 == 1)
	            {
	                xPos += _xOffset / 2;
	            }

	            GameObject hexInstance = Instantiate(hex, new Vector3(xPos, 0, z * _zOffset), Quaternion.Euler(new Vector3(90, 0, 0)));
	            hexInstance.name = "Hex_" + (x + _xCoordinateOffset) + "_" + (0 - (x + _xCoordinateOffset) - z) + "_" + z;
                hexInstance.GetComponent<Tile>().position = new Vector3((x + _xCoordinateOffset), (0 - (x + _xCoordinateOffset) - z), z);
                HexList.Add(hexInstance);
	        }
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
