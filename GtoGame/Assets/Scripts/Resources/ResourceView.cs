using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ResourceView : MonoBehaviour
    {
        public void Start()
        {
            ResourceController.OnChange += this.AddResource;
        }

        public void AddResource(ResourceModel resourceModel)
        {
            var t = GameObject.Find("ResourceText").gameObject.GetComponent<Text>();
            t.text = ResourceModel.Amount.ToString();
        }

       
    }
}
