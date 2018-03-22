using Assets.Scripts.Map;
using UnityEngine;

namespace Assets.Scripts
{
    public class TileSelect : MonoBehaviour
    {
        public UnitFactory Factory;
        private bool _inBuildMode;
        private GameObject _selectedObject;
        public Material _standardHexColor;

        void Update()
        {
            RaycastHit raycast;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycast))
            {
                if (Input.GetMouseButton(0) && _inBuildMode)
                {
                    Factory.SpawnUnit(raycast.transform.gameObject);
                }
                else if (Input.GetMouseButton(1))
                {
                    SelectObject(raycast.transform.gameObject);
                }
            }

        }
        private void SelectObject(GameObject obj)
        {
            if (_selectedObject != null)
            {
                if (obj.transform.localPosition != _selectedObject.transform.localPosition)
                {
                    ClearSelection();
                }
            }

            _selectedObject = obj;

            var hexRenderer = _selectedObject.GetComponent<Renderer>();
            hexRenderer.material.color = Color.yellow;

            _inBuildMode = false;
        }

        public void ClearSelection()
        {

            var hexRenderer = _selectedObject.GetComponent<Renderer>();
            hexRenderer.material = _standardHexColor;
            _selectedObject = null;
        }
        public void EnterBuildMode()
        {
            _inBuildMode = true;
        }
    }
}
