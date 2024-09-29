using Fusion;
using Metaphysics;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MenuStateManager;

namespace BattleStandbyMenu
{
    public class BattleStandbyState : MonoBehaviour, IState
    {
        // Metaphysicsから来た情報を入力する変数
        private CardpoolDatabase[] databases;
        private NetworkingManager networkingManager;
        private SelectionRecordManager recordManager;

        [SerializeField] private ChatSystem chatSystem;
        [SerializeField] private SessionInfoDisplay infoDisplay;
        [SerializeField] private MemberDisplay memberDisplay;

        public string Username { get; private set; }

        private NetworkRunner runner;

        private bool inConnection = false;

        public void EnterState()
        {
            // BattleStandbyMenu本体を表示
            ToggleGameObject(gameObject, true);

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

            // ユーザーネームとRunner取得
            Username = networkingManager.LocalMenuData.Username;
            runner = networkingManager.MyRunner;

            // Infoシステム初期設定
            infoDisplay.Initialize(runner);

            // Chatシステム初期設定
            chatSystem.Initialize(runner, Username);

            // 入室通知
            string entryMessage = "[" + Username + "] が入室しました。";
            chatSystem.SendSystemMessage(entryMessage);

            // MemberDisplay初期設定
            memberDisplay.SetLocal(Username);
        }

        public void InState()
        {
            if (runner.SessionInfo.PlayerCount != runner.SessionInfo.MaxPlayers)
            {
                if (infoDisplay.currentStatus != SessionInfoDisplay.SessionStatus.WAITING_FOR_PLAYER)
                {
                    infoDisplay.UpdateSessionStatus(SessionInfoDisplay.SessionStatus.WAITING_FOR_PLAYER);
                }
            }
            else if (inConnection)
            {
                if (infoDisplay.currentStatus != SessionInfoDisplay.SessionStatus.CONNECTING)
                {
                    infoDisplay.UpdateSessionStatus(SessionInfoDisplay.SessionStatus.CONNECTING);
                }
            }
            else
            {
                if (infoDisplay.currentStatus != SessionInfoDisplay.SessionStatus.READY_FOR_BATTLE)
                {
                    infoDisplay.UpdateSessionStatus(SessionInfoDisplay.SessionStatus.READY_FOR_BATTLE);
                }
            }
        }

        public void ExitState()
        {

        }
    }
}