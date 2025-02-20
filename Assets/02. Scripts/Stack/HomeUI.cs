using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    Button startButton;
    Button exitButton;

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init(UI_Manager ui_Manager)
    {
        base.Init(ui_Manager);
        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }
    void OnClickStartButton()
    {
        ui_Manager.OnClickStart();
    }

    void OnClickExitButton()
    {
        ui_Manager.OnClickExit();
    }
}

