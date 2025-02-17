using UnityEngine;
using TMPro;

using UnityEngine.UI;
public class ScoreUI : BaseUI
{
    TextMeshProUGUI scoreTxt;
    TextMeshProUGUI comboTxt;
    TextMeshProUGUI bestScoreTxt;
    TextMeshProUGUI bestComboTxt;

    Button StartBtn;
    Button ExitBtn;

    protected override UIState GetUIState()
    {
        return UIState.Score;
    }

    public override void Init(UI_Manager ui_Manager)
    {
        base.Init(ui_Manager);

        scoreTxt = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        comboTxt = transform.Find("ComboText").GetComponent<TextMeshProUGUI>();
        bestScoreTxt = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
        bestComboTxt = transform.Find("BestComboText").GetComponent<TextMeshProUGUI>();

        StartBtn = transform.Find("StartBtn").GetComponent<Button>();
        ExitBtn = transform.Find("ExitBtn").GetComponent<Button>();

        StartBtn.onClick.AddListener(OnclickStartBtn);
        ExitBtn.onClick.AddListener(OnClickExitBtn);
    }

    public void SetUI(int score, int combo, int bestScore, int bestCombo)
    {
        scoreTxt.text = score.ToString();
        comboTxt.text = combo.ToString();
        bestScoreTxt.text = bestScore.ToString();
        bestComboTxt.text = bestCombo.ToString();

    }
    void OnclickStartBtn()
    {
        ui_Manager.OnClickStart();
    }
    void OnClickExitBtn()
    {
        ui_Manager.OnClickExit();
    }

}
