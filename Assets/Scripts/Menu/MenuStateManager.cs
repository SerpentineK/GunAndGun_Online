using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuStateManager : MonoBehaviour
{
    public static MenuStateManager instance;

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
    [SerializeField] private InitialMenu initialMenu;
    [SerializeField] private BattleStandbyMenu battleStandbyMenu;

    public CardpoolDatabase[] CardpoolDatabases { get { return cardpoolDatabases; } }

    public void Awake()
    {
        instance = this;
    }
    public static void ToggleGameObject(GameObject myObject, bool state)
    {
        if (myObject.activeSelf != state) { myObject.SetActive(state); }
    }

    public void EnableInitial()
    {
        ToggleGameObject(initialMenu.gameObject, true);

        List<GunnerData> gunners = new();

        foreach (var cardpool in CardpoolDatabases)
        {
            gunners.AddRange(cardpool.GunnerDatabase.GetGunnerDataList());
        }

        GunnerData rightGunnerData = gunners[Random.Range(0, gunners.Count)];
        gunners.Remove(rightGunnerData);
        GunnerData leftGunnerData = gunners[Random.Range(0, gunners.Count)];

        initialMenu.SetGunnerFigures(rightGunnerData, leftGunnerData);
    }
}
