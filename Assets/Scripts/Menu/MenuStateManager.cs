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
