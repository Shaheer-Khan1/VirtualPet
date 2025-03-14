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
