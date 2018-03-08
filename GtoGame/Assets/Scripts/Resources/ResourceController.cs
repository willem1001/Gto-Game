using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class ResourceController : MonoBehaviour
    {
        public delegate void ResourceEventHandler(ResourceModel resourceModel);
        
        public static event ResourceEventHandler OnChange;

        ResourceModel resourceModel;

        public void Start()
        {
            resourceModel = new ResourceModel();
            ResourceModel.Amount = 123;
           
        }
        public void AddUp()
        {
            if(OnChange != null)
            {
                ResourceModel.Amount += 5;
                OnChange(resourceModel);
            }
        }

        public void Subtract()
        {
            if(OnChange != null)
            {
                ResourceModel.Amount -= 5;
                OnChange(resourceModel);
            }
        }


    }
}
