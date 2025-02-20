using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    private const string StackHighScoreKey = "BestScore_Stack";
    private const string FlappyHighScoreKey = "BestScore_Flappy";

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
            ScoreBoard.gameObject.SetActive(true);

        UpdateScoreUI();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            ScoreBoard.gameObject.SetActive(false);
    }

    private void UpdateScoreUI()
    {
        int stackHighScore = PlayerPrefs.GetInt(StackHighScoreKey, 0);
        int flappyHighScore = PlayerPrefs.GetInt(FlappyHighScoreKey, 0);
        stackScoreText.text = $"Stack High Score: {stackHighScore}";
        flappyScoreText.text = $"Flappy High Score: {flappyHighScore}";
    }

}

