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
            Mathf.Clamp(transform.position.x, -3.7f, 3.5f),
            Mathf.Clamp(transform.position.y, -2.3f, 7.3f),
            transform.position.z);
    }

    private void FixedUpdate()
    {
        Vector3 posCamera = target.localPosition + offset;
        Vector3 smoothCam = Vector3.Lerp(transform.position, posCamera, smoothSpeed);
        transform.position = smoothCam;
    }
}
