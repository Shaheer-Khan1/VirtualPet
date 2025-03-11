using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public float moveSpeed = 4f; // Movement speed of the player
    public float rotationSpeed = 700f; // Rotation speed of the player
    public float gravity = -9.81f; // Gravity effect

    private Vector3 velocity; // To store velocity for gravity

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); // Assuming an animator is attached for animations
    }

    void Update()
    {
        // Handle movement and rotation
        HandleMovement();
    }

    // Method to handle player movement and rotation
    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D for left/right rotation
        float vertical = Input.GetAxisRaw("Vertical"); // W for forward movement

        // Handle rotation using the horizontal input (A/D)
        if (horizontal != 0)
        {
            float rotationInput = horizontal; // A = -1, D = 1
            float targetAngle = transform.eulerAngles.y + rotationInput * rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }

        // Move the player forward only using the W key
        if (vertical > 0)
        {
            Vector3 moveDirection = transform.forward * moveSpeed * Time.deltaTime;
            controller.Move(moveDirection);
        }

        // Gravity handling
        if (controller.isGrounded)
        {
            velocity.y = -2f; // Small negative value to keep player grounded
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);

        // Set the animator speed based on movement
        animator.SetFloat("Speed", vertical);
    }
}
