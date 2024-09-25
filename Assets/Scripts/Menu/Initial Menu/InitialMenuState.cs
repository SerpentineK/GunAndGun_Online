using Fusion;
using InitialMenu;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.Unicode;
using static MenuStateManager;
using Metaphysics;
using static AsyncSceneLoader;

public class InitialMenuState : MonoBehaviour, IState
{
    // Metaphysicsから来た情報を入力する変数
    private CardpoolDatabase[] databases;
    private NetworkingManager networkingManager;
    private SelectionRecordManager recordManager;

    // Controllerオブジェクト
    [SerializeField] private MenuStateController controller;

    // (オンライン対戦用) セッションIDとニックネームを入力するためのウィンドウ
    [Header("Input Window")]
    [SerializeField] private GameObject inputWindow;
    [SerializeField] private Toggle toggle_randomSession;
    [SerializeField] private TMP_Text toggle_footnotes;
    [SerializeField] private TMP_InputField field_sessionID;
    [SerializeField] private TMP_Text sessionID_footnotes;
    [SerializeField] private TMP_InputField field_nickname;

    private readonly string toggle_InactiveNotes = "ランダム対戦がオフの場合\n指定したIDの対戦部屋に接続されます";
    private readonly string toggle_ActiveNotes = "ランダム対戦がオンの場合\nランダムな対戦部屋に接続されます\n（セッションIDは不要です）";

    private readonly string sessionID_InactiveNotes = "ランダム対戦がオンになっているため\nIDを入力することはできません";
    private readonly string sessionID_ActiveNotes = "IDが未入力の場合\nランダムなIDで対戦部屋を作成します";

    // (ボスバトル用) 対戦相手にするボスを選択するためのウィンドウ
    [Header("Boss Window")]
    [SerializeField] private GameObject bossWindow;
    [SerializeField] private Transform bossCandidateParent;
    [SerializeField] private GameObject bossCandidatePrefab;
    [SerializeField] private Button bossEntryButton;
    private BossData selectedBossData = null;

    [Header("Tutorial Confirmation Window")]
    [SerializeField] private GameObject tutorialWindow;

    // 各種ボタン
    [Header("Buttons")]
    [SerializeField] private Button battleButton;
    [SerializeField] private Button bossButton;
    [SerializeField] private Button tutorialButton;

    // 左右の銃士イメージ
    [Header("Gunner Figures")]
    [SerializeField] private SpriteRenderer rightGunnerFigure;
    [SerializeField] private SpriteRenderer leftGunnerFigure;

    // BossCandidateを入れるためのArray（将来必要になる）
    private BossCandidate[] bossCandidateArray;

    public void EnterState()
    {
        // InitialMenu本体を表示
        ToggleGameObject(gameObject, true);

        // InputWindow、BossWindow、TutorialWindowを非表示
        ToggleGameObject(inputWindow, false);
        ToggleGameObject(bossWindow, false);
        ToggleGameObject(tutorialWindow, false);

        // InputWindowの値を初期化する
        toggle_randomSession.isOn = false;
        toggle_footnotes.SetText(toggle_InactiveNotes);
        field_sessionID.SetTextWithoutNotify("");
        sessionID_footnotes.SetText(sessionID_ActiveNotes);
        field_nickname.SetTextWithoutNotify("");

        // Metaphysicsシーンを取得
        Scene metaScene = SceneManager.GetSceneByName("Metaphysics");

        // Metaphysicsからデータベースの参照とNetworkingManagerのコンポーネントを取ってくる
        foreach (var rootObj in metaScene.GetRootGameObjects())
        {
            if (rootObj.TryGetComponent<DatabaseRef>(out var databaseRef))
            {
                databases = databaseRef.Databases;
            }
            else if (rootObj.TryGetComponent<NetworkingManager>(out var networking))
            {
                networkingManager = networking;
            }
            else if (rootObj.TryGetComponent<SelectionRecordManager>(out var recordM))
            {
                recordManager = recordM;
            }
        }

        List<GunnerData> gunners = new();
        List<BossData> bosses = new();

        foreach (var cardpool in databases)
        {
            gunners.AddRange(cardpool.GunnerDatabase.GetGunnerDataList());
            bosses.AddRange(cardpool.Bosses);
        }

        // 左右の銃士画像をランダムに決定
        GunnerData rightGunnerData = gunners[Random.Range(0, gunners.Count)];
        gunners.Remove(rightGunnerData);
        GunnerData leftGunnerData = gunners[Random.Range(0, gunners.Count)];

        SetGunnerFigures(rightGunnerData, leftGunnerData);

        bossCandidateArray = new BossCandidate[bosses.Count];
        int count = 0;
        if(bossCandidateParent.childCount != 0)
        {
            foreach (Transform child in bossCandidateParent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        // BossWindowに表示するBossCandidateを量産(現状はラン一人だけ)
        foreach (var boss in bosses)
        {
            if (Instantiate(bossCandidatePrefab, bossCandidateParent).TryGetComponent<BossCandidate>(out var candidate))
            {
                candidate.SetCandidateData(boss);
                bossCandidateArray[count] = candidate;
                candidate.initialS = this;
                count++;
            }
        }

        // BossEntryButtonのinteractableを切っておく（ボスを選んでない状態でEntryされると困るので）
        bossEntryButton.interactable = false;
    }

    public void InState(){ }

    public void ExitState() 
    {
        ToggleGameObject(gameObject, false);
    }

    public void SetGunnerFigures(GunnerData rightData, GunnerData leftData)
    {
        rightGunnerFigure.sprite = rightData.GetGunnerImage();
        leftGunnerFigure.sprite = leftData.GetGunnerImage();
    }


    public void SelectBossCandidate(BossCandidate selected)
    {
        foreach (var boss in bossCandidateArray)
        {
            boss.ToggleSelectionState(boss == selected);
        }
        selectedBossData = (selected != null) ? selected.MyData : null;
        bossEntryButton.interactable = (selectedBossData != null);
    }

    public void ToggleButtons(bool result)
    {
        battleButton.interactable = result;
        bossButton.interactable = result;
        tutorialButton.interactable = result;
    }

    public void OnButtonPressed_Battle()
    {
        ToggleGameObject(inputWindow, true);
        ToggleButtons(false);
    }

    public void OnButtonPressed_Boss()
    {
        ToggleGameObject(bossWindow, true);
        ToggleButtons(false);
    }

    public void OnButtonPressed_Tutorial()
    {
        ToggleGameObject(tutorialWindow, true);
        ToggleButtons(false);
    }

    public void OnToggle_RandomSession()
    {
        field_sessionID.interactable = !toggle_randomSession.isOn;
        if (!field_sessionID.interactable)
        {
            field_sessionID.SetTextWithoutNotify("");
            toggle_footnotes.SetText(toggle_ActiveNotes);
            sessionID_footnotes.SetText(sessionID_InactiveNotes);
        }
        else
        {
            toggle_footnotes.SetText(toggle_InactiveNotes);
            sessionID_footnotes.SetText(sessionID_ActiveNotes);
        }
    }

    public void OnButtonPressed_InputEntry()
    {
        string sessionID = field_sessionID.text;
        string nickname = field_nickname.text;
        networkingManager.EnterSession(sessionID, nickname);
        controller.SwitchStates(controller.battleStandbyS);
    }

    public void OnButtonPressed_InputExit()
    {
        field_sessionID.SetTextWithoutNotify("");
        field_nickname.SetTextWithoutNotify("");
        ToggleGameObject(inputWindow, false);
        ToggleButtons(true);
    }

    public void OnButtonPressed_BossEntry()
    {
        recordManager.StartRecording_Singleplayer(selectedBossData);
        controller.SwitchStates(controller.bossStandbyS);
    }

    public void OnButtonPressed_BossExit()
    {
        SelectBossCandidate(null);
        ToggleGameObject(bossWindow, false);
        ToggleButtons(true);
    }
    public void OnButtonPressed_TutorialEntry()
    {
        StartCoroutine(LoadNextSceneAsync("TutorialScene"));
        StartCoroutine(UnloadPreviousSceneAsync(gameObject.scene.name));
    }

    public void OnButtonPressed_TutorialExit()
    {
        ToggleGameObject(tutorialWindow, false);
        ToggleButtons(true);
    }
}
