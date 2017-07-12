using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject hit;
    public KeyCode InteractionButton;
    public float mouseRayDistance;
    public bool debugRayEnable;

    private RaycastHit mouseHit;
    private Ray mouseRay;
    private LayerMask interactiveMask;

    private Vector3 mousePositionRaw;
    private Vector3 mousePositionFromCamera;
    private Vector3 objectPositionToScreenWorldPoint;

    private bool canRotateHorizontalBlock;
    private bool canRotateVerticalBlock;

    private void Awake()
    {
        interactiveMask = LayerMask.GetMask("Interactive");
    }

    private void Update()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        mousePositionRaw = new Vector3(Input.mousePosition.x, Input.mousePosition.y,6.9f);  // not flexible
        objectPositionToScreenWorldPoint = Camera.main.ScreenToWorldPoint(mousePositionRaw);

        if (debugRayEnable == true)
        {
            DebugRaycast();
        }

        if (PlayerInteraction() == true)
        {
            hitObject();
            if (Input.GetKey(InteractionButton))
            {
                if (hit.transform.tag == "HorizontalBlock")
                {
                    objectPositionToScreenWorldPoint.y = hit.transform.position.y;
                    hit.transform.position = objectPositionToScreenWorldPoint;

                    canRotateHorizontalBlock = true;
                    if (Input.GetKey(KeyCode.E))
                    {
                        if (canRotateHorizontalBlock == true)
                        {
                            hit.transform.Rotate(0, 0, -1.5f);
                        }
                    }
                    //else { Debug.Log("Debug: Can't rotate HBlock -1.5f"); }
                    if (Input.GetKey(KeyCode.Q))
                    {
                        if (canRotateHorizontalBlock == true)
                        {
                            hit.transform.Rotate(0, 0, 1.5f);
                        }
                    }
                    //else { Debug.Log("Debug: Can't rotate HBlock 1.5f"); }
                }

                if (hit.transform.tag == "VerticalBlock")
                {
                    objectPositionToScreenWorldPoint.x = hit.transform.position.x;
                    hit.transform.position = objectPositionToScreenWorldPoint;

                    canRotateVerticalBlock = true;
                    if (Input.GetKey(KeyCode.E))
                    {
                        if (canRotateVerticalBlock == true)
                        {
                            hit.transform.Rotate(0, 0, -1.5f);
                        }
                    }
                    //else { Debug.Log("Debug: Can't rotate VBlock -1.5f"); }
                    if (Input.GetKey(KeyCode.Q))
                    {
                        if (canRotateVerticalBlock == true)
                        {
                            hit.transform.Rotate(0, 0, 1.5f);
                        }
                    }
                    //else { Debug.Log("Debug: Can't rotate VBlock 1.5f"); }
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
