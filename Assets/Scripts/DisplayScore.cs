using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    private const string StackHighScoreKey = "StackHighScore";
    private const string FlappyHighScoreKey = "FlappyHighScore";

    public GameObject ScoreBoard;
    public TextMeshProUGUI stackScoreText;
    public TextMeshProUGUI flappyScoreText;

    void Start()
    {
        ScoreBoard.SetActive(false);
        UpdateScoreUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            UpdateScoreUI();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            ScoreBoard.gameObject.SetActive(false);
    }
    private void UpdateScoreUI()
    {
        ScoreBoard.gameObject.SetActive(true);
        int stackHighScore = PlayerPrefs.GetInt(StackHighScoreKey, 0);
        int flappyHighScore = PlayerPrefs.GetInt(FlappyHighScoreKey, 0);

        stackScoreText.text = $"Stack High Score: {stackHighScore}";
        flappyScoreText.text = $"Flappy High Score: {flappyHighScore}";
    }

    // 최고 점수 갱신 (게임에서 점수 갱신 시 호출)
    public void SaveHighScore(int stackScore, int flappyScore)
    {
        int currentStackHighScore = PlayerPrefs.GetInt(StackHighScoreKey, 0);
        int currentFlappyHighScore = PlayerPrefs.GetInt(FlappyHighScoreKey, 0);

        if (stackScore > currentStackHighScore)
            PlayerPrefs.SetInt(StackHighScoreKey, stackScore);

        if (flappyScore > currentFlappyHighScore)
            PlayerPrefs.SetInt(FlappyHighScoreKey, flappyScore);

        PlayerPrefs.Save(); // 변경 내용 저장
    }
}

    