using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Map;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Renderer renderer;
    public Player player;
    public float range;
    public float rangeLeft;
    public float attackRange;

    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    
    

   

    void Start()
    {
        ResetMove();
        attackRange += 1;
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
        Vector3 difference = tileEnd.GetComponent<Tile>().position - transform.parent.GetComponent<Tile>().position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(-90, 0, rotationZ - 135));


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
