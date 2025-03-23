using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement; // For scene management

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText; // UI Reference
    
    [Header("Web API Settings")]
    [SerializeField] private string apiUrl = "http://localhost:3000/update_score";
    [SerializeField] private string userId = "test_user"; // Default user ID, can be set at runtime

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        FindScoreText(); // Find the Score UI
    }

    private void Start()
    {
        FindScoreText(); // Make sure ScoreText is assigned at start
        SceneManager.sceneLoaded += OnSceneLoaded; // Register scene load callback
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Invoke("FindScoreText", 0.2f); // Small delay to ensure UI is fully loaded
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Clean up event
    }

    private void Update()
    {
        if (scoreText == null)
        {
            FindScoreText(); // Try finding ScoreText in case scene changed
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("✅ Score Updated: " + score);

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("⚠️ ScoreText not found! Make sure ScoreCanvas exists in this scene.");
        }

        // Send score update to server
        StartCoroutine(SendScoreToServer());
    }

    // Set the user ID (call this when player logs in or is identified)
    public void SetUserId(string newUserId)
    {
        if (!string.IsNullOrEmpty(newUserId))
        {
            userId = newUserId;
            Debug.Log("User ID set to: " + userId);
        }
    }

    // Set the API URL (useful for different environments)
    public void SetApiUrl(string newUrl)
    {
        if (!string.IsNullOrEmpty(newUrl))
        {
            apiUrl = newUrl;
            Debug.Log("API URL set to: " + apiUrl);
        }
    }

    private IEnumerator SendScoreToServer()
    {
        // Create JSON data to send
        string jsonData = JsonUtility.ToJson(new ScoreData { user_id = userId, score = score });

        // Create a UnityWebRequest to send the data
        using (UnityWebRequest www = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // Send the request
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || 
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("⚠️ Error sending score to server: " + www.error);
            }
            else
            {
                Debug.Log("✅ Score sent successfully to server");
            }
        }
    }

    private void FindScoreText()
    {
        GameObject textObject = GameObject.Find("ScoreText"); // Ensure ScoreText exists
        if (textObject != null)
        {
            scoreText = textObject.GetComponent<TextMeshProUGUI>();
            Debug.Log("✅ ScoreText found and assigned.");
            
            // Update UI with current score (in case it was found after score changed)
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("⚠️ ScoreText not found! Make sure 'ScoreText' is correctly named in every scene.");
        }
    }
}

// Class to serialize score data
[System.Serializable]
public class ScoreData
{
    public string user_id;
    public int score;
}