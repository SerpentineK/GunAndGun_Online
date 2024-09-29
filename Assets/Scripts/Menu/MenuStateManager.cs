using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuStateManager : MonoBehaviour
{

    // NetworkRunnerの格納用変数
    [Header("Runner")]
    public NetworkRunner runner;

    // 各種設定
    [Header("Settings")]
    public GameManager.GAME_MODE gameMode;
    public GameManager.SELECTION_TURN selectionTurn;
    public GameManager.CARD_SETS cardSetNumber;
    public GameManager.CARD_POOL[] cardPools;
    public GameManager.CARD_BLOCK cardBlock;

    // 各種データベース
    [Header("Databases")]
    [SerializeField] private CardpoolDatabase[] cardpoolDatabases;

    // 各メニューのGameObject
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
