using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public float speed = 5f;
    public GameObject doorObject;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private Rigidbody rb;
    public SwitchController switchController; 
    public bool startOpen = false;
    public bool isOpen = false;

    void Start()
    {
        initialPosition = doorObject.transform.position;
        targetPosition = initialPosition + new Vector3(0f, 30f, 0f);
        rb = doorObject.GetComponent<Rigidbody>();

        if (startOpen)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }
    }

    public void ToggleDoor(bool open)
    {
        isOpen = open;
    }

    void FixedUpdate()
    {
        if (isOpen)
        {
            if (doorObject.transform.position.y < targetPosition.y)
            {
                OpenDoor();
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
                doorObject.transform.position = targetPosition;
            }
        }
        else
        {
            if (doorObject.transform.position.y > initialPosition.y)
            {
                CloseDoor();
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
                doorObject.transform.position = initialPosition;
            }
        }
    }

    void OpenDoor()
    {
        rb.linearVelocity = Vector3.up * speed;
    }

    void CloseDoor()
    {
        rb.linearVelocity = Vector3.down * speed;
    }

    void OnCollisionStay(Collision collision)
    {
        if (!isOpen && collision.gameObject.CompareTag("Boxes"))
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}