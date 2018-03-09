using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class Map : MonoBehaviour
    {
        public GameObject Hex;
        public GameObject Child;
        public float AddedHexes;
        public float Scale;
        public float Startwidth;
        private List<GameObject> hexList = new List<GameObject>();
        private const float XHexDifference = 0.866025404f;
        private const float ZHexDifference = 1.5f;
        private float _row;
        private float _hexNumber;
        private float _totalHexes;
        private float _yScaleDifference;


        // Use this for initialization
        void Start()
        {
            System.Random rand = new System.Random();
            for (int i = 0; i < Startwidth; i++)
            {
                _totalHexes += Startwidth + i * AddedHexes;
            }

            Hex.transform.localScale = new Vector3(Scale, Scale , Scale);
            for (int i = 0; i < _totalHexes; i++)
            {
                if (_hexNumber >= Startwidth + AddedHexes * _row )
                {
                    _hexNumber = 0;
                    _row++;
                }

                GameObject oppositHex = Hex.gameObject;
                GameObject currentHex = Hex.gameObject;
                float random = rand.Next(0, 100);
                if (random > 66)
                {
                    currentHex.transform.localScale = new Vector3(Scale, Scale, Scale *2);
                    oppositHex.transform.localScale = new Vector3(Scale, Scale, Scale *2);
                    _yScaleDifference = Scale/2;
                }
                else
                {
                    currentHex.transform.localScale = new Vector3(Scale, Scale, Scale);
                    oppositHex.transform.localScale = new Vector3(Scale, Scale, Scale);
                    _yScaleDifference = 0;
                }

            
                currentHex.transform.SetPositionAndRotation(new Vector3((_hexNumber * 2 * XHexDifference + XHexDifference * -AddedHexes * _row) * Scale, _yScaleDifference, ZHexDifference * _row * Scale), Quaternion.Euler(new Vector3(90, 0, 0)));
                GameObject ch = Instantiate(currentHex);
                hexList.Add(ch);

                if (i <= _totalHexes - (Startwidth + (Startwidth - 1) *AddedHexes))
                {     
                    oppositHex.transform.SetPositionAndRotation(new Vector3((_hexNumber * 2 * XHexDifference + XHexDifference * -AddedHexes * _row) * Scale, _yScaleDifference, ZHexDifference * (2 * Startwidth - _row - 2) * Scale), Quaternion.Euler(new Vector3(90, 0, 0)));
                    GameObject oh = Instantiate(oppositHex);
                    hexList.Add(oh);
                }
                _hexNumber++;
            }
        }  
    }
}