using UnityEngine;

namespace Assets.Scripts.Map
{
    public class CameraController : MonoBehaviour
    {
        public float PanSpeed = 20f;
        public float PanBorderThickness = 10f;

        public Vector2 PanLimit;
        public float ScrollSpeed = 20f;

        public float MinY =0f;
        public float MaxY=40f;
        // Update is called once per frame
        public void Update()
        {
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

            position.x = Mathf.Clamp(position.x, -PanLimit.x, PanLimit.x);
            position.z = Mathf.Clamp(position.z, -PanLimit.y, PanLimit.y);
            position.y = Mathf.Clamp(position.y, MinY, MaxY);

            var scroll = Input.GetAxis("Mouse ScrollWheel");
            position.y -= scroll * ScrollSpeed * 100f * Time.deltaTime;


            transform.position = position;
        }
    }
}
