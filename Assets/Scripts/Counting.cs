/*
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Counting : MonoBehaviour
{
    public GameObject[] allItems; // Array of objects 1,2,3,4,5
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt;
    public GameObject fence;
    public GameObject player;
    
    private int currentTargetIndex = 0;
    private bool taskCompleted = false;
    private bool itemsVisible = false;

    void Start()
    {
        // Hide all items at the start
        foreach (GameObject item in allItems)
        {
            item.SetActive(false);
        }
    }

    void Update()
    {
        if (taskCompleted) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        // Show prompt, selected items, and canvas when the player is near
        if (distanceToPlayer < 20f && !itemsVisible)
        {
            ShowCanvas("Tap on " + allItems[currentTargetIndex].name);
            foreach (GameObject item in allItems)
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
                if (clickedObject == allItems[currentTargetIndex])
                {
                    clickedObject.SetActive(false); // Hide object instead of destroying it
                    currentTargetIndex++;

                    if (currentTargetIndex < allItems.Length)
                    {
                        ShowCanvas("Tap on " + allItems[currentTargetIndex].name);
                    }
                    else
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
        ShowCanvas("Task Completed! The fence is now open.");
        StartCoroutine(CompleteTaskWithDelay(2f));
    }

    private IEnumerator CompleteTaskWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (prompt != null)
        {
            prompt.SetActive(false);
        }

        // Hide the fence
        if (fence != null)
        {
            fence.SetActive(false);
        }

        Debug.Log("Task completed, deactivating object.");
        gameObject.SetActive(false);
    }
}
*/





using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Counting : MonoBehaviour
{
    public GameObject[] allItems; // Array of objects 1,2,3,4,5
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt;
    public GameObject fence;
    public GameObject player;

    private int currentTargetIndex = 0;
    private bool taskCompleted = false;
    private bool itemsVisible = false;

    void Start()
    {
        // Hide all items at the start
        foreach (GameObject item in allItems)
        {
            item.SetActive(false);
        }
    }

    void Update()
    {
        if (taskCompleted) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Show prompt, selected items, and canvas when the player is near
        if (distanceToPlayer < 20f && !itemsVisible)
        {
            ShowCanvas("Tap on " + allItems[currentTargetIndex].name);
            foreach (GameObject item in allItems)
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
                if (clickedObject == allItems[currentTargetIndex])
                {
                    clickedObject.SetActive(false); // Hide object instead of destroying it
                    currentTargetIndex++;

                    // ✅ Awarding 50 points per correct selection
                    if (ScoreManager.Instance != null)
                    {
                        ScoreManager.Instance.AddScore(50);
                        Debug.Log("✅ 50 points awarded! Total Score: " + ScoreManager.Instance.score);
                    }

                    if (currentTargetIndex < allItems.Length)
                    {
                        ShowCanvas("Tap on " + allItems[currentTargetIndex].name);
                    }
                    else
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
        ShowCanvas("Task Completed! The fence is now open.");
        StartCoroutine(CompleteTaskWithDelay(2f));
    }

    private IEnumerator CompleteTaskWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (prompt != null)
        {
            prompt.SetActive(false);
        }

        // Hide the fence
        if (fence != null)
        {
            fence.SetActive(false);
        }

        Debug.Log("Task completed, deactivating object.");
        gameObject.SetActive(false);
    }
}
