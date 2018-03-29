using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Renderer renderer;
    public Player player;
    public float range = 2;
    public float rangeLeft;
    public float attackRange = 2;

    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    
    

   

    void Start()
    {
        ResetMove();
    }

    public void Render(Player player)
    {
        this.player = player;
        renderer = GetComponent<Renderer>();
        renderer.material.color = player.color;
    }

    public void Move(int moved)
    {
        rangeLeft -= moved;
    }
    public void ResetMove()
    {
        rangeLeft = range + 1;
    }

    public void TurnToTile(GameObject tileEnd)
    {
        transform.LookAt(new Vector3(transform.position.x, transform.position.y, tileEnd.transform.position.z));

    }

    public void Damaged(int damage)
    {
        currentHealth -= damage;
    }

    public void Die()
    {
        Destroy(this);
    }

    public int Attack()
    {
        return attackDamage;
    }
}
