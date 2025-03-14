using UnityEngine;
using TMPro; // Required for TextMeshPro components
using System.Collections; // Required for IEnumerator

public class HealthyFoodInteract : MonoBehaviour
{
    public GameObject pizza; // Unhealthy food
    public GameObject burger; // Unhealthy food
    public GameObject salad; // Healthy food
    public GameObject fence;
    public GameObject player;
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt;

    private GameObject[] foods;
    private GameObject healthyFood;
    private bool taskCompleted = false; // Flag to check if the task is completed

    void Start()
    {
        foods = new GameObject[] { pizza, burger, salad };
        healthyFood = salad; // Set the healthy food

        // Initially set all foods to be invisible
        foreach (var food in foods)
        {
            food.SetActive(false);
        }

        // Choose a random food prompt (always ask for the healthy food)
        ShowCanvas("Choose the healthy food!");
    }

    void Update()
    {
        if (taskCompleted) return; // Skip further checks if the task is completed

        bool isWithinRange = false;

        // Check the distance between the player and each food
        foreach (var food in foods)
        {
            float distance = Vector3.Distance(food.transform.position, player.transform.position);

            if (distance < 5f) // Food becomes visible when the player is near
            {
                canvas.SetActive(true
                ); // Show the canvas
                isWithinRange = true; // Mark that the player is in range
                prompt.SetActive(true);
                ShowCanvas("Choose the healthy food!");

                foreach (var foodItem in foods)
                {
                    foodItem.SetActive(true);
                }
            }

            // Check if the player is close to the healthy food
            if (distance < 1f && food == healthyFood)
            {
                taskCompleted = true; // Mark the task as completed
                ShowCanvas("Good Job! You chose the healthy food!");

                foreach (var foodItem in foods)
                {
                    Destroy(foodItem);
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