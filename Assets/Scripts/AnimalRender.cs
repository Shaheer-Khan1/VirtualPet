using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AnimalRenderer : MonoBehaviour
{
    public List<GameObject> animals; // Assign 3 GameObjects in Unity Inspector
    private string apiUrl = "http://127.0.0.1:5000/get_recent_prediction"; // Flask API URL

    void Start()
    {
        if (animals == null || animals.Count < 3)
        {
            Debug.LogError("Please assign exactly 3 GameObjects in the Inspector!");
            return;
        }

        StartCoroutine(MakeApiCall());
    }

    IEnumerator MakeApiCall()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string response = www.downloadHandler.text;
                Debug.Log("Raw API Response: " + response); // Print full JSON response

                string predictedAnimal = ParsePrediction(response);
                Debug.Log("Predicted Animal After Parsing: " + predictedAnimal);

                SetAnimalVisibility(predictedAnimal);
            }
            else
            {
                Debug.LogError("Error making API call: " + www.error);
            }
        }
    }

    // JSON parsing class
    [System.Serializable]
    class PredictionResponse
    {
        public string predicted_animal;
    }

    string ParsePrediction(string response)
    {
        try
        {
            PredictionResponse parsedResponse = JsonUtility.FromJson<PredictionResponse>(response);
            return parsedResponse.predicted_animal?.Trim(); // Trim spaces
        }
        catch
        {
            Debug.LogError("Failed to parse predicted animal");
            return string.Empty;
        }
    }

    void SetAnimalVisibility(string predictedAnimal)
    {
        if (string.IsNullOrEmpty(predictedAnimal))
        {
            Debug.LogError("No predicted animal received");
            return;
        }

        predictedAnimal = predictedAnimal.Trim(); // Ensure no extra spaces

        Debug.Log("Checking visibility for predicted animal: " + predictedAnimal);

        bool animalFound = false;

        foreach (GameObject animal in animals)
        {
            string animalName = animal.name.Trim(); // Ensure no spaces in name matching

            if (animalName.Equals(predictedAnimal, System.StringComparison.OrdinalIgnoreCase))
            {
                animal.SetActive(true);
                animalFound = true;
            }
            else
            {
                animal.SetActive(false);
            }
        }

        if (!animalFound)
        {
            Debug.LogError("No matching animal found with name: " + predictedAnimal);
        }
    }
}
