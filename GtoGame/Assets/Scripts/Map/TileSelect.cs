using UnityEngine;

namespace Assets.Scripts
{
    public class TileSelect : MonoBehaviour
    {
        private GameObject hit1;
        private GameObject hit2;

        // Use this for initialization
        void Start () {
		
        }
        
        // Update is called once per frame
        void Update () {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit raycast = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycast);
                if (hit && hit1 == null)
                {
                    hit1 = raycast.transform.gameObject;
                    Tile t = hit1.GetComponent<Tile>();
                    Debug.Log(t.HasChild());
                }
                else if (hit)
                {
                    hit2 = raycast.transform.gameObject;
                    Debug.Log("2");
                }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                hit2 = null;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                hit1.GetComponent<Tile>().AddChild(hit2, hit1.transform.GetChild(0).gameObject);
                hit1 = null;
                hit2 = null;
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
            {
                hit1 = null;
                hit2 = null;
            }
        }
    }
}
