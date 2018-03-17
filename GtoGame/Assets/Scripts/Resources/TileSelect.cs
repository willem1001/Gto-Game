using Assets.Scripts.Map;
using UnityEngine;

namespace Assets.Scripts
{
    public class TileSelect : MonoBehaviour
    {
        public UnitFactory Factory;
        private bool _inBuildMode;
        private GameObject selectedObject = null;

        void Update()
        {
            // if (!_inBuildMode) return;
            // if (!Input.GetMouseButtonDown(0)) return;
            RaycastHit raycast;
            // var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycast);
            // if (!hit) return;
            // {
            //
            // }
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
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycast))
            {
                if (Input.GetMouseButton(0))
                {
                    Factory.SpawnUnit(raycast.transform.gameObject);
                }
                if (Input.GetMouseButton(1))
                {
                    Debug.Log("selected: " + raycast.collider.name);
                    selectedObject = raycast.transform.gameObject;
                    SelectObject(selectedObject);
                }
                else
                {
                    ClearSelection();
                }
            }

        }
        void SelectObject(GameObject obj)
        {
            if (selectedObject != null)
            {
                if (obj == selectedObject)
                    return;

                ClearSelection();
            }

            selectedObject = obj;

            Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
            {
                Material m = r.material;
                m.color = Color.green;
                r.material = m;
            }
        }

        void ClearSelection()
        {
            if (selectedObject == null)
                return;

            Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
            {
                Material m = r.material;
                m.color = Color.white;
                r.material = m;
            }


            selectedObject = null;
        }
        public void EnterBuildMode()
        {
            _inBuildMode = true;
        }
    }
}
