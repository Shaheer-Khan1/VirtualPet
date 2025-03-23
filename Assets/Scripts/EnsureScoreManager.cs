using UnityEngine;

public class EnsureScoreManager : MonoBehaviour
{
    public GameObject scoreManagerPrefab; // Assign ScoreManager Prefab in Inspector

    void Awake()
    {
        if (ScoreManager.Instance == null)
        {
            Debug.Log("⚠️ ScoreManager not found! Creating a new one.");
            Instantiate(scoreManagerPrefab); // Create ScoreManager
        }
    }
}