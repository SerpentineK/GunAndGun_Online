using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion;

namespace BattleStandbyMenu
{
    public class ChatLogObject : NetworkBehaviour
    {
        [SerializeField] private TMP_Text speakerDisplay;
        [SerializeField] private TMP_Text speechDisplay;

        private readonly string sysName = "System";

        public override void Spawned()
        {
            base.Spawned(); 
            FindAnyObjectByType<ChatDisplay>().RegisterLog(this);
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        public void RPC_SetChatContent(string speaker, string speech)
        {
            if (speaker == sysName)
            {
                speakerDisplay.color = Color.blue;
            }
            else
            {
                speakerDisplay.color = Color.black;
            }

            speaker = "["+speaker+"]:";
            
            speakerDisplay.SetText(speaker);
            speechDisplay.SetText(speech);
        }
    }
}