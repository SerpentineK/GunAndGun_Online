using BattleStandbyMenu;
using Metaphysics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UtilityTools;
using Fusion;

namespace BattleStandbyMenu
{
    public class ChatSystem : NetworkBehaviour
    {
        [Header("Input")]
        [SerializeField] private TMP_InputField chatInputField;

        [Header("Prefab")]
        [SerializeField] private NetworkObject chatLogPrefab;

        private string username;
        private NetworkRunner runner;

        public void Initialize(NetworkRunner _runner, string _username)
        {
            username = _username;
            runner = _runner;
        }

        public void SendChat()
        {
            string chatInput = chatInputField.text;

            if (runner.Spawn(chatLogPrefab).TryGetComponent<ChatLogObject>(out var logObj))
            {
                logObj.RPC_SetChatContent(username, chatInput);
            }

            chatInputField.SetTextWithoutNotify("");
        }

        public void SendSystemMessage(string message)
        {
            if (runner.Spawn(chatLogPrefab).TryGetComponent<ChatLogObject>(out var logObj))
            {
                logObj.RPC_SetChatContent("System", message);
            }
        }
    }
}