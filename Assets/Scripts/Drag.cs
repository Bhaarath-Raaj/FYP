using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    public GameObject heldObject;
    public Transform PlayerTransform;
    public float range = 10f;
    public Camera Camera;
    public LayerMask dragLayer;
    public LayerMask obstacleLayer; // Obstacle layer mask
    private Vector3 offset;
    private float initialYPosition;

    void Start()
    {
        dragLayer = LayerMask.GetMask("Drag");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && heldObject == null)
        {
            DragObject();
        }

        if (Input.GetButtonUp("Fire1") && heldObject != null)
        {
            Release();
        }

        if (heldObject != null)
        {
            Vector3 targetPosition = PlayerTransform.position + offset;
            targetPosition.y = initialYPosition;

            // Check for obstacles
            if (IsValidPosition(targetPosition))
            {
                heldObject.transform.position = targetPosition;
            }
            else
            {
                // If there's an obstacle, move the object as close as possible
                Vector3 direction = (targetPosition - heldObject.transform.position).normalized;
                float distance = Vector3.Distance(heldObject.transform.position, targetPosition);
                RaycastHit hit;
                if (Physics.Raycast(heldObject.transform.position, direction, out hit, distance, obstacleLayer))
                {
                    if (hit.transform.gameObject != heldObject)
                    {
                        heldObject.transform.position = hit.point - direction * 0.01f; // Move the object slightly away from the obstacle
                    }
                    else
                    {
                        heldObject.transform.position = targetPosition;
                    }
                }
            }
        }
    }

    bool IsValidPosition(Vector3 position)
    {
        // Check if there are any obstacles at the target position
        Collider[] hits = Physics.OverlapBox(position, heldObject.transform.localScale / 2, Quaternion.identity, obstacleLayer);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject != heldObject && hit.gameObject != gameObject)
            {
                return false;
            }
        }
        return true;
    }

    void DragObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range, dragLayer))
        {
            heldObject = hit.transform.gameObject;
            initialYPosition = heldObject.transform.position.y;
            offset = heldObject.transform.position - PlayerTransform.position;
            heldObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void Release()
    {
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject = null;
    }
}