using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimit : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    public Vector3 offset;
    
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -8.3f, 6.9f),
            Mathf.Clamp(transform.position.y, -11f, 10.4f),
            transform.position.z);
    }

    private void FixedUpdate()
    {
        Vector3 posCamera = target.localPosition + offset;
        Vector3 smoothCam = Vector3.Lerp(transform.position, posCamera, smoothSpeed);
        transform.position = smoothCam;
    }
}
