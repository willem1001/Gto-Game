using UnityEngine;

namespace Assets.Scripts.Map
{
    public class CameraController : MonoBehaviour
    {
        public float PanSpeed = 20f;
        public float PanBorderThickness = 10f;

        public Vector2 PanLimitMax;
        public Vector2 PanLimitMin;
        public float ScrollSpeed = 20f;

        public float MinY =0f;
        public float MaxY=40f;
        public GameObject MapGameObject;
        float startWidthx;
        float centerx;
        float startWidthy;
        float centery;
        

        public void start()
        {
        }
        // Update is called once per frame
        public void Update()
        {
            if (startWidthx.Equals(0)||startWidthy.Equals(0))
            {
                startWidthx = MapGameObject.GetComponentInChildren<NewMap>().width;
                centerx = startWidthx / 2;
                PanLimitMin.x = centerx;
                PanLimitMax.x = centerx + startWidthx;
                startWidthy = MapGameObject.GetComponentInChildren<NewMap>().height;
                centery = startWidthy / 2;
                PanLimitMin.y = centery-startWidthy;
                PanLimitMax.y = centery*2f;
            }
            
            
            
            Vector3 position = transform.position;
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - PanBorderThickness)
            {
                position.z += PanSpeed * Time.deltaTime;
            }

            if (Input.GetKey("s") || Input.mousePosition.y <= PanBorderThickness)
            {
                position.z -= PanSpeed * Time.deltaTime;
            }

            if(Input.GetKey("d")||Input.mousePosition.x >= Screen.width - PanBorderThickness)
            {
                position.x += PanSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= PanBorderThickness)
            {
                position.x -= PanSpeed * Time.deltaTime;
            }

            position.x = Mathf.Clamp(position.x, PanLimitMin.x, PanLimitMax.x);
            position.z = Mathf.Clamp(position.z, PanLimitMin.y, PanLimitMax.y);
            position.y = Mathf.Clamp(position.y, MinY, MaxY);

            var scroll = Input.GetAxis("Mouse ScrollWheel");
            position.y -= scroll * ScrollSpeed * 100f * Time.deltaTime;


            transform.position = position;
        }
    }
}
