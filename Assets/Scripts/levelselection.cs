using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons; // Drag and assign buttons in Inspector

    void Start()
    {
        PrintBuildScenes(); // Debug: Print all scenes in Build Settings

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; // Adjust index (Scene starts from 1)
            Debug.Log("Assigning Button: Level " + levelIndex); // Debug: Ensure buttons are assigned
            levelButtons[i].onClick.AddListener(() => OnLevelButtonClick(levelIndex));
        }
    }

    void OnLevelButtonClick(int levelIndex)
    {
        Debug.Log("Button Clicked: Attempting to Load Level " + levelIndex); // Debug: Button click detected
        LoadLevel(levelIndex);
    }

    void LoadLevel(int levelIndex)
    {
        string levelName = "Level" + levelIndex;

        if (IsSceneInBuildSettings(levelName))
        {
            Debug.Log("Loading Scene: " + levelName);
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogError("Scene " + levelName + " is NOT in Build Settings!");
        }
    }

    void PrintBuildScenes()
    {
        Debug.Log("Scenes in Build Settings:");
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            Debug.Log("Scene " + i + ": " + sceneName);
        }
    }

    bool IsSceneInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
            string builtSceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (builtSceneName == sceneName) return true;
        }
        return false;
    }
}
