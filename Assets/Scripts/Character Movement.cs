using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float jumpForce = 20f;
    public float gravity = 20f;
    public float mouseSensitivity = 200f;

    private CharacterController controller;
    private float verticalSpeed = 0f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement = transform.TransformDirection(movement);
        movement *= moveSpeed;

        // Jumping
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            verticalSpeed = jumpForce;
        }

        // Gravity
        if (!controller.isGrounded)
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
        else if (verticalSpeed < 0)
        {
            verticalSpeed = 0f;
        }

        movement.y = verticalSpeed;

        controller.Move(movement * Time.deltaTime);

        // Camera rotation
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        transform.localRotation = Quaternion.Euler(0, rotationX, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(-rotationY, 0, 0);
    }
}