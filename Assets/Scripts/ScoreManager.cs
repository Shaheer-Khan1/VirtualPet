using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText; // UI Reference

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
    }

    private void FindScoreText()
    {
        GameObject textObject = GameObject.Find("ScoreText"); // Ensure ScoreText exists
        if (textObject != null)
        {
            scoreText = textObject.GetComponent<TextMeshProUGUI>();
            Debug.Log("✅ ScoreText found and assigned.");
        }
        else
        {
            Debug.LogError("⚠️ ScoreText not found! Make sure 'ScoreText' is correctly named in every scene.");
        }
    }
}

/*using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Needed for scene detection

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance
    public int score = 0;
    private TextMeshProUGUI scoreText;  // Dynamically assigned

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensures ScoreCanvas persists
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Invoke("FindScoreText", 0.5f); // Delay to find ScoreText after scene load
        SceneManager.sceneLoaded += OnSceneLoaded; // Ensures ScoreText is found in new levels
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Invoke("FindScoreText", 0.5f);
    }

    private void FindScoreText()
    {
        scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();

        if (scoreText == null)
        {
            Debug.LogError("⚠️ ScoreText not found! Make sure it's named 'ScoreText' in every scene.");
        }
        else
        {
            Debug.Log("✅ ScoreText found successfully in " + SceneManager.GetActiveScene().name);
            UpdateScoreUI();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("✅ Score Updated: " + score);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
/*
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Needed to detect scene changes

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance
    public int score = 0;
    private TextMeshProUGUI scoreText;  // Will be assigned dynamically

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep ScoreManager active across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindScoreText(); // Locate ScoreText when the game starts
        SceneManager.sceneLoaded += OnSceneLoaded; // Detect when a new scene loads
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindScoreText(); // Locate ScoreText in the new scene
    }

    private void FindScoreText()
    {
        scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();

        if (scoreText == null)
        {
            Debug.LogError("⚠️ ScoreText not found! Make sure it's named 'ScoreText' in the scene.");
        }
        else
        {
            UpdateScoreUI(); // Update UI when found
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("✅ Score Updated: " + score);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("⚠️ ScoreText is still not assigned!");
        }
    }
}
#1#


/*using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;  // Assign in Inspector to display score UI

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep ScoreManager active across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score Updated: " + score);

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}#1#*/