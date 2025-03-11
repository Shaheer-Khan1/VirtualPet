using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class AnimalRenderer : MonoBehaviour
{
    public List<GameObject> animals; // Assign 3 GameObjects in the Inspector
    private string apiUrl = "http://127.0.0.1:5000/predict"; // Flask API URL

    // Start is called before the first frame update
    void Start()
    {
        if (animals == null || animals.Count < 3)
        {
            Debug.LogError("Please assign exactly 3 GameObjects in the Inspector!");
            return;
        }

        // Make the API call
        StartCoroutine(MakeApiCall());
    }

    // Coroutine to make API call
    IEnumerator MakeApiCall()
    {
        // Create JSON data to send to the API (features array with 5 values)
        string jsonData = "{\"features\": [1, 0, 1, 0, 0]}";

        // Create the request
        using (UnityWebRequest www = new UnityWebRequest(apiUrl, "POST"))
        {
            // Set the request header to inform the server that we are sending JSON
            www.SetRequestHeader("Content-Type", "application/json");

            // Convert the jsonData to a byte array
            byte[] jsonToSend = new UTF8Encoding().GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();

            // Send the request and wait for a response
            yield return www.SendWebRequest();

            // Handle response
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Parse the JSON response
                string response = www.downloadHandler.text;
                Debug.Log("API Response: " + response);

                // Deserialize the JSON to get the predicted animal
                string predictedAnimal = ParsePrediction(response);
                Debug.Log("Predicted Animal: " + predictedAnimal);

                // Handle the game logic based on the predicted animal
                SetAnimalVisibility(predictedAnimal);
            }
            else
            {
                Debug.LogError("Error making API call: " + www.error);
            }
        }
    }

    // Method to parse the prediction from the response
    string ParsePrediction(string response)
    {
        
        if (response.Contains("Zebra"))
        {

            response = null;
            response = "Zebra";
            return response;
        }

        else if (response.Contains("Cow"))
        {

            response = null;
            response = "Cow";
            return response;
        }

        else if (response.Contains("Horse"))
        {

            response = null;
            response = "Horse";
            return response;
        }

        Debug.Log("hi");
        return string.Empty;
    }

    // Method to set the visibility of animals based on the predicted animal
    void SetAnimalVisibility(string predictedAnimal)
    {
        // Make sure the predicted animal matches one of the available animals
        if (string.IsNullOrEmpty(predictedAnimal))
        {
            Debug.LogError("No predicted animal received");
            return;
        }

        // Debug log to check predicted animal
        Debug.Log("Predicted Animal: " + predictedAnimal);

        // Loop through all the animals and set the visibility based on the prediction
        bool animalFound = false;

        foreach (GameObject animal in animals)
        {
            

            // Compare the predicted animal with the GameObject name directly
            if (animal.name.Equals(predictedAnimal))
            {
                animal.SetActive(true); // Activate the matching animal
                animalFound = true;
            }
            else
            {
                animal.SetActive(false); // Deactivate the other animals
            }
        }

        // If no animal was found, log an error
        if (!animalFound)
        {
            Debug.LogError("No matching animal found with name: " + predictedAnimal);
        }
    }
}
