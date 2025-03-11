using UnityEngine;
using TMPro; // Required for TextMeshPro components
using System.Collections; // Required for IEnumerator
using UnityEngine.EventSystems; // Required for detecting clicks

public class MathInteraction : MonoBehaviour
{
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject fence;
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt;

    private GameObject[] numbers;
    private int correctAnswer;
    private bool taskCompleted = false; // Flag to check if the task is completed

    void Start()
    {
        numbers = new GameObject[] { three, four, five };

        // Initially set all numbers to be invisible
        foreach (var numObj in numbers)
        {
            numObj.SetActive(false);
            BoxCollider collider = numObj.AddComponent<BoxCollider>();
            collider.isTrigger = false; // Ensure it is not a trigger
        }

        // Generate a simple math question that results in 3, 4, or 5
        int a = Random.Range(1, 3);
        int b = Random.Range(2, 4);
        correctAnswer = a + b;
        ShowCanvas($"Solve: {a} + {b} = ?");
        Debug.Log("Correct Answer: " + correctAnswer);
    }

    void Update()
    {
        if (taskCompleted) return; // Skip further checks if the task is completed

        bool isWithinRange = false;

        foreach (var numObj in numbers)
        {
            float distance = Vector3.Distance(numObj.transform.position, Camera.main.transform.position);
            Debug.Log("Distance to " + numObj.name + ": " + distance);

            if (distance < 5f) // Object becomes visible when the player is near
            {
                isWithinRange = true; // Mark that the player is in range
                prompt.SetActive(true);

                foreach (var obj in numbers)
                {
                    obj.SetActive(true);
                }
            }
        }

        if (!isWithinRange)
        {
            prompt.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.name == correctAnswer.ToString())
                {
                    taskCompleted = true;
                    ShowCanvas("Good Job!");
                    Debug.Log("Correct answer clicked: " + clickedObject.name);

                    foreach (var obj in numbers)
                    {
                        Destroy(obj);
                    }
                    Destroy(fence);
                    StartCoroutine(CompleteTaskWithDelay(2f));
                }
            }
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
        Debug.Log("Canvas Message: " + message);
    }

    // Coroutine to wait before completing the task
    private IEnumerator CompleteTaskWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        if (prompt != null)
        {
            prompt.SetActive(false);
        }
        Debug.Log("Task completed, deactivating object.");
        gameObject.SetActive(false); // Deactivate this script's GameObject
    }
}
