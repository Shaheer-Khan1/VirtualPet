using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public Vector3 offset = new Vector3(0f, 5f, -10f);  // Default offset for the camera
    public float smoothSpeed = 0.125f;  // Smooth speed for the camera's movement

    private void LateUpdate()
    {
        // Calculate the target position behind the player, adjusted by the player's rotation
        Vector3 targetPosition = player.position + player.rotation * offset;

        // Smoothly move the camera to the target position
        transform.position = Vector3.Slerp(transform.position, targetPosition, smoothSpeed);

        // Keep the camera always looking at the player
        transform.LookAt(player);

    }
}