/*
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ItemCollectionTask : MonoBehaviour
{
    public GameObject[] allItems; // Array of 9 items
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt;
    public GameObject fence;
    public GameObject player;
    
    private List<GameObject> selectedItems = new List<GameObject>();
    private int requiredItems;
    private int collectedItems = 0;
    private bool taskCompleted = false;
    private bool itemsVisible = false;

    void Start()
    {
        // Hide all items at the start
        foreach (GameObject item in allItems)
        {
            item.SetActive(false);
        }

        // Randomly select a number of items to collect (e.g., 3 to 6)
        requiredItems = Random.Range(3, 7);
        SelectRandomItems(requiredItems);
    }

    void Update()
    {
        if (taskCompleted) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        // Show prompt, selected items, and canvas when the player is near
        if (distanceToPlayer < 10f && !itemsVisible)
        {
            ShowCanvas("Tap " + requiredItems + " items for your pet!");
            foreach (GameObject item in selectedItems)
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
                if (selectedItems.Contains(clickedObject) && clickedObject.activeSelf)
                {
                    clickedObject.SetActive(false); // Hide object instead of destroying it
                    collectedItems++;
                    ShowCanvas("Collected: " + collectedItems + "/" + requiredItems);

                    if (collectedItems >= requiredItems)
                    {
                        TaskCompleted();
                    }
                }
            }
        }
    }

    void SelectRandomItems(int count)
    {
        List<GameObject> itemList = new List<GameObject>(allItems);
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, itemList.Count);
            GameObject item = itemList[randomIndex];
            itemList.RemoveAt(randomIndex);
            selectedItems.Add(item);
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

        // Hide all remaining items after completion
        foreach (GameObject item in allItems)
        {
            item.SetActive(false);
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

public class ItemCollectionTask : MonoBehaviour
{
    public GameObject[] allItems; // Array of 9 items
    public GameObject canvas; // Reference to the Canvas GameObject
    public TMP_Text consoleText; // Reference to the TextMeshPro component
    public GameObject prompt;
    public GameObject fence;
    public GameObject player;

    private List<GameObject> selectedItems = new List<GameObject>();
    private int requiredItems;
    private int collectedItems = 0;
    private bool taskCompleted = false;
    private bool itemsVisible = false;

    void Start()
    {
        // Hide all items at the start
        foreach (GameObject item in allItems)
        {
            item.SetActive(false);
        }

        // Randomly select a number of items to collect (e.g., 3 to 6)
        requiredItems = Random.Range(3, 7);
        SelectRandomItems(requiredItems);
    }

    void Update()
    {
        if (taskCompleted) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Show prompt, selected items, and canvas when the player is near
        if (distanceToPlayer < 10f && !itemsVisible)
        {
            ShowCanvas("Tap " + requiredItems + " items for your pet!");
            foreach (GameObject item in selectedItems)
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
                if (selectedItems.Contains(clickedObject) && clickedObject.activeSelf)
                {
                    clickedObject.SetActive(false); // Hide object instead of destroying it
                    collectedItems++;
                    ShowCanvas("Collected: " + collectedItems + "/" + requiredItems);

                    // ✅ Awarding 50 points per item collected
                    if (ScoreManager.Instance != null)
                    {
                        ScoreManager.Instance.AddScore(50);
                        Debug.Log("✅ 50 points awarded! Total Score: " + ScoreManager.Instance.score);
                    }

                    if (collectedItems >= requiredItems)
                    {
                        TaskCompleted();
                    }
                }
            }
        }
    }

    void SelectRandomItems(int count)
    {
        List<GameObject> itemList = new List<GameObject>(allItems);
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, itemList.Count);
            GameObject item = itemList[randomIndex];
            itemList.RemoveAt(randomIndex);
            selectedItems.Add(item);
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

        // Hide all remaining items after completion
        foreach (GameObject item in allItems)
        {
            item.SetActive(false);
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

