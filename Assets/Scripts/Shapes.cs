using UnityEngine;
using TMPro; // Required for TextMeshPro components
using System.Collections; // Required for IEnumerator
using UnityEngine.EventSystems; // Required for detecting clicks

public class ShapeInteraction : MonoBehaviour
{
    public GameObject sphere;
    public GameObject cube;
    public GameObject fence;
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt; // Reference to the "info" GameObject (UI)

    private GameObject[] shapes;
    private string promptShape;
    private bool taskCompleted = false; // Flag to check if the task is completed

    void Start()
    {
        shapes = new GameObject[] { sphere, cube };

        // Initially set all shapes to be invisible and add colliders
        foreach (var shapeObj in shapes)
        {
            shapeObj.SetActive(false);
            if (shapeObj.GetComponent<BoxCollider>() == null)
            {
                BoxCollider collider = shapeObj.AddComponent<BoxCollider>();
                collider.isTrigger = false;
            }
        }

        // Choose a random shape prompt
        int randomIndex = Random.Range(0, shapes.Length);
        promptShape = shapes[randomIndex].name.ToLower();
        Debug.Log("Prompt Shape: " + promptShape);
    }

    void Update()
    {
        if (taskCompleted) return; // Skip further checks if the task is completed

        bool isWithinRange = false;
        Vector3 playerPosition = Camera.main.transform.position;

        // Calculate distances even if objects are inactive
        foreach (var shapeObj in shapes)
        {
            float distance = Vector3.Distance(shapeObj.transform.position, playerPosition);
            Debug.Log("Distance to " + shapeObj.name + ": " + distance);

            if (distance < 5f) // Player is in range
            {
                isWithinRange = true;
                break;
            }
        }

        // Handle UI visibility based on range
        if (isWithinRange)
        {
            // Activate shapes
            foreach (var obj in shapes)
            {
                obj.SetActive(true);
            }

            // Ensure prompt is visible
            if (prompt != null)
            {
                prompt.SetActive(true);
                Debug.Log("Prompt activated: " + (prompt.activeSelf ? "visible" : "hidden"));

                // Ensure its parent is also active
                if (prompt.transform.parent != null)
                {
                    prompt.transform.parent.gameObject.SetActive(true);
                    Debug.Log("Prompt parent activated: " + prompt.transform.parent.gameObject.activeSelf);
                }
            }
            else
            {
                Debug.LogError("Prompt reference is null!");
            }

            // Ensure canvas and text are visible
            if (canvas != null)
            {
                canvas.SetActive(true);
                Debug.Log("Canvas activated: " + (canvas.activeSelf ? "visible" : "hidden"));

                if (consoleText != null)
                {
                    consoleText.text = "Click the shape: " + promptShape;
                    consoleText.gameObject.SetActive(true); // Explicitly activate text object
                    Debug.Log("Console text updated: " + consoleText.text);
                }
                else
                {
                    Debug.LogError("Console text reference is null!");
                }
            }
            else
            {
                Debug.LogError("Canvas reference is null!");
            }
        }
        else
        {
            // Hide UI elements when not in range
            if (prompt != null)
            {
                prompt.SetActive(false);
            }

            if (canvas != null)
            {
                canvas.SetActive(false);
            }
        }

        // Process click detection when in range
        if (isWithinRange && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                Debug.Log("Clicked on: " + clickedObject.name);

                if (clickedObject.name.ToLower() == promptShape)
                {
                    taskCompleted = true;

                    // Ensure UI is visible for success message
                    if (canvas != null)
                    {
                        canvas.SetActive(true);

                        if (consoleText != null)
                        {
                            consoleText.text = "Good Job!";
                            consoleText.gameObject.SetActive(true);
                        }
                    }

                    Debug.Log("Correct shape clicked: " + clickedObject.name);

                    foreach (var obj in shapes)
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
