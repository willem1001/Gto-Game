using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour
{
    public Camera camera;

    public void Update()
    {
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward,
            camera.transform.rotation * Vector3.up);
    }
}