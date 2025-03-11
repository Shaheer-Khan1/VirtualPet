using UnityEngine;
using TMPro;

public class CampfireInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject messageObject; // Reference to the GameObject containing TMP

    private TextMeshProUGUI tmpText; // Reference to the TMP component

    void Start()
    {
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

        // Show message when the player is near the campfire
        if (distanceToCampfire < 10f)
        {
            messageObject.SetActive(true);
            if (tmpText != null)
            {
                tmpText.text = "Please choose a safe path";
            }
        }
        else
        {
            messageObject.SetActive(false);
        }
    }
}
