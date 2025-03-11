using UnityEngine;

public class HorseAnimation : MonoBehaviour
{
    private Animator animator;  // Reference to the Animator
    private float speed;        // The speed of the cow
    private const float walkThreshold = 0.1f; // Threshold to determine when to switch to Walk
    private bool isWalking = false; // To check if the cow is currently walking
    private float turnSpeed = 10f; // The speed at which the cow turns
    public Transform player;  // Reference to the player's Transform
    public float offsetSide = 2f;  // Distance to the left of the player

    void Start()
    {
        // Get the Animator component attached to the cow
        animator = GetComponent<Animator>();

        // Check if the Animator is attached
        if (animator == null)
        {
            Debug.LogError("Animator not found on the cow object!");
        }

        // Ensure the player reference is set
        if (player == null)
        {
            Debug.LogError("Player reference not set on CowAnimation script!");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Get input from the player (this could be replaced by your own movement logic)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate speed based on movement input (this could be more complex depending on your setup)
        speed = new Vector3(horizontal, 0, vertical).magnitude;

        // Set the 'Speed' parameter in the Animator based on the calculated speed
        animator.SetFloat("Speed", speed);

        // Rotate the cow based on the player's rotation
        if (speed > walkThreshold)
        {
            // If moving, rotate the cow towards the player's current rotation
            Vector3 movementDirection = new Vector3(horizontal, 0, vertical).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

            // If the cow is moving and we are not already in the Walk state, restart the Walk animation
            if (!isWalking)
            {
                animator.Play("Walk", 0, 0f);  // Play Walk animation from the start
                isWalking = true;
            }
        }
        else
        {
            // If the cow is not moving and we are not already in the Idle state, restart the Idle animation
            if (isWalking)
            {
                animator.Play("Idle", 0, 0f);  // Play Idle animation from the start
                isWalking = false;
            }
        }

        // Position the cow to the left of the player
        Vector3 offset = new Vector3(-offsetSide, 0, 0); // Adjust side distance (left of the player)
        transform.position = player.position + player.TransformDirection(offset);

        // Follow the player's full 360-degree rotation (y-axis only)
        transform.rotation = Quaternion.Euler(0, player.eulerAngles.y, 0);
    }
}
