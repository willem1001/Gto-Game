using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Color color;

    public void StartTurn()
    {
        gameObject.SetActive(true);
    }

    public void EndTurn()
    {
        gameObject.SetActive(false);
    }

}
