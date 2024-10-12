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
using BattleStandbyMenu;

namespace InitialMenu
{
    public class InitialMenuState : MonoBehaviour, IState
    {
        // Metaphysicsから来た情報を入力する変数
        private CardpoolDatabase[] databases;
        private NetworkingManager networkingManager;
        private SelectionRecordManager recordManager;

        // Controllerオブジェクト
        [SerializeField] private MenuStateController controller;

        // (オンライン対戦用) セッションIDとニックネームを入力するためのウィンドウ
        [Header("Session Window")]
        [SerializeField] private GameObject sessionWindow;

        [Header("Specify Session")]
        [SerializeField] private GameObject specificSessionWindow;
        [SerializeField] private TMP_InputField sessionID_SSW;
        [SerializeField] private TMP_InputField username_SSW;
        [SerializeField] private TMP_Dropdown publicity_SSW;

        [Header("Random Session")]
        [SerializeField] private GameObject randomSessionWindow;
        [SerializeField] private TMP_InputField username_RSW;
        [SerializeField] private TMP_Dropdown matchingMode_RSW;
        [SerializeField] private GameObject errorMessage_RSW;

        [Header("Loading Display")]
        [SerializeField] private GameObject LoadingCanvas;

        private GameObject currentInputWindow = null;

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

            // InputWindow、BossWindow、TutorialWindow、LoadingCanvasを非表示
            ToggleGameObject(sessionWindow, false);
            ToggleGameObject(bossWindow, false);
            ToggleGameObject(tutorialWindow, false);
            ToggleGameObject(LoadingCanvas, false);

            // InputWindowの値を初期化する
            BatchResetValues();

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
            if (bossCandidateParent.childCount != 0)
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

        public void InState() { }

        public void ExitState()
        {
            ToggleGameObject(gameObject, false);
        }

        public void SetGunnerFigures(GunnerData rightData, GunnerData leftData)
        {
            rightGunnerFigure.sprite = rightData.GetGunnerImage();
            leftGunnerFigure.sprite = leftData.GetGunnerImage();
        }

        private void BatchResetValues()
        {
            ToggleGameObject(specificSessionWindow, true);
            ToggleGameObject(randomSessionWindow, false);
            currentInputWindow = specificSessionWindow;

            sessionID_SSW.SetTextWithoutNotify("");
            username_SSW.SetTextWithoutNotify("");
            publicity_SSW.value = 0;

            username_RSW.SetTextWithoutNotify("");
            matchingMode_RSW.value = 0;
            ToggleGameObject(errorMessage_RSW, false);
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

        public void OnButtonPressed_Online()
        {
            ToggleGameObject(sessionWindow, true);
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

        public void OnButtonPressed_WindowChange(GameObject nextWindow)
        {
            if (nextWindow != currentInputWindow)
            {
                if (currentInputWindow != null) { ToggleGameObject(currentInputWindow, false); }
                if (nextWindow != null) { ToggleGameObject(nextWindow, true); }
                currentInputWindow = nextWindow;
            }
        }

        public async void OnButtonPressed_EntrySSW()
        {
            string sessionID = sessionID_SSW.text;
            string username = username_SSW.text;
            bool isPublic = publicity_SSW.value == 0;
            ToggleGameObject(LoadingCanvas, true);
            var result = await networkingManager.ConnectToSession(sessionID, username, isPublic);
            ToggleGameObject(LoadingCanvas, false);
            if (result == NetworkingManager.ConnectionResult.CONNECTED)
            {
                controller.SwitchStates(controller.battleStandbyS);
            }
        }

        public async void OnButtonPressed_EntryRSW()
        {
            string username = username_RSW.text;
            bool createSession = matchingMode_RSW.value == 0;
            ToggleGameObject(LoadingCanvas, true);
            var result = await networkingManager.ConnectToRandomSession(username, createSession);
            ToggleGameObject(LoadingCanvas, false);
            if (result == NetworkingManager.ConnectionResult.CONNECTED)
            {
                controller.SwitchStates(controller.battleStandbyS);
            }
            else if (result == NetworkingManager.ConnectionResult.NOT_FOUND)
            {
                ToggleGameObject(errorMessage_RSW, true);
            }
        }

        public void OnButtonPressed_InputExit()
        {
            BatchResetValues();
            ToggleGameObject(sessionWindow, false);
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
}