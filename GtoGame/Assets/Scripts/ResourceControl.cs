using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class ResourceControl : MonoBehaviour
    {
        private ResourceModel model;
        private event EventHandler<ResourceTextChange> onChange; 

        public void Start()
        {
            model = new ResourceModel();
        }

        public void Add()
        {
            model.Amount++;

        }

    }
}
