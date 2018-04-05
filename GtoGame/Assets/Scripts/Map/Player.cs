using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Color color;
    public bool isCurrentPlayer;
    public List<GameObject> UnitList = new List<GameObject>();
    public List<newFactory> FactoryList = new List<newFactory>();
    public List<GameObject> SpawnHexes = new List<GameObject>();
    public List<GameObject> OwnedTiles = new List<GameObject>();
    public Color tileColor;
    public int tilesToWin;
   

    public void StartTurn()
    {
        gameObject.SetActive(true);
        isCurrentPlayer = true;
        foreach (var unit in UnitList)
        {
            unit.GetComponent<Unit>().ResetMove();
        }
    }

    public void EndTurn()
    {
        
        foreach (var factory in FactoryList)
        {
            factory.TurnOver();
        }

        gameObject.SetActive(false);
        isCurrentPlayer = false;
    }

    public bool haswon()
    {
        return OwnedTiles.Count >= tilesToWin;
    }

}
