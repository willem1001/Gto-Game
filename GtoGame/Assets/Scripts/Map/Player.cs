using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Color color;
    public bool isCurrentPlayer;
    public List<GameObject> UnitList = new List<GameObject>();
    public Color tileColor;

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
        gameObject.SetActive(false);
        isCurrentPlayer = false;
    }

}
