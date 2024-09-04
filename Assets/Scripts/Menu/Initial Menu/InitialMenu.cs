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
    [SerializeField] private GameObject inputWindow;
    [SerializeField] private TMP_InputField field_sessionID;
    [SerializeField] private TMP_InputField field_nickname;

    // �e��{�^��
    [SerializeField] private Button battleButton;
    [SerializeField] private Button bossButton;
    [SerializeField] private Button tutorialButton;

    // ���E�̏e�m�C���[�W
    [SerializeField] private SpriteRenderer rightGunnerFigure;
    [SerializeField] private SpriteRenderer leftGunnerFigure;

    // NetworkRunner�̃v���n�u
    [SerializeField] private GameObject RunnerPrefab;

    // NetworkRunner�̐e�I�u�W�F�N�g��Transform
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
