using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Renderer renderer;
    public Player player;

    public void Render(Player player)
    {
        this.player = player;
        renderer = GetComponent<Renderer>();
        renderer.material.color = player.color;
    }
}
