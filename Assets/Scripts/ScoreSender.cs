using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreSender : MonoBehaviour
{
    private string flaskUrl = "http://127.0.0.1:3000/update_score"; // Your Flask endpoint

    public void SendScoreToFlask(string userId, int score) // Now takes 2 arguments
    {
        StartCoroutine(SendScoreCoroutine(userId, score));
    }

    private IEnumerator SendScoreCoroutine(string userId, int score)
    {
        ScoreData scoreData = new ScoreData { user_id = userId, score = score };

        string jsonData = JsonUtility.ToJson(scoreData); // Unity's built-in JSON converter
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(flaskUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("✅ Score sent successfully! Response: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("❌ Error sending score to Flask: " + request.error);
            }
        }
    }

    [System.Serializable]
    private class ScoreData
    {
        public string user_id;
        public int score;
    }
}