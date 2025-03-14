using UnityEngine;
using TMPro;
using System.Collections;

public class MathInteraction : MonoBehaviour
{
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject fence;
    public GameObject canvas;
    public TMP_Text consoleText;
    public GameObject prompt;

    private GameObject[] numbers;
    private int correctAnswer;
    private bool taskCompleted = false;
    private bool numbersMadeVisible = false;

    void Start()
    {
        numbers = new GameObject[] { three, four, five };

        // Initially set all numbers to be invisible
        foreach (var numObj in numbers)
        {
            if (numObj == null)
            {
                Debug.LogError("A number GameObject is missing in the Inspector!");
                continue;
            }

            numObj.SetActive(false);
            if (numObj.GetComponent<BoxCollider>() == null)
            {
                BoxCollider collider = numObj.AddComponent<BoxCollider>();
                collider.isTrigger = false;
            }
        }

        // Initially hide UI elements
        if (canvas != null) canvas.SetActive(false);
        if (prompt != null) prompt.SetActive(false);
    }

    void Update()
    {
        if (taskCompleted) return;

        bool isWithinRange = false;

        foreach (var numObj in numbers)
        {
            if (numObj == null) continue;

            float distance = Vector3.Distance(numObj.transform.position, Camera.main.transform.position);

            if (distance < 5f)
            {
                isWithinRange = true;

                if (!numbersMadeVisible) // Generate question only once when numbers become visible
                {
                    numbersMadeVisible = true;
                    GenerateMathQuestion();
                    ShowCanvas($"Solve: {correctAnswer - Random.Range(1, 3)} + {Random.Range(1, 3)} = ?");

                    if (prompt != null) prompt.SetActive(true);

                    foreach (var obj in numbers)
                    {
                        obj.SetActive(true);
                    }
                }
            }
        }

        if (!isWithinRange && numbersMadeVisible)
        {
            if (prompt != null) prompt.SetActive(false);
        }

        HandleClickDetection();
    }

    void HandleClickDetection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                // Check if clicked object's name matches correct answer numerically or textually
                if (clickedObject.name == correctAnswer.ToString() || 
                    clickedObject.name.ToLower() == NumberToWord(correctAnswer))
                {
                    Debug.Log("Correct answer clicked: " + clickedObject.name);
                    TaskCompleted();
                }
                else
                {
                    Debug.Log("Incorrect answer: " + clickedObject.name);
                }
            }
        }
    }

    void GenerateMathQuestion()
    {
        int[][] possiblePairs = { new int[] { 1, 2 }, new int[] { 2, 2 }, new int[] { 2, 3 } };
        int[] selectedPair = possiblePairs[Random.Range(0, possiblePairs.Length)];
        int a = selectedPair[0], b = selectedPair[1];
        correctAnswer = a + b;
        Debug.Log($"New Question Generated: {a} + {b} = {correctAnswer}");
    }

    void ShowCanvas(string message)
    {
        if (canvas != null) canvas.SetActive(true);
        if (consoleText != null) consoleText.text = message;
        Debug.Log("Canvas Message Updated: " + message);
    }

    void TaskCompleted()
    {
        taskCompleted = true;
        ShowCanvas("Good Job!");

        foreach (var obj in numbers)
        {
            Destroy(obj);
        }
        Destroy(fence);
        StartCoroutine(CompleteTaskWithDelay(2f));
    }

    private IEnumerator CompleteTaskWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (consoleText != null) consoleText.text = "";
        if (prompt != null) prompt.SetActive(false);
        if (canvas != null) canvas.SetActive(false);

        Debug.Log("Task completed, deactivating object.");
        gameObject.SetActive(false);
    }

    string NumberToWord(int number)
    {
        switch (number)
        {
            case 3: return "three";
            case 4: return "four";
            case 5: return "five";
            default: return number.ToString();
        }
    }
}
