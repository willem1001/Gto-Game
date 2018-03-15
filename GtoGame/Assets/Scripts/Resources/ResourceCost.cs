using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    public class ResourceCost : MonoBehaviour
    {
        public Resource Resource;
        public float Cost;

        public bool CanAfford()
        {
            return Resource.CanAfford(Cost);
        }

        public void Pay()
        {
            Resource.Remove(Cost);
            Resource.OnChange.Invoke();
        }
    }
}
