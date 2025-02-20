using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    private int CurrentScore = 0;

    private int B_Score = 0;
    public int BestScore { get => B_Score; }

    UIManager uiManager;
    public UIManager UImanager {  get { return uiManager; } }
    private const string FlappyBestScoreKey = "BestScore_Flappy";
    public GameObject uiCanvas;

    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        B_Score = PlayerPrefs.GetInt(FlappyBestScoreKey);

        uiManager.UpdateScore(0,B_Score);
    }

    public void GameOver()
    {
        uiCanvas.SetActive(true);
        
        uiManager.UpdateScore(CurrentScore, B_Score);

    }

    public void Restart()
    {
        uiManager.Panel.SetActive(true);
    }

    public void AddScore(int score)
    {
        CurrentScore += score;
        uiManager.IngameScore(CurrentScore);
        if (B_Score < CurrentScore)
        {
            PlayerPrefs.SetInt(FlappyBestScoreKey, CurrentScore);
        }
  
    }
}
