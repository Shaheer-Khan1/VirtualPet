/*

using UnityEngine;
using TMPro;
using System.Collections;


public class CampfireInteract : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star4;
    public GameObject fence;
    public GameObject player;
    public GameObject messageObject; // Reference to the GameObject containing TMP

    private TextMeshProUGUI tmpText; // Reference to the TMP component
    private GameObject[] stars;
    private bool starsVisible = false;
    private int collectedStars = 0;

    void Start()
    {
        stars = new GameObject[] { star1, star2, star3, star4 };

        // Initially set all stars and the fence to be invisible
        foreach (var star in stars)
        {
            star.SetActive(false);
        }

        // Initialize the TMP component and hide the parent GameObject
        if (messageObject != null)
        {
            tmpText = messageObject.GetComponentInChildren<TextMeshProUGUI>();
            messageObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Message object is not assigned.");
        }
    }

    void Update()
    {
        float distanceToCampfire = Vector3.Distance(transform.position, player.transform.position);

        // Show stars and TMP message when the player is near the campfire
        if (distanceToCampfire < 10f && !starsVisible)
        {
            messageObject.SetActive(true);
            if (tmpText != null)
            {
                tmpText.text = "Your pet is feeling scared of fire, collect stars for your pet!";
            }

            foreach (var star in stars)
            {
                star.SetActive(true);
            }

            starsVisible = true; // Prevent repeating the prompt and visibility logic
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.onPause();
        }

        // Check if the player collects any star
        foreach (var star in stars)
        {
            if (star.activeSelf) // Only check active stars
            {
                float distanceToStar = Vector3.Distance(star.transform.position, player.transform.position);
                if (distanceToStar < 1f)
                {
                    if (tmpText != null)
                    {
                        tmpText.text = "Star collected! " + (collectedStars + 1);
                    }
                    star.SetActive(false); // Hide the star
                    collectedStars++; // Increment the collected stars count
                }
            }
        }

        // If all stars are collected, hide the fence
        if (collectedStars == stars.Length && fence.activeSelf)
        {
            if (tmpText != null)
            {
                tmpText.text = "All stars collected! The fence is now hidden.";
            }
            fence.SetActive(false);
            StartCoroutine(HideMessageAfterDelay(3f)); // Wait for 3 seconds
        }
    }

    // Coroutine to wait before hiding the message
    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (messageObject != null)
        {
            messageObject.SetActive(false);
        }
    }
}
*/

/*
using UnityEngine;
using TMPro;
using System.Collections;

public class CampfireInteract : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star4;
    public GameObject fence;
    public GameObject player;
    public GameObject messageObject;

    private TextMeshProUGUI tmpText;
    private GameObject[] stars;
    private bool starsVisible = false;
    private int collectedStars = 0;

    void Start()
    {
        stars = new GameObject[] { star1, star2, star3, star4 };

        foreach (var star in stars)
        {
            star.SetActive(false);
        }

        if (messageObject != null)
        {
            tmpText = messageObject.GetComponentInChildren<TextMeshProUGUI>();
            messageObject.SetActive(false);
        }
        else
        {
            Debug.LogError("❌ Message object is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        float distanceToCampfire = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToCampfire < 10f && !starsVisible)
        {
            // First, ensure the message object reference is not null
            if (messageObject == null)
            {
                Debug.LogError("❌ messageObject is null when trying to show message!");
                return;
            }
        
            messageObject.SetActive(true);
            Debug.Log("✅ Message panel activated!");

            // Verify tmpText is not null
            if (tmpText == null)
            {
                tmpText = messageObject.GetComponentInChildren<TextMeshProUGUI>();
                Debug.LogWarning("⚠️ Attempting to get TMP component at runtime");
            }

            if (tmpText != null)
            {
                tmpText.text = "Your pet is feeling scared of fire, collect stars for your pet!";
                tmpText.ForceMeshUpdate(); // Force text to update
                Debug.Log("✅ Message text updated successfully: " + tmpText.text);
            }
            else
            {
                Debug.LogError("❌ TMP Text component could not be found!");
            }

            // Rest of your code remains the same
            foreach (var star in stars)
            {
                star.SetActive(true);
            }

            starsVisible = true;
        }
    
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.onPause();
        }

        foreach (var star in stars)
        {
            if (star.activeSelf)
            {
                float distanceToStar = Vector3.Distance(star.transform.position, player.transform.position);
                if (distanceToStar < 1f)
                {
                    if (tmpText != null)
                    {
                        tmpText.text = "Star collected! " + (collectedStars + 1);
                        tmpText.ForceMeshUpdate();
                    }
                    star.SetActive(false);
                    collectedStars++;

                    ScoreManager.Instance.AddScore(50);
                    Debug.Log("✅ 50 points awarded for collecting a star!");
                }
            }
        }

        if (collectedStars == stars.Length && fence.activeSelf)
        {
            if (tmpText != null)
            {
                tmpText.text = "All stars collected! The fence is now hidden.";
                tmpText.ForceMeshUpdate();
            }
            fence.SetActive(false);
            StartCoroutine(HideMessageAfterDelay(3f));
        }
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (messageObject != null)
        {
            messageObject.SetActive(false);
        }
    }
}
*/


/*
using UnityEngine;
using TMPro;
using System.Collections;

public class CampfireInteract : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star4;
    public GameObject fence;
    public GameObject player;
    public GameObject messageObject; // Reference to the GameObject containing TMP

    private TextMeshProUGUI tmpText; // Reference to the TMP component
    private GameObject[] stars;
    private bool starsVisible = false;
    private int collectedStars = 0;

    void Start()
    {
        stars = new GameObject[] { star1, star2, star3, star4 };

        // Initially set all stars and the fence to be invisible
        foreach (var star in stars)
        {
            star.SetActive(false);
        }

        // Initialize the TMP component and hide the parent GameObject
        if (messageObject != null)
        {
            tmpText = messageObject.GetComponentInChildren<TextMeshProUGUI>();
            messageObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Message object is not assigned.");
        }
    }

    void Update()
    {
        float distanceToCampfire = Vector3.Distance(transform.position, player.transform.position);

        // Show stars and TMP message when the player is near the campfire
        if (distanceToCampfire < 10f && !starsVisible)
        {
            messageObject.SetActive(true);
            if (tmpText != null)
            {
                tmpText.text = "Your pet is feeling scared of fire, collect stars for your pet!";
            }

            foreach (var star in stars)
            {
                star.SetActive(true);
            }

            starsVisible = true; // Prevent repeating the prompt and visibility logic
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.onPause();
        }

        // Check if the player collects any star
        foreach (var star in stars)
        {
            if (star.activeSelf) // Only check active stars
            {
                float distanceToStar = Vector3.Distance(star.transform.position, player.transform.position);
                if (distanceToStar < 1f)
                {
                    if (tmpText != null)
                    {
                        tmpText.text = "Star collected! " + (collectedStars + 1);
                    }
                    star.SetActive(false); // Hide the star
                    collectedStars++; // Increment the collected stars count
                    
                    // Add score for collecting a star
                    ScoreManager.Instance.AddScore(50);
                    Debug.Log("✅ 50 points awarded for collecting a star!");
                }
            }
        }

        // If all stars are collected, hide the fence
        if (collectedStars == stars.Length && fence.activeSelf)
        {
            if (tmpText != null)
            {
                tmpText.text = "All stars collected! The fence is now hidden.";
            }
            fence.SetActive(false);
            StartCoroutine(HideMessageAfterDelay(3f)); // Wait for 3 seconds
        }
    }

    // Coroutine to wait before hiding the message
    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (messageObject != null)
        {
            messageObject.SetActive(false);
        }
    }
}*/

using UnityEngine;
using TMPro;
using System.Collections;

public class CampfireInteract : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star4;
    public GameObject fence;
    public GameObject player;
    public GameObject messageObject;

    private TextMeshProUGUI tmpText;
    private GameObject[] stars;
    private bool starsVisible = false;
    private int collectedStars = 0;

    void Start()
    {
        stars = new GameObject[] { star1, star2, star3, star4 };

        foreach (var star in stars)
        {
            star.SetActive(false);
        }

        if (messageObject != null)
        {
            tmpText = messageObject.GetComponentInChildren<TextMeshProUGUI>();
            messageObject.SetActive(false);
        }
        else
        {
            Debug.LogError("❌ Message object is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        float distanceToCampfire = Vector3.Distance(transform.position, player.transform.position);

        // ✅ Restored Original Message Display Logic 
        if (distanceToCampfire < 10f && !starsVisible)
        {
            if (messageObject != null)
            {
                messageObject.SetActive(true);
                if (tmpText != null)
                {
                    tmpText.text = "Your pet is feeling scared of fire, collect stars for your pet!";
                    tmpText.ForceMeshUpdate(); // ✅ Ensure UI Refreshes
                }
            }

            foreach (var star in stars)
            {
                star.SetActive(true);
            }

            starsVisible = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.onPause();
        }

        foreach (var star in stars)
        {
            if (star.activeSelf)
            {
                float distanceToStar = Vector3.Distance(star.transform.position, player.transform.position);
                if (distanceToStar < 1f)
                {
                    if (tmpText != null)
                    {
                        tmpText.text = "Star collected! " + (collectedStars + 1);
                        tmpText.ForceMeshUpdate();
                    }
                    star.SetActive(false);
                    collectedStars++;

                    // ✅ Added Score Functionality Without Affecting UI 
                    ScoreManager.Instance.AddScore(50);
                    Debug.Log("✅ 50 points awarded for collecting a star!");
                }
            }
        }

        if (collectedStars == stars.Length && fence.activeSelf)
        {
            if (tmpText != null)
            {
                tmpText.text = "All stars collected! The fence is now hidden.";
                tmpText.ForceMeshUpdate();
            }
            fence.SetActive(false);
            StartCoroutine(HideMessageAfterDelay(3f));
        }
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (messageObject != null)
        {
            messageObject.SetActive(false);
        }
    }
}
