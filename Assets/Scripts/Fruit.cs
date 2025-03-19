/*
using UnityEngine;
using TMPro; // Required for TextMeshPro components
using System.Collections; // Required for IEnumerator
using UnityEngine.EventSystems; // Required for detecting clicks

public class ColorInteraction : MonoBehaviour
{
    public GameObject red;
    public GameObject blue;
    public GameObject green;
    public GameObject white;
    public GameObject fence;
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt;

    private GameObject[] colors;
    private string promptColor;
    private bool taskCompleted = false; // Flag to check if the task is completed

    void Start()
    {
        colors = new GameObject[] { red, blue, green, white };

        // Initially set all colors to be invisible
        foreach (var colorObj in colors)
        {
            colorObj.SetActive(false);
            BoxCollider collider = colorObj.AddComponent<BoxCollider>();
            collider.isTrigger = false; // Ensure it is not a trigger
        }

        // Choose a random color prompt
        int randomIndex = Random.Range(0, colors.Length);
        promptColor = colors[randomIndex].name.ToLower();
        Debug.Log("Prompt Color: " + promptColor);
    }

    void Update()
    {
        if (taskCompleted) return; // Skip further checks if the task is completed

        bool isWithinRange = false;

        foreach (var colorObj in colors)
        {
            float distance = Vector3.Distance(colorObj.transform.position, Camera.main.transform.position);
            Debug.Log("Distance to " + colorObj.name + ": " + distance);

            if (distance < 10f) // Object becomes visible when the player is near
            {
                isWithinRange = true; // Mark that the player is in range
                prompt.SetActive(true);
                ShowCanvas("Click the color: " + promptColor);

                foreach (var obj in colors)
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
                if (clickedObject.name.ToLower() == promptColor)
                {
                    taskCompleted = true;
                    ShowCanvas("Good Job!");
                    Debug.Log("Correct color clicked: " + clickedObject.name);

                    foreach (var obj in colors)
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
*/


using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class ColorInteraction : MonoBehaviour
{
    public GameObject red;
    public GameObject blue;
    public GameObject green;
    public GameObject white;
    public GameObject fence;
    public GameObject canvas;
    public TMP_Text consoleText;
    public GameObject prompt;

    private GameObject[] colors;
    private string promptColor;
    private bool taskCompleted = false;

    void Start()
    {
        colors = new GameObject[] { red, blue, green, white };

        foreach (var colorObj in colors)
        {
            if (colorObj == null)
            {
                Debug.LogError("🚨 Missing color GameObject in Inspector!");
                continue;
            }

            colorObj.SetActive(false);
            BoxCollider collider = colorObj.AddComponent<BoxCollider>();
            collider.isTrigger = false;
        }

        if (fence == null)
        {
            Debug.LogError("🚨 Fence is not assigned in Inspector!");
        }

        if (ScoreManager.Instance == null)
        {
            Debug.LogError("❌ ScoreManager Instance is NULL! Make sure it's in the scene.");
        }

        int randomIndex = Random.Range(0, colors.Length);
        promptColor = colors[randomIndex].name.ToLower();
        Debug.Log("Prompt Color: " + promptColor);
    }

    void Update()
    {
        if (taskCompleted) return;

        bool isWithinRange = false;

        foreach (var colorObj in colors)
        {
            if (colorObj == null) continue;

            float distance = Vector3.Distance(colorObj.transform.position, Camera.main.transform.position);
            if (distance < 10f)
            {
                isWithinRange = true;
                prompt.SetActive(true);
                ShowCanvas("Click the color: " + promptColor);

                foreach (var obj in colors)
                {
                    obj.SetActive(true);
                }
            }
        }

        if (!isWithinRange)
        {
            prompt.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.name.ToLower() == promptColor)
                {
                    taskCompleted = true;
                    ShowCanvas("Good Job!");
                    Debug.Log("✅ Correct color clicked: " + clickedObject.name);

                    // ✅ Add Score Safely
                    if (ScoreManager.Instance != null)
                    {
                        ScoreManager.Instance.AddScore(100);
                        Debug.Log("✅ 100 points awarded!");
                    }

                    foreach (var obj in colors)
                    {
                        if (obj != null) Destroy(obj);
                    }

                    if (fence != null)
                    {
                        Destroy(fence);
                        Debug.Log("✅ Fence Removed!");
                    }

                    StartCoroutine(CompleteTaskWithDelay(2f));
                }
            }
        }
    }

    void ShowCanvas(string message)
    {
        if (canvas != null) canvas.SetActive(true);
        if (consoleText != null) consoleText.text = message;
    }

    private IEnumerator CompleteTaskWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (prompt != null) prompt.SetActive(false);
        gameObject.SetActive(false);
    }
}
