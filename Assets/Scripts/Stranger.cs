using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include for TextMeshPro

public class Stranger : MonoBehaviour
{
    public GameObject targetObject; // The object to check proximity with
    public GameObject messagePanel; // Panel to show the message
    public TextMeshProUGUI messageText; // TMP text component for the message
    public float detectionRadius = 3.0f; // Radius within which interaction triggers

    private bool interactionActive = false;

    void Start()
    {
        // Ensure the messagePanel starts hidden
        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
        }
    }

    void Update()
    {
        // Check if the target object is within the detection radius
        if (Vector3.Distance(transform.position, targetObject.transform.position) <= detectionRadius)
        {
            if (!interactionActive)
            {
                StartInteraction();
            }
        }
        else
        {
            if (interactionActive)
            {
                EndInteraction();
            }
        }

        if (interactionActive)
        {
            // Check for user input
            if (Input.GetKeyDown(KeyCode.Y))
            {
                HandleYesResponse();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                HandleNoResponse();
            }
        }
    }

    void StartInteraction()
    {
        messagePanel.SetActive(true);

        interactionActive = true;
        if (messagePanel != null && messageText != null)
        {
            messageText.text = "A stranger wants to shake hands with you.(Y/N)";
            messagePanel.SetActive(true);
        }
    }

    void EndInteraction()
    {
        interactionActive = false;
        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
        }
    }

    void HandleYesResponse()
    {
        if (messageText != null)
        {
            messageText.text = "You should shake hands with strangers.(Hint N)";
        }
    }

    void HandleNoResponse()
    {
        if (messageText != null)
        {
            messageText.text = "Good Job rejecting the handshake! Move on.";
        }
        StartCoroutine(HideMessageAfterDelay());
    }

    IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(2.0f); // Wait for 2 seconds
        EndInteraction();
    }
}