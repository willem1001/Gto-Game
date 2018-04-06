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

        public float MinY = 0f;
        public float MaxY = 40f;
        public NewMap Map;
        public Vector3 middle;
        public float mapWidth;
        public float mapHeigth;
        float startWidthx;
        float centerx;
        float startWidthy;
        float centery;


        public void Start()
        {
                //startWidthx = MapGameObject.GetComponentInChildren<NewMap>().width;
                //centerx = startWidthx / 2;
                //PanLimitMin.x = centerx;
                //PanLimitMax.x = centerx + startWidthx;
                //startWidthy = MapGameObject.GetComponentInChildren<NewMap>().height;
                //centery = startWidthy / 2;
                //PanLimitMin.y = centery - startWidthy;
                //PanLimitMax.y = centery * 2f;
        }

        public void CameraSetup()
        {

            middle = Map.MiddleHex.transform.position;
            mapHeigth = Map.Endhex.transform.position.z - Map.StartHex.transform.position.z;
            mapWidth = Map.Endhex.transform.position.x - Map.StartHex.transform.position.x;

            PanLimitMin.x = middle.x - mapWidth / 4;
            PanLimitMax.x = middle.x + mapWidth / 4;
            PanLimitMin.y = middle.z - mapHeigth / 2;
            PanLimitMax.y = middle.z + mapHeigth / 2;

            Vector3 cameraVector3 = transform.position;
            cameraVector3.x = middle.x;
            cameraVector3.z = middle.z;
            transform.position = cameraVector3;

            Debug.Log("Max" + PanLimitMax.x + " " + PanLimitMax.y);
            Debug.Log("Min" + PanLimitMin.x + " " + PanLimitMin.y);

        }
        // Update is called once per frame
        public void Update()
        {
            Vector3 position = transform.position;
            if ((int)this.gameObject.transform.rotation.eulerAngles.y == 0)
            {
                if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - PanBorderThickness)
                {
                    position.z += PanSpeed * Time.deltaTime;
                }

                if (Input.GetKey("s") || Input.mousePosition.y <= PanBorderThickness)
                {
                    position.z -= PanSpeed * Time.deltaTime;
                }

                if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - PanBorderThickness)
                {
                    position.x += PanSpeed * Time.deltaTime;
                }
                if (Input.GetKey("a") || Input.mousePosition.x <= PanBorderThickness)
                {
                    position.x -= PanSpeed * Time.deltaTime;
                }
            }
            else if ((int)this.gameObject.transform.rotation.eulerAngles.y == 180)
            {
                if (Input.GetKey("w") || Input.mousePosition.y <= Screen.height - PanBorderThickness)
                {
                    position.z += PanSpeed * Time.deltaTime;
                }

                if (Input.GetKey("s") || Input.mousePosition.y >= PanBorderThickness)
                {
                    position.z -= PanSpeed * Time.deltaTime;
                }

                if (Input.GetKey("d") || Input.mousePosition.x <= Screen.width - PanBorderThickness)
                {
                    position.x += PanSpeed * Time.deltaTime;
                }
                if (Input.GetKey("a") || Input.mousePosition.x >= PanBorderThickness)
                {
                    position.x -= PanSpeed * Time.deltaTime;
                }
            }

            //position.x = Mathf.Clamp(position.x, PanLimitMin.x, PanLimitMax.x);
            //position.z = Mathf.Clamp(position.z, PanLimitMin.y, PanLimitMax.y);
            //position.y = Mathf.Clamp(position.y, MinY, MaxY);

            var scroll = Input.GetAxis("Mouse ScrollWheel");
            position.y -= scroll * ScrollSpeed * 100f * Time.deltaTime;

            Debug.Log(position.x);
            Debug.Log(PanLimitMax.x);

            if (position.x > PanLimitMax.x || position.x < PanLimitMin.x)
            {
                position.x = transform.position.x;
            }

            if (position.z > PanLimitMax.y || position.z < PanLimitMin.y)
            {
                position.z = transform.position.z;
            }

            transform.position = position;
        }

        public void SwitchTurn()
        {
            Vector3 cameraVector3 = transform.position;
            cameraVector3.x = middle.x;
            cameraVector3.z = middle.z;
            transform.position = cameraVector3;

            if ((int)this.gameObject.transform.rotation.eulerAngles.y == 0)
            {
                Vector3 start = this.gameObject.transform.eulerAngles;
                start.y = 180;
                this.gameObject.transform.eulerAngles = start;
            }
            else if ((int)this.gameObject.transform.rotation.eulerAngles.y == 180)
            {
                Vector3 start = this.gameObject.transform.eulerAngles;
                start.y = 0;
                this.gameObject.transform.eulerAngles = start;
            }
        }
    }
}
