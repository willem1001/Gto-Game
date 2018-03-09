using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Unit : MonoBehaviour {
    public GameObject UnitObject;

    public void SpawnUnit()
    {
        foreach (var hex in Map.hexList)
        {
            if (!hex.GetComponent<Tile>().HasChild())
            {
                Instantiate(UnitObject, hex.transform.position, hex.transform.rotation, hex.transform);
                break;
            }
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
