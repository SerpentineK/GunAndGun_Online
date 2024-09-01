using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuStateManager : MonoBehaviour
{
    public static MenuStateManager instance;

    // セッションIDとニックネームを入力するためのウィンドウ
    [SerializeField] private GameObject inputWindow;
    [SerializeField] private TMP_InputField field_sessionID;
    [SerializeField] private TMP_InputField field_nickname;

    // 各種ボタン
    [SerializeField] private Button battleButton;
    [SerializeField] private Button bossButton;
    [SerializeField] private Button tutorialButton;

    // NetworkRunnerのプレハブ
    [SerializeField] private GameObject RunnerPrefab;

    // NetworkRunnerの格納用変数
    private NetworkRunner runner;

    public void Awake()
    {
        instance = this;
    }

    public void ToggleButtons(bool result)
    {
        battleButton.enabled = result;
        bossButton.enabled = result;
        tutorialButton.enabled = result;
    }

    public void OnButtonPressed_Battle()
    {
        if (!inputWindow.activeSelf) { inputWindow.SetActive(true); }
        ToggleButtons(false);
    }

    public void OnButtonPressed_Boss()
    {

    }

    public void OnButtonPressed_Tutorial()
    {

    }

    public void OnButtonPressed_Entry()
    {
        string sessionID = field_sessionID.text;
        string nickname = field_nickname.text;
        runner = Instantiate(RunnerPrefab).GetComponent<NetworkRunner>();
        runner.StartGame(new StartGameArgs
        {
            SessionName = sessionID,
            PlayerCount = 2,
            IsOpen = true,
        });
    }

    public void OnButtonPressed_Exit() 
    {
        if (inputWindow.activeSelf) { inputWindow.SetActive(false); }
        ToggleButtons(true);
    }
}
