using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Map;
using Assets.Scripts.Resources;
using UnityEngine;

public class FactoryFactory : MonoBehaviour {

    public GameObject Factory;
    public List<ResourceCost> Costs;
    public GameObject Unit;

    public void SpawnFactory(GameObject hex)
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

            GameObject factory = Instantiate(Factory, hex.transform);
            factory.GetComponent<newFactory>().Setup(Unit, Costs, factory.GetComponentInChildren<Canvas>(), GetComponentInParent<Player>());
        }
    }


}
