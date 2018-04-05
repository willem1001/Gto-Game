using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<Player> _players;
    private int _currentPlayer;

    void Start()
    {
        foreach (var player in _players)
        {
            player.EndTurn();
        }

       GetCurrentPlayer().StartTurn();
    }

    public void NextTurn()
    {
        if (GetCurrentPlayer().haswon())
        {
            GameOver();
        }

        GetCurrentPlayer().GetComponentInChildren<NewSelect>().Deselect();
        GetCurrentPlayer().EndTurn();
        _currentPlayer++;
        if (_currentPlayer >= _players.Count)
        {
            _currentPlayer = 0;
        }
       GetCurrentPlayer().StartTurn();
    }

    private Player GetCurrentPlayer()
    {
        return _players[_currentPlayer];
    }

    private void GameOver()
    {
        this.gameObject.GetComponentInChildren<Canvas>().enabled = true;
    }
}
