using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    private int CurrentScore = 0;

    UIManager uiManager;
    public UIManager UImanager {  get { return uiManager; } } 
   
    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        uiManager.UpdateScore(0);
    }

    public void GameOver()
    {
        uiManager.SetRestart();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        CurrentScore += score;
        uiManager.UpdateScore(CurrentScore);
    }








}
