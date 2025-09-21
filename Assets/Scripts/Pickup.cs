using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup: MonoBehaviour
{
    public GameObject heldObject;
    public Transform PlayerTransform;
    public float range = 10f;
    public Camera Camera;
    public LayerMask pickupLayer;

    void Start()
    {
        pickupLayer = LayerMask.GetMask("Pickup");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && heldObject == null)
        {
            pickupObject();
        }

        if (Input.GetButtonUp("Fire1") && heldObject != null)
        {
            Drop();
        }
    }

    void pickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range, pickupLayer))
        {
            heldObject = hit.transform.gameObject;
            heldObject.GetComponent<Rigidbody>().isKinematic = true;
            heldObject.transform.SetParent(PlayerTransform);
        }
    }

    void Drop()
    {
        heldObject.transform.SetParent(null);
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject = null;
    }
}