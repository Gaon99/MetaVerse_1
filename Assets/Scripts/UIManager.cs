using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI RestartText;

    void Start()
    {
        if (RestartText == null)
        {
            Debug.LogError("RestartTxt is Null");
        }
        if (ScoreText ==  null)
        {
            Debug.LogError("ScoreTxt is Null");
        }

        RestartText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        RestartText.gameObject.SetActive(true);
    }
    public void UpdateScore(int score)
    {
        ScoreText.text = score.ToString();
    }
}

