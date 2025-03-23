/*
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SafeObjectSelectionTask : MonoBehaviour
{
    public GameObject[] safeObjects; // Array of safe objects
    public GameObject[] unsafeObjects; // Array of unsafe objects
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt;
    public GameObject fence;
    public GameObject player;
    
    private int collectedSafeObjects = 0;
    private bool taskCompleted = false;
    private bool itemsVisible = false;

    void Start()
    {
        // Hide all objects at the start
        foreach (GameObject item in safeObjects)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in unsafeObjects)
        {
            item.SetActive(false);
        }
    }

    void Update()
    {
        if (taskCompleted) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        // Show prompt, objects, and canvas when the player is near
        if (distanceToPlayer < 10f && !itemsVisible)
        {
            ShowCanvas("Tap only the safe objects!");
            foreach (GameObject item in safeObjects)
            {
                item.SetActive(true);
            }
            foreach (GameObject item in unsafeObjects)
            {
                item.SetActive(true);
            }
            itemsVisible = true;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) // Detect left mouse button click or space key
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (System.Array.Exists(safeObjects, element => element == clickedObject))
                {
                    clickedObject.SetActive(false); // Hide object instead of destroying it
                    collectedSafeObjects++;

                    if (collectedSafeObjects >= safeObjects.Length)
                    {
                        TaskCompleted();
                    }
                }
            }
        }
    }

    void ShowCanvas(string message)
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
        if (consoleText != null)
        {
            consoleText.text = message;
        }
        if (prompt != null)
        {
            prompt.SetActive(true);
        }
        Debug.Log("Canvas Message: " + message);
    }

    void TaskCompleted()
    {
        taskCompleted = true;
        ShowCanvas("Level Completed!");
        StartCoroutine(CompleteTaskWithDelay(2f));
    }

    private IEnumerator CompleteTaskWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (prompt != null)
        {
            prompt.SetActive(false);
        }

        // Hide all remaining objects after completion
        foreach (GameObject item in safeObjects)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in unsafeObjects)
        {
            item.SetActive(false);
        }

        Debug.Log("Task completed, loading Level 5.");
        SceneManager.LoadScene("Level5");
    }
}
*/



using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SafeObjectSelectionTask : MonoBehaviour
{
    public GameObject[] safeObjects; // Array of safe objects
    public GameObject[] unsafeObjects; // Array of unsafe objects
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt;
    public GameObject fence;
    public GameObject player;

    private int collectedSafeObjects = 0;
    private bool taskCompleted = false;
    private bool itemsVisible = false;

    void Start()
    {
        // Hide all objects at the start
        foreach (GameObject item in safeObjects)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in unsafeObjects)
        {
            item.SetActive(false);
        }
    }

    void Update()
    {
        if (taskCompleted) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Show prompt, objects, and canvas when the player is near
        if (distanceToPlayer < 10f && !itemsVisible)
        {
            ShowCanvas("Tap only the safe objects!");
            foreach (GameObject item in safeObjects)
            {
                item.SetActive(true);
            }
            foreach (GameObject item in unsafeObjects)
            {
                item.SetActive(true);
            }
            itemsVisible = true;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) // Detect left mouse button click or space key
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (System.Array.Exists(safeObjects, element => element == clickedObject))
                {
                    clickedObject.SetActive(false); // Hide object instead of destroying it
                    collectedSafeObjects++;

                    // ✅ Awarding 100 points per correct selection
                    if (ScoreManager.Instance != null)
                    {
                        ScoreManager.Instance.AddScore(100);
                        Debug.Log("✅ 100 points awarded! Total Score: " + ScoreManager.Instance.score);
                    }

                    if (collectedSafeObjects >= safeObjects.Length)
                    {
                        TaskCompleted();
                    }
                }
            }
        }
    }

    void ShowCanvas(string message)
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
        if (consoleText != null)
        {
            consoleText.text = message;
        }
        if (prompt != null)
        {
            prompt.SetActive(true);
        }
        Debug.Log("Canvas Message: " + message);
    }

    void TaskCompleted()
    {
        taskCompleted = true;
        ShowCanvas("Level Completed!");
        StartCoroutine(CompleteTaskWithDelay(2f));
    }

    private IEnumerator CompleteTaskWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (prompt != null)
        {
            prompt.SetActive(false);
        }

        // Hide all remaining objects after completion
        foreach (GameObject item in safeObjects)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in unsafeObjects)
        {
            item.SetActive(false);
        }

        Debug.Log("Task completed, loading Level 5.");
        SceneManager.LoadScene("Level5");
    }
}
