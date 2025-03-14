using UnityEngine;
using TMPro; // Required for TextMeshPro components
using System.Collections; // Required for IEnumerator

public class FruitInteract : MonoBehaviour
{
    public GameObject grape;
    public GameObject orange;
    public GameObject banana;
    public GameObject watermelon;
    public GameObject fence;
    public GameObject player;
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt;

    private GameObject[] fruits;
    private string promptFruit;
    private bool taskCompleted = false; // Flag to check if the task is completed

    void Start()
    {
        fruits = new GameObject[] { grape, orange, banana, watermelon };

        // Initially set all fruits to be invisible
        foreach (var fruit1 in fruits)
        {
            fruit1.SetActive(false);
        }

        

        // Choose a random fruit prompt
        int randomIndex = Random.Range(0, fruits.Length);
        promptFruit = fruits[randomIndex].name.Substring(0, 1).ToLower();
    }

    void Update()
    {
        if (taskCompleted) return; // Skip further checks if the task is completed

        bool isWithinRange = false;

        // Check the distance between the player and each fruit
        foreach (var fruit in fruits)
        {
            float distance = Vector3.Distance(fruit.transform.position, player.transform.position);

            if (distance < 5f) // Fruit becomes visible when the player is near
            {
                isWithinRange = true; // Mark that the player is in range
                prompt.SetActive(true);
                ShowCanvas("Choose the fruit that starts with the letter: " + promptFruit);

                foreach (var fruit1 in fruits)
                {
                    fruit1.SetActive(true);
                }
            }

            if (distance < 1f && fruit.name.StartsWith(promptFruit))
            {
                taskCompleted = true; // Mark the task as completed
                ShowCanvas("Good Job!");

                foreach (var fruit1 in fruits)
                {
                    Destroy(fruit1);
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
        }
        gameObject.SetActive(false); // Deactivate this script's GameObject
    }
}
