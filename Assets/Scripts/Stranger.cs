/*
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Required for scene management

public class Stranger : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public float detectionRadius = 3.0f;

    private bool interactionActive = false;

    void Start()
    {
        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetObject.transform.position) <= detectionRadius)
        {
            if (!interactionActive)
            {
                StartInteraction();
            }
        }
        else if (interactionActive)
        {
            EndInteraction();
        }

        if (interactionActive)
        {
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
        if (messageText != null)
        {
            messageText.text = "A stranger wants to shake hands with you. (Y/N)";
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
            messageText.text = "You should shake hands with strangers. (Hint: N)";
        }
    }

    void HandleNoResponse()
    {
        if (messageText != null)
        {
            messageText.text = "Good Job rejecting the handshake! Move on.";
        }
        StartCoroutine(TransitionToLevel2());
    }

    IEnumerator TransitionToLevel2()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Level2"); // Load Level 2
    }
}
*/
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Stranger : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public float detectionRadius = 5.0f;

    private bool interactionActive = false;

    void Start()
    {
        if (messagePanel == null)
        {
            Debug.LogError("❌ Message Panel is NOT assigned in the Inspector!");
        }
        else
        {
            messagePanel.SetActive(false);
        }

        if (messageText == null)
        {
            Debug.LogError("❌ messageText is NOT assigned in the Inspector!");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        if (distance <= detectionRadius)
        {
            if (!interactionActive)
            {
                StartInteraction();
            }
        }
        else if (interactionActive)
        {
            EndInteraction();
        }

        if (interactionActive)
        {
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
        if (messagePanel != null)
        {
            messagePanel.SetActive(true);
            Debug.Log("✅ Message Panel activated!");
        }

        interactionActive = true;

        if (messageText != null)
        {
            messageText.text = "A stranger wants to shake hands with you. (Y/N)";
            messageText.ForceMeshUpdate(); // Make sure this is called
            Debug.Log("✅ Stranger message displayed!");
        }
        else
        {
            Debug.LogError("❌ messageText reference is null in StartInteraction!");
        }
    }

    void EndInteraction()
    {
        interactionActive = false;
        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
            Debug.Log("✅ Message Panel hidden.");
        }
    }

    void HandleYesResponse()
    {
        if (messageText != null)
        {
            messageText.text = "You should shake hands with strangers. (Hint: N)";
            messageText.ForceMeshUpdate();
        }
    }

    void HandleNoResponse()
    {
        if (messageText != null)
        {
            messageText.text = "Good Job rejecting the handshake! Move on.";
            messageText.ForceMeshUpdate();
        }

        ScoreManager.Instance.AddScore(100);
        Debug.Log("✅ 100 points awarded for rejecting the handshake!");

        StartCoroutine(TransitionToLevel2());
    }

    IEnumerator TransitionToLevel2()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Level2");
    }
}



/*

using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Stranger : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public float detectionRadius = 3.0f;

    private bool interactionActive = false;

    void Start()
    {
        if (messagePanel != null)
        {
            messagePanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetObject.transform.position) <= detectionRadius)
        {
            if (!interactionActive)
            {
                StartInteraction();
            }
        }
        else if (interactionActive)
        {
            EndInteraction();
        }

        if (interactionActive)
        {
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
        if (messageText != null)
        {
            messageText.text = "A stranger wants to shake hands with you. (Y/N)";
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
            messageText.text = "You should shake hands with strangers. (Hint: N)";
        }
    }

    void HandleNoResponse()
    {
        if (messageText != null)
        {
            messageText.text = "Good Job rejecting the handshake! Move on.";
        }
        
        // Add score when player correctly rejects the handshake
        ScoreManager.Instance.AddScore(100);
        Debug.Log("✅ 100 points awarded for rejecting the handshake!");
        
        StartCoroutine(TransitionToLevel2());
    }

    IEnumerator TransitionToLevel2()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Level2");
    }
}*/






