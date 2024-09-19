using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuStateManager : MonoBehaviour
{
    public static MenuStateManager instance;

    // NetworkRunner�̊i�[�p�ϐ�
    public NetworkRunner runner;

    // �e��ݒ�
    public GameManager.GAME_MODE gameMode;
    public GameManager.SELECTION_TURN selectionTurn;
    public GameManager.CARD_SETS cardSetNumber;
    public GameManager.CARD_POOL[] cardPools;
    public GameManager.CARD_BLOCK cardBlock;

    // �e��f�[�^�x�[�X
    [SerializeField] private GunnerDatabase gunnerDataBase01;
    [SerializeField] private GunnerDatabase gunnerDataBase02;
    [SerializeField] private GunnerDatabase gunnerDataBase03;
    [SerializeField] private GunnerDatabase gunnerDataBase04;

    [SerializeField] private GunsDatabase gunsDataBase01;
    [SerializeField] private GunsDatabase gunsDataBase02;
    [SerializeField] private GunsDatabase gunsDataBase03;
    [SerializeField] private GunsDatabase gunsDataBase04;

    // �e���j���[��GameObject
    [SerializeField] InitialMenu initialMenu;
    [SerializeField] BattleStandbyMenu battleStandbyMenu;

    public void Awake()
    {
        instance = this;
    }

    public void EnableInitial()
    {
        if (!initialMenu.gameObject.activeSelf) { initialMenu.gameObject.SetActive(true); }
    }
}
