using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject pausePanel;
    private int _count;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void onPause()
    {
        if (_count == 0)
        {
            pausePanel.SetActive(true);
            _count = 1;
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(false);
            _count = 0;
            Time.timeScale = 1;
        }
    }
}
