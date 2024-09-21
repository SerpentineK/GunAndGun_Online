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

    // �Z�b�V����ID�ƃj�b�N�l�[������͂��邽�߂̃E�B���h�E
    [Header("Input Window")]
    [SerializeField] private GameObject inputWindow;
    [SerializeField] private TMP_InputField field_sessionID;
    [SerializeField] private TMP_InputField field_nickname;

    // �e��{�^��
    [Header("Buttons")]
    [SerializeField] private Button battleButton;
    [SerializeField] private Button bossButton;
    [SerializeField] private Button tutorialButton;

    // ���E�̏e�m�C���[�W
    [Header("Gunner Figures")]
    [SerializeField] private SpriteRenderer rightGunnerFigure;
    [SerializeField] private SpriteRenderer leftGunnerFigure;

    // NetworkRunner�̃v���n�u
    [Header("Networking")]
    [SerializeField] private GameObject RunnerPrefab;

    // NetworkRunner�̐e�I�u�W�F�N�g��Transform
    [SerializeField] private Transform RunnerParentTransform;

    public void Awake()
    {
        instance = this;
    }

    public void SetGunnerFigures(GunnerData rightData, GunnerData leftData)
    {
        rightGunnerFigure.sprite = rightData.GetGunnerImage();
        leftGunnerFigure.sprite = leftData.GetGunnerImage();
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
        if ((sessionID.Length > 0)&&(nickname.Length > 0)) {
            MenuStateManager.instance.runner = Instantiate(RunnerPrefab, RunnerParentTransform).GetComponent<NetworkRunner>();
            MenuStateManager.instance.runner.StartGame(new StartGameArgs
            {
                SessionName = sessionID,
                PlayerCount = 2,
                IsOpen = true,
            });
        }
    }

    public void OnButtonPressed_InputExit()
    {
        if (inputWindow.activeSelf) { inputWindow.SetActive(false); }
        ToggleButtons(true);
    }
}
