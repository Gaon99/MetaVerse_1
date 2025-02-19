using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    private int CurrentScore = 0;

    private int BestScore_Flappy = 0;
    public int BestScore { get => BestScore_Flappy; }
    UIManager uiManager;
    public UIManager UImanager {  get { return uiManager; } }
    private const string BestScoreKey = "BestScore";
    public GameObject uiCanvas;

    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        uiManager.UpdateScore(0,BestScore);

        BestScore_Flappy = PlayerPrefs.GetInt(BestScoreKey);
    }

    public void GameOver()
    {
        uiCanvas.SetActive(true);
    }

    public void Restart()
    {
        uiManager.Panel.SetActive(true);
    }

    public void AddScore(int score)
    {
        CurrentScore += score;
        uiManager.UpdateScore(CurrentScore,BestScore_Flappy);
    }
}
