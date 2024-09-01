using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuStateManager : MonoBehaviour
{
    public static MenuStateManager instance;

    // �Z�b�V����ID�ƃj�b�N�l�[������͂��邽�߂̃E�B���h�E
    [SerializeField] private GameObject inputWindow;
    [SerializeField] private TMP_InputField field_sessionID;
    [SerializeField] private TMP_InputField field_nickname;

    // �e��{�^��
    [SerializeField] private Button battleButton;
    [SerializeField] private Button bossButton;
    [SerializeField] private Button tutorialButton;

    // NetworkRunner�̃v���n�u
    [SerializeField] private GameObject RunnerPrefab;

    // NetworkRunner�̊i�[�p�ϐ�
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
