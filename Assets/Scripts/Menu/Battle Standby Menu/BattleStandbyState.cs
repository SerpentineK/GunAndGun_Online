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
        // Metaphysics���痈��������͂���ϐ�
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
            // BattleStandbyMenu�{�̂�\��
            ToggleGameObject(gameObject, true);

            // Metaphysics�V�[�����擾
            Scene metaScene = SceneManager.GetSceneByName("Metaphysics");

            // Metaphysics����f�[�^�x�[�X�̎Q�Ƃ�NetworkingManager�̃R���|�[�l���g������Ă���
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

            // ���[�U�[�l�[����Runner�擾
            Username = networkingManager.LocalMenuData.Username;
            runner = networkingManager.MyRunner;

            // Info�V�X�e�������ݒ�
            infoDisplay.Initialize(runner);

            // Chat�V�X�e�������ݒ�
            chatSystem.Initialize(runner, Username);

            // �����ʒm
            string entryMessage = "[" + Username + "] ���������܂����B";
            chatSystem.SendSystemMessage(entryMessage);

            // MemberDisplay�����ݒ�
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