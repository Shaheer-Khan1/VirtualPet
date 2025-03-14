using UnityEngine;
using TMPro; // Required for TextMeshPro components
using System.Collections; // Required for IEnumerator

public class StarInteract : MonoBehaviour
{
    public GameObject smallStar; // Small star
    public GameObject MediumStar; // medium star
    public GameObject largeStar; // Large star
    public GameObject fence;
    public GameObject player;
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt;

    private GameObject[] stars;
    private string promptedSize; // Stores the size prompted to the player
    private bool taskCompleted = false; // Flag to check if the task is completed

    void Start()
    {
        stars = new GameObject[] { smallStar, MediumStar, largeStar };

        // Initially set all stars to be invisible
        foreach (var star in stars)
        {
            star.SetActive(false);
        }

        // Randomly choose a size to prompt the player
        string[] sizes = { "small", "medium", "large" };
        int randomIndex = Random.Range(0, sizes.Length);
        promptedSize = sizes[randomIndex];

        // Show the prompt on the canvas
        ShowCanvas("Choose the " + promptedSize + " star!");
    }

    void Update()
    {
        if (taskCompleted) return; // Skip further checks if the task is completed

        bool isWithinRange = false;

        // Check the distance between the player and each star
        foreach (var star in stars)
        {
            float distance = Vector3.Distance(star.transform.position, player.transform.position);

            if (distance < 5f) // Star becomes visible when the player is near
            {
                isWithinRange = true; // Mark that the player is in range
                prompt.SetActive(true);
                ShowCanvas("Choose the " + promptedSize + " star!");

                foreach (var starItem in stars)
                {
                    starItem.SetActive(true);
                }
            }

            // Check if the player is close to the correct star
            if (distance < 1f && star.name.ToLower().Contains(promptedSize))
            {
                taskCompleted = true; // Mark the task as completed
                ShowCanvas("Good Job! You chose the " + promptedSize + " star!");

                foreach (var starItem in stars)
                {
                    Destroy(starItem);
                }
                Destroy(fence);

                // Add a wait here before hiding the prompt and deactivating the game object
                StartCoroutine(CompleteTaskWithDelay(2f)); // 2-second delay
                return;
            }
        }

        // Hide the prompt and canvas if the player is out of range
        if (!isWithinRange)
        {
            prompt.SetActive(false);
        }
    }

    // Method to show and update the canvas text
    void ShowCanvas(string message)
    {
        if (canvas != null)
        {
            canvas.SetActive(true); // Show the canvas
        }
        if (consoleText != null)
        {
            consoleText.text = message;
        }
    }

    // Coroutine to wait before completing the task
    private IEnumerator CompleteTaskWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        if (prompt != null)
        {
            prompt.SetActive(false);
        }
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
        gameObject.SetActive(false); // Deactivate this script's GameObject
    }
}