using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BestScoreText;

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

    public void SetRestart()
    {
       Panel.SetActive(true); // Panel È°¼ºÈ­
    }
    public void UpdateScore(int score, int bestscore)
    {
        ScoreText.text = score.ToString();
        BestScoreText.text = bestscore.ToString();
    }
}


