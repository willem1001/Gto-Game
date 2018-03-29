using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Map;
using Assets.Scripts.Resources;
using UnityEngine;
using UnityEngine.UI;

public class UnitFactory : MonoBehaviour
{
    public GameObject Unit;
    public List<ResourceCost> Costs;
    public TurnManager TurnManager;

    public void SpawnUnit(GameObject hex)
    {
        if (!hex.GetComponent<Tile>().HasChild())
        {
            var canAfford = true;
            foreach (var cost in Costs)
            {
                if (!cost.CanAfford())
                {
                    canAfford = false;
                }
            }

            if (!canAfford) return;
            
                foreach (var cost in Costs)
                {
                    cost.Pay();
                }
                GameObject unit = Instantiate(Unit, hex.transform);
                unit.GetComponent<Unit>().Render(GetComponentInParent<Player>());
                
                GetComponentInParent<Player>().UnitList.Add(unit);
                
                

        }
    }
}
