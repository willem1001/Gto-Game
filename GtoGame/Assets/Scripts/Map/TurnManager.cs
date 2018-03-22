using System.Collections;
using System.Collections.Generic;
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

        _players[_currentPlayer].StartTurn();
    }

    public void NextTurn()
    {
        GetCurrentPlayer().GetComponentInChildren<TileSelect>().ClearSelection();
        GetCurrentPlayer().EndTurn();
        _currentPlayer++;
        if (_currentPlayer >= _players.Count)
        {
            _currentPlayer = 0;
        }
        _players[_currentPlayer].StartTurn();
    }

    public Player GetCurrentPlayer()
    {
        return _players[_currentPlayer];
    }
}
