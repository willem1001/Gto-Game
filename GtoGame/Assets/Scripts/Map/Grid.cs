using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public float AddedHexes;
    public float Scale;
    public float Startwidth;
    private List<Vector3> pointList = new List<Vector3>();
    private const float XHexDifference = 0.866025404f;
    private const float ZHexDifference = 1.5f;
    private float _row;
    private float _hexNumber;
    private float _totalHexes;


    // Use this for initialization
    void Start()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < Startwidth; i++)
        {
            _totalHexes += Startwidth + i * AddedHexes;
        }
        for (int i = 0; i < _totalHexes; i++)
        {
            if (_hexNumber >= Startwidth + AddedHexes * _row)
            {
                _hexNumber = 0;
                _row++;
            }
            pointList.Add(new Vector3((_hexNumber * 2 * XHexDifference + XHexDifference * -AddedHexes * _row) * Scale, ZHexDifference * _row * Scale));

            if (i <= _totalHexes - (Startwidth + (Startwidth - 1) * AddedHexes))
            {
                pointList.Add(new Vector3((_hexNumber * 2 * XHexDifference + XHexDifference * -AddedHexes * _row) * Scale, ZHexDifference * (2 * Startwidth - _row - 2) * Scale));
            }
            _hexNumber++;
        }

    }
}
