using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [Header("Camera Movement")]
    public Transform lookAt;
    public Vector3 cameraOffset;
    public float cameraSmoothSpeed;
    public bool cameraPanOut;
    public bool cameraPanIn;

    [Header("Interaction")]
    public Vector2 mousePositionOnScreen;
    public Vector3 mousePositionRaw;
    public GameObject hit;
    public KeyCode InteractionButton;
    public float mouseRayDistance;

    private Vector3 originalCameraPosition;
    private Vector3 pannedInCameraPosition;

    private Vector3 mousePositionFromCamera;
    private Vector3 objectPositionToScreenWorldPoint;

    private RaycastHit mouseHit;
    private Ray mouseRay;
    private LayerMask interactiveMask;

    private bool canRotate;

    private void Awake()
    {
        originalCameraPosition = Camera.main.transform.position;
        pannedInCameraPosition = new Vector3(0, 0, -5f);
        interactiveMask = LayerMask.GetMask("Interactive");
    }

    private void Update()
    {
        //DebugRaycast();
        mousePositionRaw = new Vector3(Input.mousePosition.x, Input.mousePosition.y,5f);
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        objectPositionToScreenWorldPoint = Camera.main.ScreenToWorldPoint(mousePositionRaw);

        if (PlayerInteraction() == true)
        {
            hitObject();
            if (Input.GetKey(InteractionButton))
            {
                if (hitObject().transform.tag == "HorizontalBlock")
                {
                    objectPositionToScreenWorldPoint.y = hit.transform.position.y;
                    hit.transform.position = objectPositionToScreenWorldPoint;
                }

                if (hitObject().transform.tag == "VerticalBlock")
                {
                    objectPositionToScreenWorldPoint.x = hit.transform.position.x;
                    hit.transform.position = objectPositionToScreenWorldPoint;
                }
            }
        }
    }

    private void DebugRaycast()
    {
        mousePositionFromCamera = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawRay(mousePositionFromCamera, mouseRay.direction * mouseRayDistance, Color.green);
    }
    
    public bool PlayerInteraction()
    {
        return Physics.Raycast(mouseRay, out mouseHit, mouseRayDistance, interactiveMask);
    }

    public GameObject hitObject()
    {
        hit = mouseHit.transform.gameObject;
        return hit;
    }
}
