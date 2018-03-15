using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Resources
{
    class ResourceUI : MonoBehaviour
    {
        public Text Name;
        public Text Amount;

        private Resource resource;

        void Start()
        {
            resource = GetComponent<Resource>();
            Name.text = resource.name;
            Amount.text = resource.GetInitialQuantity().ToString();

        }

        public void UpdateUI()
        {
            Amount.text = resource.GetQuantity().ToString();
        }
    }
}
