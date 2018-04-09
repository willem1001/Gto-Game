using System.Collections;
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

            var scroll = Input.GetAxis("Mouse ScrollWheel");
            position.y -= scroll * ScrollSpeed * 100f * Time.deltaTime;

            if (position.x > PanLimitMax.x || position.x < PanLimitMin.x)
            {
                position.x = transform.position.x;
            }

            if (position.z > PanLimitMax.y || position.z < PanLimitMin.y)
            {
                position.z = transform.position.z;
            }

            if (position.y < 5 || position.y > 20)
            {
                position.y = transform.position.y;
            }

            transform.position = position;
        }

        public void SwitchTurn()
        {
            Vector3 cameraVector3 = transform.position;
            
            cameraVector3.y = 17;
            
            if ((int)this.gameObject.transform.rotation.eulerAngles.y == 0)
            {
                Debug.Log(PanLimitMax.x);
                cameraVector3.z = PanLimitMax.y;
                cameraVector3.x = middle.x - 5;
                StartCoroutine(Rotate(3, 180, cameraVector3));
            }
            else if ((int)this.gameObject.transform.rotation.eulerAngles.y == 180)
            {
                cameraVector3.z = PanLimitMin.y;
                cameraVector3.x = middle.x + 5;
                StartCoroutine(Rotate(3, -180, cameraVector3));
            }

//            transform.position = cameraVector3;
        }

        IEnumerator Rotate(float duration, float amount, Vector3 endPoint)
        {
            float startRotation = transform.eulerAngles.y;
            float endRotation = startRotation + amount;
            float t = 0.0f;
            Vector3 add = new Vector3();
            float movement = Vector3.Distance(transform.position, endPoint) / duration;
            while (t < duration)
            {
                add = transform.eulerAngles;
                add.y += (endRotation - startRotation) / duration * Time.deltaTime;
                transform.eulerAngles = add;

                transform.position = Vector3.MoveTowards(transform.position, endPoint, movement * Time.deltaTime);

                t += Time.deltaTime;
                yield return null;
            }

            add.y = endRotation;
            transform.eulerAngles = add;
        }
    }
}
