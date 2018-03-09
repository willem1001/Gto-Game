using Assets.Scripts.Map;
using UnityEngine;

namespace Assets.Scripts
{
    public class TileSelect : MonoBehaviour
    {
        public GameObject Child;
        private GameObject _select;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit raycast;
                var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycast);
                if (!hit) return;
                {
                    _select = raycast.transform.gameObject;
                    if (_select.GetComponent<Tile>().HasChild()) return;
                    {
                        Instantiate(Child, _select.transform.position, _select.transform.rotation, _select.transform);
                        _select = null;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1) && _select != null)
            {
                RaycastHit raycast;
                var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycast);
                if (hit && !raycast.transform.gameObject.GetComponent<Tile>().HasChild())
                {
                    _select.GetComponent<Tile>().AddChild(raycast.transform.gameObject, _select.gameObject.transform.GetChild(0).gameObject);
                }
            }
        }
    }
}
