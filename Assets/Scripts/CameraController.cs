using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAt;
    public Vector3 cameraOffset;
    public float cameraSmoothSpeed;
    public bool cameraPanIn;

    private Vector3 originalCameraPosition;
    private Vector3 pannedInCameraPosition;

    void Start ()
    {
        originalCameraPosition = Camera.main.transform.position;
    }

    private void Update()
    {
        if (cameraPanIn == true)
        {
            pannedInCameraPosition = lookAt.transform.position + cameraOffset;
            transform.position = Vector3.Lerp(transform.position, pannedInCameraPosition, Time.deltaTime * cameraSmoothSpeed);
        }
    }
}
