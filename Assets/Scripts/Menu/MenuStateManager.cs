using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuStateManager : MonoBehaviour
{

    // NetworkRunner�̊i�[�p�ϐ�
    [Header("Runner")]
    public NetworkRunner runner;

    // �e��ݒ�
    [Header("Settings")]
    public GameManager.GAME_MODE gameMode;
    public GameManager.SELECTION_TURN selectionTurn;
    public GameManager.CARD_SETS cardSetNumber;
    public GameManager.CARD_POOL[] cardPools;
    public GameManager.CARD_BLOCK cardBlock;

    // �e��f�[�^�x�[�X
    [Header("Databases")]
    [SerializeField] private CardpoolDatabase[] cardpoolDatabases;

    // �e���j���[��GameObject
    [Header("Menu Objects")]
    [SerializeField] private InitialMenu.InitialMenuState initialMenu;
    [SerializeField] private BattleStandbyMenu.BattleStandbyState battleStandbyMenu;
    [SerializeField] private BossStandbyMenu.BossStandbyState bossStandbyMenu;

    public BossData selectedBoss = null;

    private GameObject[] menuObjects;

    public static void ToggleGameObject(GameObject myObject, bool state)
    {
        if (myObject.activeSelf != state) { myObject.SetActive(state); }
    }
}
