using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.Unicode;

public class InitialMenu : MonoBehaviour
{
    public static InitialMenu instance;

    // セッションIDとニックネームを入力するためのウィンドウ
    [SerializeField] private GameObject inputWindow;
    [SerializeField] private TMP_InputField field_sessionID;
    [SerializeField] private TMP_InputField field_nickname;

    // 各種ボタン
    [SerializeField] private Button battleButton;
    [SerializeField] private Button bossButton;
    [SerializeField] private Button tutorialButton;

    // 左右の銃士イメージ
    [SerializeField] private SpriteRenderer rightGunnerFigure;
    [SerializeField] private SpriteRenderer leftGunnerFigure;

    // NetworkRunnerのプレハブ
    [SerializeField] private GameObject RunnerPrefab;

    // NetworkRunnerの親オブジェクトのTransform
    [SerializeField] private Transform RunnerParentTransform;

    public void Awake()
    {
        instance = this;
    }

    public void SetGunnerFigures(GunnerData rightGunnerData, GunnerData leftGunnerData)
    {
        Sprite rightSprite = rightGunnerData.GetGunnerImage();
        Sprite leftSprite = leftGunnerData.GetGunnerImage();
        rightGunnerFigure.sprite = rightSprite;
        leftGunnerFigure.sprite = leftSprite;
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

    public void OnButtonPressed_InputEntry()
    {
        string sessionID = field_sessionID.text;
        string nickname = field_nickname.text;
        MenuStateManager.instance.runner = Instantiate(RunnerPrefab, RunnerParentTransform).GetComponent<NetworkRunner>();
        MenuStateManager.instance.runner.StartGame(new StartGameArgs
        {
            SessionName = sessionID,
            PlayerCount = 2,
            IsOpen = true,
        });
    }

    public void OnButtonPressed_InputExit()
    {
        if (inputWindow.activeSelf) { inputWindow.SetActive(false); }
        ToggleButtons(true);
    }
}
