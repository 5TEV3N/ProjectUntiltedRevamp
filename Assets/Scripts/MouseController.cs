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
    public GameObject hit;
    public KeyCode InteractionButton;
    public float mouseRayDistance;

    public Vector3 mousePositionRaw;
    private Vector3 mousePositionFromCamera;
    private Vector3 originalCameraPosition;
    private Vector3 pannedInCameraPosition;
    private RaycastHit mouseHit;
    private Ray mouseRay;
    private LayerMask interactiveMask;

    private void Awake()
    {
        originalCameraPosition = Camera.main.transform.position;
        interactiveMask = LayerMask.GetMask("Interactive");
    }

    private void Update()
    {
        DebugRaycast();
        mousePositionRaw = new Vector3(Input.mousePosition.x, Input.mousePosition.y,5f);
        Vector3 objectPositionToScreenWorldPoint = Camera.main.ScreenToWorldPoint(mousePositionRaw);

        if (PlayerInteraction() == true)
        {
            hitObject();

            if (Input.GetKey(InteractionButton))
            {
                if (hitObject().transform.tag == "HorizontalBlock")
                {
                    hit.transform.position = objectPositionToScreenWorldPoint;

                }

                if (hitObject().transform.tag == "VerticalBlock")
                {

                }
            }
        }

    }

    private void DebugRaycast()
    {
        mousePositionFromCamera = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
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
/*
 *      Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); //Make a new Vector 3 which takes the position of your mouse's xy co ordinance
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); //Make a Vector 3 value that takes the mouse position relative to your screen
        objPosition.y = transform.position.y; // limit from going to left to right
        transform.position = objPosition; // Transform the object to objPosition
        CanRotate = true;
        print("Mouse Drag of Horizontal Platform is True");
*/
