using Assets.Scripts.Map;
using UnityEngine;

namespace Assets.Scripts
{
    public class TileSelect : MonoBehaviour
    {
        public UnitFactory Factory;
        private bool _inBuildMode;


        void Update()
        {
            if (!_inBuildMode) return;
            if (!Input.GetMouseButtonDown(0)) return;
            RaycastHit raycast;
            var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycast);
            if (!hit) return;
            {
                Factory.SpawnUnit(raycast.transform.gameObject);
            }
            //else if (Input.GetMouseButtonDown(1) && _select != null)
            //{
            //    RaycastHit raycast;
            //    var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycast);
            //    if (hit && !raycast.transform.gameObject.GetComponent<Tile>().HasChild())
            //    {
            //        _select.GetComponent<Tile>().AddChild(raycast.transform.gameObject,
            //            _select.gameObject.transform.GetChild(0).gameObject);
            //    }
            //}
        }

        public void EnterBuildMode()
        {
            _inBuildMode = true;
        }
    }
}
