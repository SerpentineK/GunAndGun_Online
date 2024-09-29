using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UtilityTools;

namespace BattleStandbyMenu
{
    public class ChatDisplay : MonoBehaviour
    {
        [SerializeField] private ScrollRect logScrollRect;

        public void Awake()
        {
            DestroyAllChildren(logScrollRect.content);
        }

        public void RegisterLog(ChatLogObject chatLogObject)
        {
            chatLogObject.transform.SetParent(logScrollRect.content.transform);
        }
    }
}