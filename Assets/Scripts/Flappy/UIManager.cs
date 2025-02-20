using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BestScoreText;
    public TextMeshProUGUI IngameScoreText;

    public GameObject Panel;


    private void Awake()
    {
        if (Panel != null)
        {
            ScoreText = Panel.transform.Find("CurrentScore")?.GetComponent<TextMeshProUGUI>();
            BestScoreText = Panel.transform.Find("BestScore")?.GetComponent<TextMeshProUGUI>();
        }
    }
    void Start()
    {
        Panel.SetActive(false);
    }

    public void UpdateScore(int score, int bestscore)
    {
        if (score > bestscore)
        {
            bestscore = score;
        }

        ScoreText.text = $"CurrentScore : {score.ToString()}";
        BestScoreText.text = $"BestScore : {bestscore.ToString()}";
    }
    public void IngameScore(int score)
    {
        IngameScoreText.text = score.ToString();
    }
}


