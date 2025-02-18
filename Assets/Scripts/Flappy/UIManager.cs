using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BestScoreText;

    void Start()
    {
        Transform panelTransform = transform.Find("Panel");
        
        ScoreText = panelTransform.Find("CurrentScore")?.GetComponent<TextMeshProUGUI>();
        BestScoreText = panelTransform.Find("BestScore")?.GetComponent<TextMeshProUGUI>();
    }

    public void SetRestart()
    {
        gameObject.SetActive(true);
    }
    public void UpdateScore(int score, int bestscore)
    {
        ScoreText.text = score.ToString();
        BestScoreText.text = bestscore.ToString();
    }

}

