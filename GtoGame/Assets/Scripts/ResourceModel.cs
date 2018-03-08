using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class ResourceModel : MonoBehaviour
    {
        private string Name { get; set; }
        private string Discription { get; set; }
        private float Amount { get; set; }

        event EventHandler<Resource> 
    }
}
