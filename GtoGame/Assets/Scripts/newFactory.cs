using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Map;
using Assets.Scripts.Resources;
using UnityEngine;
using UnityEngine.UI;

public class newFactory : MonoBehaviour
{

    public GameObject Unit;
    public List<ResourceCost> Costs;
    public GameObject SpawnHex;
    public bool IsSpawning;
    public int spawnTimer;
    public int currentSpawnTimer;
    public Canvas Canvas;
    public Player Player;

    public void Setup(GameObject unit, List<ResourceCost> costs, Canvas canvas, Player player)
    {
        this.Unit = unit;
        this.Costs = costs;
        this.Canvas = canvas;
        this.Player = player;

        this.gameObject.GetComponent<Renderer>().material.color = player.color;
        this.Player.FactoryList.Add(this);
    }

    public void SpawnUnit()
    {
        if (IsSpawning) return;
        var canAfford = true;
        foreach (var cost in Costs)
        {
            if (!cost.CanAfford())
            {
                canAfford = false;
            }


            if (!canAfford) return;

            foreach (var payCost in Costs)
            {
                payCost.Pay();
            }
        }

        if (canAfford)
        {
            OnDeselect();
            IsSpawning = true;
            currentSpawnTimer = spawnTimer;
            transform.GetChild(1).GetChild(1).GetComponent<Image>().fillAmount = 1;
        }
    }

    public void OnSelect()
    {
        if (!IsSpawning)
        {
            Canvas.enabled = true;
        }
    }

    public void OnDeselect()
    {
        Canvas.enabled = false;
    }

    public void TurnOver()
    {
        OnDeselect();

        if(!IsSpawning) return;
        currentSpawnTimer--;
        if (currentSpawnTimer > 0) return;

        foreach (var hex in TileFinder.FindTiles(this.gameObject.transform.parent.gameObject, 2))
        {
            if (hex.transform.childCount != 0) continue;
            SpawnHex = hex;
            break;
        }

        if (SpawnHex == null)
        {
            Debug.Log(this.name + " unitspawn is blocked");
        }
        else
        {
            GameObject unit = Instantiate(Unit, SpawnHex.transform);
            unit.GetComponent<Unit>().player = Player;
            unit.GetComponent<Unit>().Render(Player);
            Player.UnitList.Add(unit);
        }

        IsSpawning = false;
        SpawnHex = null;
        transform.GetChild(1).GetChild(1).GetComponent<Image>().fillAmount = 0;
    }
}
