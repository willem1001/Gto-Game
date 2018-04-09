using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using Assets.Scripts.Map;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public List<Player> _players;
    private int _currentPlayer;
    public CameraController MainCamera;

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
        GetCurrentPlayer().GetComponentInChildren<NewSelect>().Deselect();
        GetCurrentPlayer().EndTurn();

        if (GetCurrentPlayer().haswon())
        {
            GameOver();
        }

        _currentPlayer++;
        if (_currentPlayer >= _players.Count)
        {
            _currentPlayer = 0;
        }
       MainCamera.SwitchTurn();
       GetCurrentPlayer().StartTurn();
    }

    private Player GetCurrentPlayer()
    {
        return _players[_currentPlayer];
    }

    private void GameOver()
    {
        Color wonColor = GetCurrentPlayer().color;
        wonColor.a = 1f;
        Canvas canvas = this.gameObject.GetComponentInChildren<Canvas>();
        canvas.GetComponentInChildren<Text>().text = GetCurrentPlayer().playerColor +" has won";
        canvas.GetComponentInChildren<Text>().color = wonColor;
        canvas.enabled = true;
        
    }
}
