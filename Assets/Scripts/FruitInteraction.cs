using UnityEngine;
using TMPro; // Required for TextMeshPro components
using System.Collections; // Required for IEnumerator
using UnityEngine.EventSystems; // Required for detecting clicks

public class FruitInteraction : MonoBehaviour
{
    public GameObject orange;
    public GameObject grape;
    public GameObject banana;
    public GameObject watermelon;
    public GameObject fence;
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt;

    private GameObject[] fruits;
    private string promptFruit;
    private bool taskCompleted = false; // Flag to check if the task is completed

    void Start()
    {
        fruits = new GameObject[] { orange, grape, banana, watermelon };

        // Initially set all fruits to be invisible
        foreach (var fruitObj in fruits)
        {
            fruitObj.SetActive(false);
            BoxCollider collider = fruitObj.AddComponent<BoxCollider>();
            collider.isTrigger = false; // Ensure it is not a trigger
        }

        // Choose a random fruit prompt
        int randomIndex = Random.Range(0, fruits.Length);
        promptFruit = fruits[randomIndex].name.ToLower();
        Debug.Log("Prompt Fruit: " + promptFruit);
    }

    void Update()
    {
        if (taskCompleted) return; // Skip further checks if the task is completed

        bool isWithinRange = false;

        foreach (var fruitObj in fruits)
        {
            float distance = Vector3.Distance(fruitObj.transform.position, Camera.main.transform.position);
            Debug.Log("Distance to " + fruitObj.name + ": " + distance);

            if (distance < 10f) // Object becomes visible when the player is near
            {
                isWithinRange = true; // Mark that the player is in range
                prompt.SetActive(true);
                ShowCanvas("Click the fruit: " + promptFruit);

                foreach (var obj in fruits)
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
                if (clickedObject.name.ToLower() == promptFruit)
                {
                    taskCompleted = true;
                    ShowCanvas("Good Job!");
                    Debug.Log("Correct fruit clicked: " + clickedObject.name);

                    foreach (var obj in fruits)
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
