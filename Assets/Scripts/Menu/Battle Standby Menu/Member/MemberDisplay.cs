using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BattleStandbyMenu
{
    public class MemberDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text localUsernameArea;
        [SerializeField] private TMP_Text remoteUsernameArea;

        public void SetLocal(string local)
        {
            if (local != null) { localUsernameArea.SetText(local); }
            else { localUsernameArea.SetText("---"); }
        }

        public void SetRemote(string remote)
        {
            if (remote != null) { remoteUsernameArea.SetText(remote); }
            else { remoteUsernameArea.SetText("---"); }
        }
    }
}