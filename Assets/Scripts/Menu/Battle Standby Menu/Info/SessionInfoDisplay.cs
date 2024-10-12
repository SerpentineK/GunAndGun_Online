using Fusion;
using Metaphysics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UtilityTools;

namespace BattleStandbyMenu
{
    public class SessionInfoDisplay : MonoBehaviour
    {
        // セッションの状態を表すenum
        public enum SessionStatus
        {
            NONE,
            WAITING_FOR_PLAYER,
            READY_FOR_BATTLE,
            CONNECTING,
            IN_BATTLE
        }

        [SerializeField] private TMP_Text display_ID;
        [SerializeField] private TMP_Text display_Region;
        [SerializeField] private TMP_Text display_Status;

        public Dictionary<SessionStatus, string> statusDict = new()
        {
            [SessionStatus.NONE] = "N/A",
            [SessionStatus.WAITING_FOR_PLAYER] = "参加待機中",
            [SessionStatus.READY_FOR_BATTLE] = "戦闘準備完了",
            [SessionStatus.CONNECTING] = "接続中",
            [SessionStatus.IN_BATTLE] = "戦闘中"
        };

        private NetworkRunner runner;

        public SessionStatus currentStatus = SessionStatus.NONE;

        public void Initialize(NetworkRunner _runner)
        {
            runner = _runner;

            string myID = runner.SessionInfo.Name;
            string myRegion = PhotonRegionDictionary[runner.SessionInfo.Region];
            display_ID.SetText(myID);
            display_Region.SetText(myRegion);

            UpdateSessionStatus(SessionStatus.NONE);
        }

        public void UpdateSessionStatus(SessionStatus status)
        {
            currentStatus = status;

            display_Status.SetText(statusDict[currentStatus]);
        }
    }
}