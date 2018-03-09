using UnityEngine;

namespace Assets.Scripts
{
    public class TileSelect : MonoBehaviour {

        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit raycast = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycast);
                if (hit)
                {
                    Destroy(raycast.transform.gameObject);
                }
            }
        }
    }
}
