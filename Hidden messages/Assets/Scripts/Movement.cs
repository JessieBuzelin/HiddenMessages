using UnityEngine;

public class CrashBandicootMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the character
    public float jumpForce = 10f; // Force applied when jumping
    public LayerMask groundLayer; // Layer to check for ground
    public Transform groundCheck; // Position to check if the character is on the ground
    public Transform cameraTransform; // Reference to the camera

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    private void Update()
    {
        // Check if the character is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        // Handle forward movement with "W" key and backward with "S"
        if (Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveBackward();
        }

        // Handle left and right movement
        float horizontalInput = Input.GetAxis("Horizontal");
        MoveHorizontal(horizontalInput);

        // Handle jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void MoveForward()
    {
        Vector3 forwardMovement = transform.forward * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }

    private void MoveBackward()
    {
        Vector3 backwardMovement = -transform.forward * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + backwardMovement);
    }

    private void MoveHorizontal(float horizontalInput)
    {
        Vector3 horizontalMovement = transform.right * horizontalInput * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + horizontalMovement);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void LateUpdate()
    {
        // Camera follows the player
        if (cameraTransform != null)
        {
            Vector3 cameraPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z - 5);
            cameraTransform.position = cameraPosition;
            cameraTransform.LookAt(transform);
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the ground check area in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, 0.1f);
    }
}