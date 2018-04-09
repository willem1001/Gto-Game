using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour
{
    public GameObject MainCamera;

     void Update()
    {
            transform.LookAt(transform.position + MainCamera.transform.rotation * Vector3.forward,
            MainCamera.transform.rotation * Vector3.up);      
    }

  
}